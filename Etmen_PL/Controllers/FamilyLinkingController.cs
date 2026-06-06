using Etmen_BLL.Repositories.IServices;
using Etmen_PL.Models.ViewModels.Patient;
using Etmen_Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Etmen_PL.Controllers
{
    /// <summary>
    /// Family Linking Controller
    /// Invitation flows and viewer permission adjustments
    /// </summary>
    [Authorize(Roles = "Patient")]
    public class FamilyLinkingController : Controller
    {
        private readonly IFamilyService _familyService;
        private readonly IPatientService _patientService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<FamilyLinkingController> _logger;

        public FamilyLinkingController(
            IFamilyService familyService,
            IPatientService patientService,
            UserManager<ApplicationUser> userManager,
            ILogger<FamilyLinkingController> logger)
        {
            _familyService = familyService;
            _patientService = patientService;
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// GET: /FamilyLinking/Index
        /// Lists family links and status
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return RedirectToAction("Login", "Account");

                var profileResult = await _patientService.GetProfileAsync(userId);
                if (!profileResult.IsSuccess || profileResult.Data == null)
                {
                    TempData["Error"] = "فشل تحميل ملف المريض";
                    return RedirectToAction("Index", "PatientDashboard");
                }

                var familyResult = await _familyService.GetFamilyMembersAsync(profileResult.Data.Id);
                var familyMembers = familyResult.IsSuccess ? familyResult.Data : new List<Etmen_BLL.DTOs.Family.FamilyDto>();

                ViewBag.FamilyInvite = new FamilyInviteViewModel();
                return View(familyMembers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving family members");
                TempData["Error"] = "خطأ في تحميل أفراد الأسرة";
                return RedirectToAction("Index", "PatientDashboard");
            }
        }

        /// <summary>
        /// POST: /FamilyLinking/Invite
        /// Sends link invite
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Invite(FamilyInviteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "البيانات المدخلة غير صالحة";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return RedirectToAction("Login", "Account");

                // Find recipient user by email
                var recipientUser = await _userManager.FindByEmailAsync(viewModel.Email);
                if (recipientUser == null)
                {
                    TempData["Error"] = "هذا البريد الإلكتروني غير مسجل في النظام";
                    return RedirectToAction(nameof(Index));
                }

                // Get recipient's patient profile
                var recipientProfileResult = await _patientService.GetProfileAsync(recipientUser.Id);
                if (!recipientProfileResult.IsSuccess || recipientProfileResult.Data == null)
                {
                    TempData["Error"] = "المستخدم المدعو ليس لديه ملف طبي نشط";
                    return RedirectToAction(nameof(Index));
                }

                // Check that they aren't inviting themselves
                if (recipientUser.Id == userId)
                {
                    TempData["Error"] = "لا يمكنك إرسال دعوة ارتباط لنفسك";
                    return RedirectToAction(nameof(Index));
                }

                var inviteDto = new Etmen_BLL.DTOs.Family.FamilyInviteDto
                {
                    LinkedPatientId = recipientProfileResult.Data.Id,
                    Relationship = viewModel.Relationship,
                    CanViewRecords = viewModel.CanViewRecords,
                    CanViewRisk = viewModel.CanViewRisk,
                    CanBookAppointments = viewModel.CanBookAppointments
                };

                var result = await _familyService.InviteFamilyMemberAsync(inviteDto);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Family invite sent from patient of user {UserId} to user {RecipientId}", userId, recipientUser.Id);
                    TempData["Success"] = "تم إرسال دعوة الارتباط العائلي بنجاح";
                }
                else
                {
                    TempData["Error"] = result.ErrorMessage ?? "فشل إرسال الدعوة";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending family invite");
                TempData["Error"] = "خطأ في إرسال الدعوة";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// GET: /FamilyLinking/Accept
        /// Completes link from token parameter
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Accept(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                TempData["Error"] = "رابط الدعوة غير صالح";
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var result = await _familyService.AcceptFamilyInviteAsync(token);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Family invite accepted with token: {Token}", token);
                    TempData["Success"] = "تم قبول دعوة الارتباط العائلي بنجاح";
                    if (User.Identity?.IsAuthenticated == true)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return RedirectToAction("Login", "Account");
                    }
                }

                TempData["Error"] = result.ErrorMessage ?? "فشل قبول الدعوة. قد يكون الرابط منتهياً أو تم استخدامه بالفعل.";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error accepting family invite");
                TempData["Error"] = "خطأ في قبول الدعوة";
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// POST: /FamilyLinking/Remove
        /// Deletes family link
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var result = await _familyService.RemoveFamilyMemberAsync(id);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Family link {LinkId} removed", id);
                    TempData["Success"] = "تم حذف الرابط بنجاح";
                }
                else
                {
                    TempData["Error"] = result.ErrorMessage ?? "فشل حذف الارتباط العائلي";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing family member");
                TempData["Error"] = "خطأ في حذف الرابط";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// POST: /FamilyLinking/UpdatePermissions
        /// Adjusts record view settings
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePermissions(int id, FamilyInviteViewModel viewModel)
        {
            try
            {
                var dto = new Etmen_BLL.DTOs.Family.FamilyDto
                {
                    CanViewRecords = viewModel.CanViewRecords,
                    CanViewRisk = viewModel.CanViewRisk,
                    CanBookAppointments = viewModel.CanBookAppointments
                };

                var result = await _familyService.UpdateFamilyPermissionsAsync(id, dto);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Family permissions updated for link {LinkId}", id);
                    TempData["Success"] = "تم تحديث الصلاحيات بنجاح";
                }
                else
                {
                    TempData["Error"] = result.ErrorMessage ?? "فشل تحديث الصلاحيات";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating family permissions");
                TempData["Error"] = "خطأ في تحديث الصلاحيات";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
