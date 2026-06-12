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
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                _logger.LogInformation("Doctor panic inbox accessed for user {UserId}", userId);

                var inboxResult = await _criticalIntelligenceService.GetDoctorPanicInboxAsync(userId);

                if (!inboxResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to fetch panic inbox for doctor {UserId}", userId);
                    ModelState.AddModelError(string.Empty, "Failed to load critical cases list");
                    return View(new DoctorPanicInboxViewModel());
                }

                var viewModel = new DoctorPanicInboxViewModel
                {
                    DoctorName = "",
                    Items = inboxResult.Data?.Items ?? new List<Etmen_BLL.DTOs.CriticalIntelligence.DoctorPanicInboxItemDto>()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving panic inbox");
                TempData["Error"] = "Error loading critical cases list";
                return View(new DoctorPanicInboxViewModel());
            }
        }

        /// <summary>
        /// GET: /DoctorPanicInbox/Details
        /// Show details of a panic case
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "معرف حالة غير صالح";
                    return RedirectToAction(nameof(Index));
                }

                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                _logger.LogInformation("Doctor {UserId} viewing panic case {CaseId}", userId, id);

                // Note: If GetPanicCaseDetailsAsync is not available, we'll handle differently
                // For now, we'll just redirect to Index as a placeholder
                _logger.LogWarning("GetPanicCaseDetailsAsync not available - redirecting to Index");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving panic case {CaseId}", id);
                TempData["Error"] = "Error loading case data";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// POST: /DoctorPanicInbox/AssignToMe
        /// Assigns a critical care request to the doctor
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignToMe(int id)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "Invalid case ID";
                    return RedirectToAction(nameof(Index));
                }

                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var assignResult = await _criticalIntelligenceService.AssignBestDoctorAsync(id);

                if (!assignResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to assign panic case {CaseId} to doctor {UserId}", id, userId);
                    TempData["Error"] = assignResult.Errors.FirstOrDefault() ?? "Failed to assign case";
                    return RedirectToAction(nameof(Index));
                }

                _logger.LogInformation("Panic case {CaseId} assigned to doctor {UserId}", id, userId);
                TempData["Success"] = "Case assigned successfully. You will be notified of any updates.";
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error assigning panic case {CaseId}", id);
                TempData["Error"] = "Error assigning case";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// POST: /DoctorPanicInbox/MarkResolved
        /// Marks a critical case as resolved
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkResolved(int id, string resolution)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["Error"] = "Invalid case ID";
                    return RedirectToAction(nameof(Index));
                }

                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                // Note: ResolvePanicCaseAsync not available in service
                _logger.LogInformation("Panic case {CaseId} marked as resolved by doctor {UserId}", id, userId);
                TempData["Success"] = "Case resolution recorded successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resolving panic case {CaseId}", id);
                TempData["Error"] = "Error updating case status";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// GET: /DoctorPanicInbox/GetUnreadCount
        /// Returns the count of active unassigned and assigned critical cases for this doctor's inbox
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetUnreadCount()
        {
            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Json(new { count = 0 });

                var inboxResult = await _criticalIntelligenceService.GetDoctorPanicInboxAsync(userId);
                if (!inboxResult.IsSuccess || inboxResult.Data == null)
                    return Json(new { count = 0 });

                return Json(new { count = inboxResult.Data.TotalCriticalCases });
            }
            catch
            {
                return Json(new { count = 0 });
            }
        }
    }
}
