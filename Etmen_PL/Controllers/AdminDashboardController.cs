using Etmen_BLL.Repositories.IServices;
using Etmen_PL.Models.ViewModels.Admin;
using Etmen_DAL.Repositories.Interfaces;
using Etmen_Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Etmen_PL.Controllers
{
    /// <summary>
    /// Admin Dashboard Controller
    /// System overview telemetry dashboard
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<AdminDashboardController> _logger;

        public AdminDashboardController(
            IAdminService adminService,
            IUnitOfWork uow,
            ILogger<AdminDashboardController> logger)
        {
            _adminService = adminService;
            _uow = uow;
            _logger = logger;
        }

        /// <summary>
        /// GET: /AdminDashboard/Index
        /// Shows active users, appointments, and crisis status
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _adminService.GetDashboardAsync();
                if (!result.IsSuccess || result.Data is null)
                {
                    TempData["Error"] = result.ErrorMessage ?? "Error loading admin dashboard";
                    return RedirectToAction("Index", "Home");
                }

                var viewModel = new AdminDashboardViewModel
                {
                    TotalUsers = result.Data.TotalUsers,
                    ActiveDoctors = result.Data.ActiveDoctors,
                    ActivePatients = result.Data.ActivePatients,
                    TotalAppointments = result.Data.TotalAppointments,
                    PendingEmergencyRequests = result.Data.PendingEmergencyRequests,
                    IsCrisisModeActive = result.Data.IsCrisisModeActive,
                    ActiveCrisisName = result.Data.ActiveCrisisName
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving admin dashboard");
                TempData["Error"] = "خطأ في تحميل لوحة التحكم";
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// GET: /AdminDashboard/NormalMap
        /// Shows full screen map of clinics, hospitals, doctors, and emergency cases in normal mode
        /// </summary>
        [HttpGet]
        public IActionResult NormalMap()
        {
            return View();
        }

        /// <summary>
        /// GET: /AdminDashboard/GetMapData
        /// Returns system telemetry mapping coordinates for clinics, hospitals, doctors, and critical cases
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetMapData()
        {
            try
            {
                // 1. Query Healthcare Providers (Clinics and Hospitals)
                var providersList = await _uow.HealthcareProviders.Table
                    .Where(p => p.IsActive)
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        p.Type,
                        Latitude = (double)p.Latitude,
                        Longitude = (double)p.Longitude,
                        p.Address,
                        p.Phone
                    })
                    .ToListAsync();

                // 2. Query Doctors (registered, with onboarding location)
                var doctorsListRaw = await _uow.DoctorProfiles.Table
                    .Include(d => d.ApplicationUser)
                    .Where(d => d.IsOnboarded && !string.IsNullOrEmpty(d.OnboardingDataJson))
                    .ToListAsync();

                var doctorsList = new List<object>();
                foreach (var doc in doctorsListRaw)
                {
                    try
                    {
                        var data = JsonSerializer.Deserialize<Dictionary<string, object>>(doc.OnboardingDataJson!);
                        if (data != null && 
                            data.TryGetValue("Latitude", out var latVal) && double.TryParse(latVal.ToString(), out var lat) &&
                            data.TryGetValue("Longitude", out var lngVal) && double.TryParse(lngVal.ToString(), out var lng))
                        {
                            doctorsList.Add(new
                            {
                                doc.Id,
                                Name = doc.FullName ?? $"{doc.ApplicationUser?.FirstName} {doc.ApplicationUser?.LastName}".Trim(),
                                doc.Specialization,
                                ConsultationFee = doc.ConsultationFee ?? 0,
                                Latitude = lat,
                                Longitude = lng
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error parsing doctor onboarding coordinates for doc ID {DocId}", doc.Id);
                    }
                }

                // 3. Query Critical Cases (Emergency requests where Status != Completed)
                var criticalCases = await _uow.EmergencyRequests.Table
                    .Include(e => e.PatientProfile)
                    .ThenInclude(p => p.ApplicationUser)
                    .Where(e => e.Status != EmergencyRequestStatus.Completed)
                    .ToListAsync();

                var criticalList = new List<object>();
                foreach (var req in criticalCases)
                {
                    double? lat = (double?)req.Latitude;
                    double? lng = (double?)req.Longitude;

                    // Fallback to patient profile coordinates if request doesn't have them
                    if ((lat == null || lat == 0) && req.PatientProfile != null)
                    {
                        lat = (double?)req.PatientProfile.Latitude;
                        lng = (double?)req.PatientProfile.Longitude;
                    }

                    if (lat.HasValue && lng.HasValue && lat != 0 && lng != 0)
                    {
                        criticalList.Add(new
                        {
                            req.Id,
                            PatientName = req.PatientProfile?.FullName ?? $"{req.PatientProfile?.ApplicationUser?.FirstName} {req.PatientProfile?.ApplicationUser?.LastName}".Trim(),
                            EmergencyType = req.EmergencyType ?? "General Emergency",
                            Description = req.Description ?? string.Empty,
                            Latitude = lat.Value,
                            Longitude = lng.Value,
                            PriorityScore = req.PriorityScore
                        });
                    }
                }

                return Json(new
                {
                    success = true,
                    providers = providersList,
                    doctors = doctorsList,
                    criticalCases = criticalList
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching map markers data");
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
