using Etmen_BLL.DTOs.Nearby;
using Etmen_BLL.Repositories.IServices;
using Etmen_PL.Models.ViewModels.Patient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Etmen_PL.Controllers
{
    /// <summary>
    /// Nearby Providers Controller
    /// Queries and maps nearest emergency clinics.
    /// </summary>
    [Authorize(Roles = "Patient")]
    public class NearbyProvidersController : Controller
    {
        private readonly INearbyService _nearbyService;
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly ILogger<NearbyProvidersController> _logger;

        public NearbyProvidersController(
            INearbyService nearbyService,
            IAppointmentService appointmentService,
            IPatientService patientService,
            ILogger<NearbyProvidersController> logger)
        {
            _nearbyService = nearbyService;
            _appointmentService = appointmentService;
            _patientService = patientService;
            _logger = logger;
        }

        /// <summary>
        /// GET: /NearbyProviders/Index
        /// Renders GPS location map finder.
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            return View(new NearbySearchViewModel());
        }

        /// <summary>
        /// POST: /NearbyProviders/Index
        /// Lists facilities near coordinate radius.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(NearbySearchViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            if (!viewModel.Latitude.HasValue || !viewModel.Longitude.HasValue)
            {
                ModelState.AddModelError(string.Empty, "Latitude and longitude are required.");
                return View(viewModel);
            }

            try
            {
                var dto = new NearbySearchDto
                {
                    Latitude = viewModel.Latitude.Value,
                    Longitude = viewModel.Longitude.Value,
                    RadiusInKm = viewModel.RadiusInKm,
                    Type = viewModel.Type
                };

                var result = await _nearbyService.SearchNearbyProvidersAsync(dto);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Nearby providers search failed: {Message}", result.ErrorMessage);
                    ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Unable to search nearby providers.");
                    return View(viewModel);
                }

                viewModel.SearchResults = result.Data ?? new List<ProviderDto>();
                _logger.LogInformation("Nearby providers search returned {Count} providers", viewModel.SearchResults.Count);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching nearby providers");
                ModelState.AddModelError(string.Empty, "Error searching nearby providers.");
                return View(viewModel);
            }
        }

        /// <summary>
        /// POST: /NearbyProviders/Book
        /// Books a slot with a doctor.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(int providerId, int slotId)
        {
            try
            {
                if (providerId <= 0 || slotId <= 0)
                {
                    TempData["Error"] = "Valid provider and slot are required.";
                    return RedirectToAction(nameof(Index));
                }

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrWhiteSpace(userId))
                    return RedirectToAction("Login", "Account");

                var profileResult = await _patientService.GetProfileAsync(userId);
                if (!profileResult.IsSuccess || profileResult.Data == null)
                {
                    TempData["Error"] = profileResult.ErrorMessage ?? "Patient profile was not found.";
                    return RedirectToAction(nameof(Index));
                }

                var slotsResult = await _nearbyService.GetAvailableSlotsByProviderAsync(providerId);
                if (!slotsResult.IsSuccess || slotsResult.Data == null)
                {
                    TempData["Error"] = slotsResult.ErrorMessage ?? "Unable to load available slots.";
                    return RedirectToAction(nameof(Index));
                }

                var slot = slotsResult.Data.FirstOrDefault(s => s.Id == slotId);
                if (slot == null)
                {
                    TempData["Error"] = "Selected slot is not available.";
                    return RedirectToAction(nameof(Index));
                }

                var booking = new BookingRequestDto
                {
                    PatientProfileId = profileResult.Data.Id,
                    DoctorId = slot.DoctorId > 0 ? slot.DoctorId : providerId,
                    SlotId = slot.Id,
                    Date = slot.Date,
                    StartTime = slot.StartTime,
                    EndTime = slot.EndTime
                };

                var result = await _appointmentService.BookAppointmentAsync(userId, booking);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("Failed to book appointment for user {UserId}: {Message}", userId, result.ErrorMessage);
                    TempData["Error"] = result.ErrorMessage ?? "Unable to book appointment.";
                    return RedirectToAction(nameof(Index));
                }

                _logger.LogInformation("Appointment booked for user {UserId}", userId);
                TempData["Success"] = "Appointment booked successfully.";
                return RedirectToAction("Index", "PatientDashboard");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error booking appointment");
                TempData["Error"] = "Error booking appointment.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
