using Etmen_BLL.DTOs.Nearby;
using System.Collections.Generic;

namespace Etmen_PL.Models.ViewModels.Patient
{
    public class DoctorDetailsViewModel
    {
        public int ProviderId { get; set; }
        public string ProviderName { get; set; } = string.Empty;
        public string ProviderAddress { get; set; } = string.Empty;
        public string ProviderType { get; set; } = string.Empty;

        public int? DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public decimal ConsultationFee { get; set; }
        public int YearsOfExperience { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;

        public List<AvailableSlotDto> AvailableSlots { get; set; } = new();
    }
}
