using Etmen_BLL.Repositories.IServices;
using Etmen_PL.Models.ViewModels.Patient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        private readonly ILogger<NearbyProvidersController> _logger;

        public NearbyProvidersController(
            INearbyService nearbyService,
            IAppointmentService appointmentService,
            ILogger<NearbyProvidersController> logger)
        {
            _nearbyService = nearbyService;
            _appointmentService = appointmentService;
            _logger = logger;
        }

        /// <summary>
        /// GET: /NearbyProviders/Index
        /// Renders GPS location map finder
        /// TODO: Return View with new NearbySearchViewModel
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
        /// TODO: Validate ModelState
        /// TODO: Call _nearbyService.SearchNearbyProvidersAsync(latitude, longitude, radius, type)
        /// TODO: Map results to NearbySearchViewModel.SearchResults
        /// TODO: Return View(viewModel)
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(NearbySearchViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            try
            {
                // TODO: Implementation
                _logger.LogInformation("Nearby providers search performed");
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
        /// POST: /NearbyProviders/Book
        /// Books a slot with a doctor
        /// TODO: Validate ModelState
        /// TODO: Get current user ID
        /// TODO: Call _appointmentService.BookAppointmentAsync(userId, dto)
        /// TODO: Redirect to appointment confirmation or Patient Appointments list
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(int providerId, int slotId)
        {
            try
            {
                // TODO: Get current user ID
                // TODO: Create BookingRequestDto
                // TODO: Call _appointmentService.BookAppointmentAsync(userId, dto)
                // TODO: Check result.IsSuccess

                _logger.LogInformation("Appointment booked");
                TempData["Success"] = "تم حجز الموعد بنجاح";
                return RedirectToAction("Index", "PatientAppointments");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error booking appointment");
                TempData["Error"] = "خطأ في حجز الموعد";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
