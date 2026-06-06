using Etmen_BLL.Repositories.IServices;
using Etmen_PL.Models.ViewModels.Doctor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen_PL.Controllers
{
    /// <summary>
    /// Doctor Appointments Controller
    /// Manages scheduled consultations
    /// </summary>
    [Authorize(Roles = "Doctor")]
    public class DoctorAppointmentsController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly ILogger<DoctorAppointmentsController> _logger;

        public DoctorAppointmentsController(
            IDoctorService doctorService,
            ILogger<DoctorAppointmentsController> logger)
        {
            _doctorService = doctorService;
            _logger = logger;
        }

        /// <summary>
        /// GET: /DoctorAppointments/Index
        /// Lists scheduled appointments
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                _logger.LogInformation("Doctor appointments list accessed for user {UserId}", userId);

                var appointmentsResult = await _doctorService.GetAppointmentsAsync(userId);

                if (!appointmentsResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to fetch appointments for doctor {UserId}", userId);
                    ModelState.AddModelError(string.Empty, "Failed to load appointments");
                    return View(new List<Etmen_BLL.DTOs.Doctor.DoctorAppointmentDto>());
                }

                return View(appointmentsResult.Data ?? new List<Etmen_BLL.DTOs.Doctor.DoctorAppointmentDto>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving appointments");
                TempData["Error"] = "Error loading appointments";
                return View(new List<Etmen_BLL.DTOs.Doctor.DoctorAppointmentDto>());
            }
        }

        /// <summary>
        /// GET: /DoctorAppointments/Details
        /// Shows specific appointment details
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "Invalid appointment ID";
                    return RedirectToAction(nameof(Index));
                }

                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var appointmentResult = await _doctorService.GetAppointmentAsync(userId, id);

                if (!appointmentResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to fetch appointment {AppointmentId} for doctor {UserId}", id, userId);
                    TempData["Error"] = "Appointment not found or access denied";
                    return RedirectToAction(nameof(Index));
                }

                return View(appointmentResult.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving appointment details");
                TempData["Error"] = "Error loading appointment details";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// GET: /DoctorAppointments/UpdateStatus
        /// Show update status form
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> UpdateStatus(int id)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "Invalid appointment ID";
                    return RedirectToAction(nameof(Index));
                }

                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var appointmentResult = await _doctorService.GetAppointmentAsync(userId, id);

                if (!appointmentResult.IsSuccess)
                {
                    TempData["Error"] = "Appointment not found";
                    return RedirectToAction(nameof(Index));
                }

                var viewModel = new UpdateAppointmentStatusViewModel
                {
                    AppointmentId = id,
                    Status = appointmentResult.Data?.Status ?? ""
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading update status form");
                TempData["Error"] = "Error loading form";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// POST: /DoctorAppointments/UpdateStatus
        /// Updates appointment status
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(UpdateAppointmentStatusViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var statusDto = new Etmen_BLL.DTOs.Doctor.UpdateAppointmentStatusDto
                {
                    Status = viewModel.Status,
                    Notes = viewModel.Notes
                };

                var result = await _doctorService.UpdateAppointmentStatusAsync(userId, viewModel.AppointmentId, statusDto);

                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Failed to update appointment {AppointmentId} for doctor {UserId}", 
                        viewModel.AppointmentId, userId);
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error);
                    return View(viewModel);
                }

                _logger.LogInformation("Appointment {AppointmentId} status updated for user {UserId}", 
                    viewModel.AppointmentId, userId);
                TempData["Success"] = "Appointment status updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating appointment status");
                ModelState.AddModelError(string.Empty, "Error updating appointment status");
                return View(viewModel);
            }
        }
    }
}
