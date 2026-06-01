using Etmen_BLL.Repositories.IServices;
using Etmen_PL.Models.ViewModels.Patient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen_PL.Controllers
{
    /// <summary>
    /// Patient Profile Controller
    /// Renders and saves patient baseline medical metrics
    /// </summary>
    [Authorize(Roles = "Patient")]
    public class PatientProfileController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<PatientProfileController> _logger;

        public PatientProfileController(
            IPatientService patientService,
            ILogger<PatientProfileController> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        /// <summary>
        /// GET: /PatientProfile/Index
        /// Renders patient profile editing page
        /// TODO: Get current user ID
        /// TODO: Call _patientService.GetProfileAsync(userId)
        /// TODO: Map ProfileDto to PatientProfileViewModel
        /// TODO: Return View(viewModel)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                // TODO: Implementation
                var viewModel = new PatientProfileViewModel();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading patient profile");
                TempData["Error"] = "خطأ في تحميل الملف الشخصي";
                return RedirectToAction("Index", "PatientDashboard");
            }
        }

        /// <summary>
        /// POST: /PatientProfile/Index
        /// Updates patient metrics (weight, allergies, etc.)
        /// TODO: Validate ModelState
        /// TODO: Get current user ID
        /// TODO: Map PatientProfileViewModel to ProfileDto
        /// TODO: Call _patientService.UpdateProfileAsync(userId, dto)
        /// TODO: Check result.IsSuccess and redirect or return error
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(PatientProfileViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            try
            {
                // TODO: Implementation
                _logger.LogInformation("Patient profile updated");
                TempData["Success"] = "تم تحديث الملف الشخصي بنجاح";
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating patient profile");
                ModelState.AddModelError(string.Empty, "خطأ في تحديث الملف الشخصي");
                return View(viewModel);
            }
        }
    }
}
