using Etmen_BLL.DTOs.CriticalIntelligence;

namespace Etmen_PL.Models.ViewModels.Crisis
{
    public class CrisisCommandCenterViewModel
    {
        public int ActiveCriticalCases { get; set; }
        public int WaitingForHospital { get; set; }
        public int HospitalAccepted { get; set; }
        public int WaitingForDoctor { get; set; }
        public int DoctorAssigned { get; set; }
        public decimal AverageWaitingMinutes { get; set; }
        public List<CriticalCommandCenterItemDto> Cases { get; set; } = new();
    }
}
