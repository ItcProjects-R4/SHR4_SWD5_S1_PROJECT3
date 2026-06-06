using Etmen_BLL.DTOs.Admin;
using Etmen_BLL.Helpers;

namespace Etmen_PL.Areas.Admin.Models
{
    public class AdminDashboardViewModel
    {
        public AdminDashboardDto Dashboard { get; set; } = new();
        public PaginatedResult<UserListItemDto> Users { get; set; } = new();
        public PaginatedResult<ProviderListItemDto> Providers { get; set; } = new();
        public PaginatedResult<AdminReportDto> Reports { get; set; } = new();
        public AdminCrisisDto Crisis { get; set; } = new();
        public List<ActivityLogDto> ActivityLogs { get; set; } = [];
        public SystemConfigDto SystemConfig { get; set; } = new();
        public CreateProviderDto NewProvider { get; set; } = new();
        public int UsersPage { get; set; } = 1;
        public int ProvidersPage { get; set; } = 1;
    }
}
