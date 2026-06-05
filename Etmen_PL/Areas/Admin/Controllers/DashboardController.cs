using Etmen_BLL.DTOs.Admin;
using Etmen_BLL.Repositories.IServices;
using Etmen_PL.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen_PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,CrisisAdmin")]
    public class DashboardController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(IAdminService adminService, ILogger<DashboardController> logger)
        {
            _adminService = adminService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int usersPage = 1, int providersPage = 1)
        {
            var vm = new AdminDashboardViewModel
            {
                UsersPage = usersPage,
                ProvidersPage = providersPage
            };

            var dashboard = await _adminService.GetDashboardAsync();
            var users = await _adminService.GetAllUsersAsync(usersPage, 8);
            var providers = await _adminService.GetAllProvidersAsync(providersPage, 6);
            var reports = await _adminService.GetReportsAsync(1, 5);
            var crisis = await _adminService.GetCrisisManagementAsync();
            var logs = await _adminService.GetActivityLogsAsync(1, 8);
            var config = await _adminService.GetSystemConfigAsync();

            if (dashboard.IsSuccess && dashboard.Data is not null) vm.Dashboard = dashboard.Data;
            if (users.IsSuccess && users.Data is not null) vm.Users = users.Data;
            if (providers.IsSuccess && providers.Data is not null) vm.Providers = providers.Data;
            if (reports.IsSuccess && reports.Data is not null) vm.Reports = reports.Data;
            if (crisis.IsSuccess && crisis.Data is not null) vm.Crisis = crisis.Data;
            if (logs.IsSuccess && logs.Data is not null) vm.ActivityLogs = logs.Data;
            if (config.IsSuccess && config.Data is not null) vm.SystemConfig = config.Data;

            AddFirstError(dashboard.ErrorMessage, users.ErrorMessage, providers.ErrorMessage, reports.ErrorMessage, crisis.ErrorMessage, logs.ErrorMessage, config.ErrorMessage);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleUserStatus(string userId, bool isActive)
        {
            var result = await _adminService.UpdateUserStatusAsync(0, new UpdateUserStatusDto { UserId = userId, IsActive = isActive });
            Flash(result, isActive ? "تم تفعيل المستخدم بنجاح." : "تم تعطيل المستخدم بنجاح.");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BulkUsers(BulkUserActionDto dto)
        {
            var result = await _adminService.BulkUserActionAsync(dto);
            Flash(result, "تم تنفيذ الإجراء الجماعي بنجاح.");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProvider(CreateProviderDto dto)
        {
            var result = await _adminService.CreateProviderAsync(dto);
            Flash(result, "تم إضافة مقدم الخدمة بنجاح.");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProvider(UpdateProviderDto dto)
        {
            var result = await _adminService.UpdateProviderAsync(dto.Id, dto);
            Flash(result, "تم تحديث بيانات مقدم الخدمة بنجاح.");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            var result = await _adminService.DeleteProviderAsync(id);
            Flash(result, "تم تعطيل مقدم الخدمة بنجاح.");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveCrisis(int id)
        {
            var result = await _adminService.ApproveCrisisAsync(id);
            Flash(result, "تم تفعيل وضع الأزمة بنجاح.");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseCrisis(int id, string reason = "إغلاق من لوحة الإدارة")
        {
            var result = await _adminService.RejectCrisisAsync(id, reason);
            Flash(result, "تم إغلاق الأزمة بنجاح.");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateConfig(SystemConfigDto dto)
        {
            var result = await _adminService.UpdateSystemConfigAsync(dto);
            Flash(result, "تم حفظ إعدادات النظام بنجاح.");
            return RedirectToAction(nameof(Index));
        }

        private void Flash(Etmen_BLL.Helpers.ServiceResult result, string successMessage)
        {
            if (result.IsSuccess)
                TempData["Success"] = successMessage;
            else
                TempData["Error"] = result.ErrorMessage ?? "تعذر تنفيذ العملية.";
        }

        private void AddFirstError(params string?[] errors)
        {
            var error = errors.FirstOrDefault(e => !string.IsNullOrWhiteSpace(e));
            if (!string.IsNullOrWhiteSpace(error))
            {
                TempData["Error"] = error;
                _logger.LogWarning("Admin dashboard loaded with service warning: {Error}", error);
            }
        }
    }
}
