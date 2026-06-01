using Etmen_BLL.Repositories.IServices;
using Etmen_PL.Models.ViewModels.Patient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen_PL.Controllers
{
    /// <summary>
    /// Emergency Controller
    /// Triggers ambulance requests and tracks dispatch
    /// </summary>
    [Authorize(Roles = "Patient")]
    public class EmergencyController : Controller
    {
        private readonly IEmergencyService _emergencyService;
        private readonly ILogger<EmergencyController> _logger;

        public EmergencyController(
            IEmergencyService emergencyService,
            ILogger<EmergencyController> logger)
        {
            _emergencyService = emergencyService;
            _logger = logger;
        }

        /// <summary>
        /// POST: /Emergency/RequestAmbulance
        /// Requests an emergency ambulance at coordinates
        /// TODO: Validate ModelState
        /// TODO: Get current user ID
        /// TODO: Call _emergencyService.CreateEmergencyRequestAsync(userId, dto)
        /// TODO: Store requestId in TempData
        /// TODO: Redirect to Track action
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestAmbulance(EmergencyRequestViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", "PatientDashboard");

            try
            {
                // TODO: Implementation
                _logger.LogInformation("Emergency ambulance requested");
                TempData["EmergencyRequestId"] = 0; // Store request ID
                return RedirectToAction(nameof(Track));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error requesting ambulance");
                TempData["Error"] = "خطأ في طلب الإسعاف";
                return RedirectToAction("Index", "PatientDashboard");
            }
        }

        /// <summary>
        /// GET: /Emergency/Track
        /// Tracks ambulance status and distance
        /// TODO: Get emergency request ID from TempData or route
        /// TODO: Call _emergencyService.GetEmergencyRequestAsync(requestId)
        /// TODO: Map to EmergencyTrackingViewModel
        /// TODO: Return View(viewModel)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Track(int? id)
        {
            try
            {
                // TODO: Implementation
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error tracking emergency");
                TempData["Error"] = "خطأ في تتبع الإسعاف";
                return RedirectToAction("Index", "PatientDashboard");
            }
        }
    }
}
