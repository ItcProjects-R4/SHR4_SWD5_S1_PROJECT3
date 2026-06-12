using Etmen_BLL.DTOs.Nearby;
using Etmen_BLL.Repositories.IServices;
using Etmen_PL.Models.ViewModels.Patient;
using Etmen_DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Etmen_PL.Controllers
{
    /// <summary>
    /// Nearby Providers Controller
    /// Queries and maps nearest emergency clinics
    /// </summary>
    [Authorize(Roles = "Patient")]
    public class NearbyProvidersController : Controller
    {
        private readonly INearbyService _nearbyService;
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<NearbyProvidersController> _logger;

        public NearbyProvidersController(
            INearbyService nearbyService,
            IAppointmentService appointmentService,
            IPatientService patientService,
            IUnitOfWork uow,
            ILogger<NearbyProvidersController> logger)
        {
            _nearbyService = nearbyService;
            _appointmentService = appointmentService;
            _patientService = patientService;
            _uow = uow;
            _logger = logger;
        }

        /// <summary>
        /// GET: /NearbyProviders/Index
        /// Renders GPS location map finder
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new NearbySearchViewModel();
            return View(viewModel);
        }

        /// <summary>
        /// POST: /NearbyProviders/Index
        /// Lists facilities near coordinate radius
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(NearbySearchViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            try
            {
                if (!viewModel.Latitude.HasValue || !viewModel.Longitude.HasValue)
                {
                    ModelState.AddModelError(string.Empty, "Location coordinates are required.");
                    return View(viewModel);
                }

                var searchDto = new NearbySearchDto
                {
                    Latitude = viewModel.Latitude.Value,
                    Longitude = viewModel.Longitude.Value,
                    RadiusInKm = viewModel.RadiusInKm,
                    Type = viewModel.Type
                };

                var result = await _nearbyService.SearchNearbyProvidersAsync(searchDto);
                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Error searching nearby providers.");
                    return View(viewModel);
                }

                viewModel.SearchResults = result.Data ?? new List<ProviderDto>();
                _logger.LogInformation("Nearby providers search performed at {Latitude}, {Longitude}", viewModel.Latitude, viewModel.Longitude);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching nearby providers");
                ModelState.AddModelError(string.Empty, "خطأ في البحث عن المراكز الصحية");
                return View(viewModel);
            }
        }

        /// <summary>
        /// GET: /NearbyProviders/GetSlots
        /// Retrieves available slots for a provider (AJAX)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetSlots(int providerId)
        {
            var result = await _nearbyService.GetAvailableSlotsByProviderAsync(providerId);
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);
            return Json(result.Data);
        }

        /// <summary>
        /// POST: /NearbyProviders/Book
        /// Books a slot with a doctor
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(int providerId, int slotId, int? doctorId = null)
        {
            try
            {
                var selectedDoctorId = doctorId.GetValueOrDefault(providerId);
                if (selectedDoctorId <= 0 || slotId <= 0)
                {
                    TempData["Error"] = "Invalid doctor or slot.";
                    return RedirectToAction(nameof(Index));
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrWhiteSpace(userId))
                    return RedirectToAction("Login", "Account");

                var profileResult = await _patientService.GetProfileAsync(userId);
                if (!profileResult.IsSuccess || profileResult.Data == null)
                {
                    TempData["Error"] = profileResult.ErrorMessage ?? "Patient profile not found.";
                    return RedirectToAction(nameof(Index));
                }

                var slotsResult = await _nearbyService.GetAvailableSlotsByProviderAsync(selectedDoctorId);
                if (!slotsResult.IsSuccess || slotsResult.Data == null)
                {
                    TempData["Error"] = slotsResult.ErrorMessage ?? "No available slots for this provider.";
                    return RedirectToAction(nameof(Index));
                }

                var slot = slotsResult.Data.FirstOrDefault(s => s.Id == slotId);
                if (slot == null)
                {
                    TempData["Error"] = "Selected slot is not available.";
                    return RedirectToAction(nameof(Index));
                }

                var bookingDto = new BookingRequestDto
                {
                    PatientProfileId = profileResult.Data.Id,
                    DoctorId = slot.DoctorId,
                    SlotId = slot.Id,
                    Date = slot.Date,
                    StartTime = slot.StartTime,
                    EndTime = slot.EndTime
                };

                var result = await _appointmentService.BookAppointmentAsync(userId, bookingDto);
                if (!result.IsSuccess)
                {
                    TempData["Error"] = result.ErrorMessage ?? "Error booking appointment.";
                    return RedirectToAction(nameof(Index));
                }

                _logger.LogInformation("Appointment booked for user {UserId} and slot {SlotId}", userId, slotId);
                TempData["Success"] = "تم حجز الموعد بنجاح";
                return RedirectToAction("Index", "PatientDashboard");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error booking appointment");
                TempData["Error"] = "خطأ في حجز الموعد";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// GET: /NearbyProviders/DoctorDetails/{id}
        /// Premium Vezeeta-style Doctor Details View
        /// </summary>
        [HttpGet("NearbyProviders/DoctorDetails/{id}")]
        public async Task<IActionResult> DoctorDetails(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest();

                // 1. Get Healthcare Provider details
                var provider = await _uow.HealthcareProviders.GetByIdAsync(id);
                if (provider == null)
                {
                    TempData["Error"] = "المنشأة الطبية غير موجودة";
                    return RedirectToAction(nameof(Index));
                }

                // 2. Find doctor linked to this provider in their onboarding JSON
                Etmen_Domain.Entities.DoctorProfile? doctor = null;
                var doctors = await _uow.DoctorProfiles.Table.Include(d => d.ApplicationUser).ToListAsync();
                foreach (var doc in doctors)
                {
                    if (!string.IsNullOrEmpty(doc.OnboardingDataJson))
                    {
                        try
                        {
                            using var docJson = System.Text.Json.JsonDocument.Parse(doc.OnboardingDataJson);
                            if (docJson.RootElement.TryGetProperty("HealthcareProviderId", out var prop) && prop.GetInt32() == id)
                            {
                                doctor = doc;
                                break;
                            }
                        }
                        catch { }
                    }
                }

                // 3. Get available slots
                var slotsResult = await _nearbyService.GetAvailableSlotsByProviderAsync(id);
                var slots = slotsResult.IsSuccess ? slotsResult.Data ?? new() : new();

                // Group slots by Date for the 3-day sliding calendar
                var viewModel = new DoctorDetailsViewModel
                {
                    ProviderId = id,
                    ProviderName = provider.Name,
                    ProviderAddress = provider.Address ?? string.Empty,
                    ProviderType = provider.Type,
                    DoctorId = doctor?.Id,
                    DoctorName = doctor?.FullName ?? (doctor?.ApplicationUser != null ? $"{doctor.ApplicationUser.FirstName} {doctor.ApplicationUser.LastName}".Trim() : string.Empty),
                    Specialization = doctor?.Specialization ?? "أخصائي عام",
                    Bio = doctor?.Bio ?? "أخصائي متميز بخبرة في الرعاية الطبية الشاملة وتقديم الاستشارات المتخصصة للمرضى.",
                    ConsultationFee = doctor?.ConsultationFee ?? 150.00m,
                    YearsOfExperience = doctor?.YearsOfExperience ?? 5,
                    LicenseNumber = doctor?.LicenseNumber ?? string.Empty,
                    AvailableSlots = slots
                };

                // Fallback for DoctorName if no doctor profile is linked
                if (string.IsNullOrWhiteSpace(viewModel.DoctorName))
                {
                    viewModel.DoctorName = provider.Name;
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading doctor booking details");
                TempData["Error"] = "حدث خطأ أثناء تحميل صفحة الطبيب";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
