using System.ComponentModel.DataAnnotations;

namespace Etmen_PL.Models.ViewModels.Patient
{
    public class EmergencyRequestViewModel
    {
        public int PatientProfileId { get; set; }

        [Required]
        [Range(-90, 90)]
        public decimal Latitude { get; set; }

        [Required]
        [Range(-180, 180)]
        public decimal Longitude { get; set; }

        [StringLength(100)]
        public string? EmergencyType { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }
}
