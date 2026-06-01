using Etmen_BLL.Repositories.IServices;
using Etmen_PL.Models.ViewModels.Patient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen_PL.Controllers
{
    /// <summary>
    /// Patient Dashboard Controller
    /// Displays patient landing dashboard metrics
    /// </summary>
    [Authorize(Roles = "Patient")]
    public class PatientDashboardController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<PatientDashboardController> _logger;

        public PatientDashboardController(
            IPatientService patientService,
            ILogger<PatientDashboardController> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        /// <summary>
        /// GET: /PatientDashboard/Index
        /// Renders patient home dashboard panels
        /// TODO: Implement dashboard retrieval using _patientService.GetDashboardAsync()
        /// TODO: Handle ServiceResult error checking
        /// TODO: Return PatientDashboardViewModel with dashboard data
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                // TODO: Get current user ID from User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                // TODO: Call _patientService.GetDashboardAsync(userId)
                // TODO: Check if result.IsSuccess
                // TODO: Map result.Data to PatientDashboardViewModel
                // TODO: Return View(viewModel)

                _logger.LogInformation("PatientDashboard Index accessed");
                var viewModel = new PatientDashboardViewModel();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving patient dashboard");
                TempData["Error"] = "حدث خطأ أثناء تحميل لوحة التحكم";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
