using Etmen_BLL.Repositories.IServices;
using Etmen_PL.Models.ViewModels.Emergency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen_PL.Controllers
{
    /// <summary>
    /// Doctor Panic Inbox Controller
    /// Handles urgent patient alerts and case assignment
    /// </summary>
    [Authorize(Roles = "Doctor")]
    public class DoctorPanicInboxController : Controller
    {
        private readonly ICriticalIntelligenceService _criticalIntelligenceService;
        private readonly ILogger<DoctorPanicInboxController> _logger;

        public DoctorPanicInboxController(
            ICriticalIntelligenceService criticalIntelligenceService,
            ILogger<DoctorPanicInboxController> logger)
        {
            _criticalIntelligenceService = criticalIntelligenceService;
            _logger = logger;
        }

        /// <summary>
        /// GET: /DoctorPanicInbox/Index
        /// Lists assigned and unassigned critical cases
        /// TODO: Get current doctor user ID
        /// TODO: Call _criticalIntelligenceService.GetDoctorPanicInboxAsync(doctorId)
        /// TODO: Return View(viewModel)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                // TODO: Implementation
                var viewModel = new DoctorPanicInboxViewModel();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving panic inbox");
                TempData["Error"] = "خطأ في تحميل قائمة الحالات الحرجة";
                return RedirectToAction("Index", "DoctorDashboard");
            }
        }

        /// <summary>
        /// POST: /DoctorPanicInbox/Claim
        /// Assigns a critical care request to the doctor
        /// TODO: Validate id parameter
        /// TODO: Get current doctor user ID
        /// TODO: Call _criticalIntelligenceService.AssignBestDoctorAsync(caseId)
        /// TODO: Redirect to Index on success
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Claim(int id)
        {
            try
            {
                // TODO: Implementation
                _logger.LogInformation("Critical case claimed");
                TempData["Success"] = "تم تحمل الحالة بنجاح";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error claiming case");
                TempData["Error"] = "خطأ في تحمل الحالة";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
