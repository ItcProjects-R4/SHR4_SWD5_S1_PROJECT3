using Etmen_BLL.Repositories.IServices;
using Etmen_PL.Models.ViewModels.Doctor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen_PL.Controllers
{
    /// <summary>
    /// Doctor Profile Controller
    /// Renders and saves doctor clinical settings
    /// </summary>
    [Authorize(Roles = "Doctor")]
    public class DoctorProfileController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly ILogger<DoctorProfileController> _logger;

        public DoctorProfileController(
            IDoctorService doctorService,
            ILogger<DoctorProfileController> logger)
        {
            _doctorService = doctorService;
            _logger = logger;
        }

        /// <summary>
        /// GET: /DoctorProfile/Index
        /// Renders profile editor form
        /// TODO: Get current doctor user ID
        /// TODO: Call _doctorService.GetProfileAsync(doctorId)
        /// TODO: Map DoctorProfileDto to DoctorProfileViewModel
        /// TODO: Return View(viewModel)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                // TODO: Implementation
                var viewModel = new DoctorProfileViewModel();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading doctor profile");
                TempData["Error"] = "خطأ في تحميل الملف الشخصي";
                return RedirectToAction("Index", "DoctorDashboard");
            }
        }

        /// <summary>
        /// POST: /DoctorProfile/Index
        /// Updates availability, fees, specialization
        /// TODO: Validate ModelState
        /// TODO: Get current doctor user ID
        /// TODO: Call _doctorService.UpdateProfileAsync(doctorId, dto)
        /// TODO: Return View(viewModel) with updated data
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DoctorProfileViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            try
            {
                // TODO: Implementation
                _logger.LogInformation("Doctor profile updated");
                TempData["Success"] = "تم تحديث الملف الشخصي بنجاح";
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating doctor profile");
                ModelState.AddModelError(string.Empty, "خطأ في تحديث الملف الشخصي");
                return View(viewModel);
            }
        }
    }
}
