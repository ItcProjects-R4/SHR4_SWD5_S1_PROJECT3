using System.ComponentModel.DataAnnotations;

namespace Etmen_PL.Models.ViewModels.Emergency
{
    public class EmergencyTrackingViewModel
    {
        public int RequestId { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal? PatientLatitude { get; set; }
        public decimal? PatientLongitude { get; set; }
        public decimal? AmbulanceLatitude { get; set; }
        public decimal? AmbulanceLongitude { get; set; }
        public string? ProviderName { get; set; }
        public string? AmbulanceDriverName { get; set; }
        public string? AmbulancePhoneNumber { get; set; }
        public int EstimatedArrivalMinutes { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? AcceptedAt { get; set; }
    }
}
