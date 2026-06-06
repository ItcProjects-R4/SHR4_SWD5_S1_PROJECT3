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
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var profileResult = await _doctorService.GetProfileAsync(userId);

                if (!profileResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to fetch profile for doctor {UserId}", userId);
                    ModelState.AddModelError(string.Empty, "Failed to load profile");
                    return View(new DoctorProfileViewModel());
                }

                var viewModel = new DoctorProfileViewModel
                {
                    FullName = profileResult.Data?.FullName ?? string.Empty,
                    Specialization = profileResult.Data?.Specialization,
                    LicenseNumber = profileResult.Data?.LicenseNumber,
                    YearsOfExperience = profileResult.Data?.YearsOfExperience,
                    Bio = profileResult.Data?.Bio,
                    ConsultationFee = profileResult.Data?.ConsultationFee,
                    IsAvailable = profileResult.Data?.IsAvailable ?? true
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading doctor profile");
                TempData["Error"] = "Error loading profile";
                return View(new DoctorProfileViewModel());
            }
        }

        /// <summary>
        /// POST: /DoctorProfile/Index
        /// Updates availability, fees, specialization
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DoctorProfileViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var profileDto = new Etmen_BLL.DTOs.Doctor.DoctorProfileDto
                {
                    FullName = viewModel.FullName,
                    Specialization = viewModel.Specialization,
                    LicenseNumber = viewModel.LicenseNumber,
                    YearsOfExperience = viewModel.YearsOfExperience,
                    Bio = viewModel.Bio,
                    ConsultationFee = viewModel.ConsultationFee,
                    IsAvailable = viewModel.IsAvailable
                };

                var updateResult = await _doctorService.UpdateProfileAsync(userId, profileDto);

                if (!updateResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to update profile for doctor {UserId}", userId);
                    foreach (var error in updateResult.Errors)
                        ModelState.AddModelError(string.Empty, error);
                    return View(viewModel);
                }

                _logger.LogInformation("Doctor profile updated successfully for user {UserId}", userId);
                TempData["Success"] = "Profile updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating doctor profile");
                ModelState.AddModelError(string.Empty, "Error updating profile");
                return View(viewModel);
            }
        }
    }
}
