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
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                _logger.LogInformation("Doctor dashboard accessed for user {UserId}", userId);

                var dashboardResult = await _doctorService.GetDashboardAsync(userId);

                if (!dashboardResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to fetch dashboard data for doctor {UserId}", userId);
                    ModelState.AddModelError(string.Empty, "Failed to load dashboard. Please try again later.");
                    var emptyViewModel = new DoctorDashboardViewModel();
                    return View(emptyViewModel);
                }

                var viewModel = new DoctorDashboardViewModel
                {
                    DoctorName = dashboardResult.Data?.DoctorName ?? "",
                    Specialization = dashboardResult.Data?.Specialization,
                    TodayAppointmentsCount = dashboardResult.Data?.TodayAppointmentsCount ?? 0,
                    PendingAppointmentsCount = dashboardResult.Data?.PendingAppointmentsCount ?? 0,
                    TotalPatientsCount = dashboardResult.Data?.TotalPatientsCount ?? 0,
                    AverageRating = dashboardResult.Data?.AverageRating,
                    UpcomingAppointments = dashboardResult.Data?.UpcomingAppointments ?? new List<Etmen_BLL.DTOs.Doctor.UpcomingAppointmentDto>(),
                    RecentPatients = dashboardResult.Data?.RecentPatients ?? new List<Etmen_BLL.DTOs.Doctor.RecentPatientDto>()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving doctor dashboard");
                TempData["Error"] = "Error loading dashboard";
                return View(new DoctorDashboardViewModel());
            }
        }

        /// <summary>
        /// GET: /DoctorDashboard/Statistics
        /// Returns dashboard statistics as JSON for AJAX/widget updates
        /// </summary>
        [HttpGet]
        [Route("DoctorDashboard/Statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var statsResult = await _doctorService.GetStatisticsAsync(userId);

                if (!statsResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to fetch statistics for doctor {UserId}", userId);
                    return Json(new { isSuccess = false, message = "Failed to load statistics" });
                }

                return Json(new
                {
                    isSuccess = true,
                    data = statsResult.Data
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching statistics");
                return Json(new { isSuccess = false, message = "An error occurred while fetching statistics" });
            }
        }
    }
}
