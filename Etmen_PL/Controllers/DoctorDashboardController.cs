using Etmen_BLL.Repositories.IServices;
using Etmen_PL.Models.ViewModels.Doctor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen_PL.Controllers
{
    /// <summary>
    /// Doctor Dashboard Controller
    /// Displays doctor landing dashboard metrics and scheduling statistics
    /// </summary>
    [Authorize(Roles = "Doctor")]
    public class DoctorDashboardController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly ILogger<DoctorDashboardController> _logger;

        public DoctorDashboardController(
            IDoctorService doctorService,
            ILogger<DoctorDashboardController> logger)
        {
            _doctorService = doctorService;
            _logger = logger;
        }

        /// <summary>
        /// GET: /DoctorDashboard/Index
        /// Displays schedule summary and statistics charts
        /// TODO: Get current doctor user ID
        /// TODO: Call _doctorService.GetDashboardAsync(doctorId)
        /// TODO: Map DoctorDashboardDto to DoctorDashboardViewModel
        /// TODO: Return View(viewModel)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                // TODO: Implementation
                _logger.LogInformation("Doctor dashboard accessed");
                var viewModel = new DoctorDashboardViewModel();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving doctor dashboard");
                TempData["Error"] = "خطأ في تحميل لوحة التحكم";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
