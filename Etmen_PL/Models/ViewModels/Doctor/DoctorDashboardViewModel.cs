using Etmen_BLL.DTOs.Doctor;

namespace Etmen_PL.Models.ViewModels.Doctor
{
    public class DoctorDashboardViewModel
    {
        public string DoctorName { get; set; } = string.Empty;
        public string? Specialization { get; set; }
        public int TodayAppointmentsCount { get; set; }
        public int PendingAppointmentsCount { get; set; }
        public int TotalPatientsCount { get; set; }
        public decimal? AverageRating { get; set; }
        public List<UpcomingAppointmentDto> UpcomingAppointments { get; set; } = new();
        public List<RecentPatientDto> RecentPatients { get; set; } = new();
        public DoctorStatisticsDto? Statistics { get; set; }
    }
}
