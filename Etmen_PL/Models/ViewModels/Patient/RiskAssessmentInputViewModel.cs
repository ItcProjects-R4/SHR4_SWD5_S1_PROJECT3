using System.ComponentModel.DataAnnotations;

namespace Etmen_PL.Models.ViewModels.Patient
{
    public class RiskAssessmentInputViewModel
    {
        [StringLength(1000)]
        public string? Symptoms { get; set; }

        [Range(0, 300)]
        public decimal? HeartRate { get; set; }

        [Range(0, 300)]
        public decimal? SystolicBP { get; set; }

        [Range(0, 300)]
        public decimal? DiastolicBP { get; set; }

        [Range(20, 45)]
        public decimal? Temperature { get; set; }

        [Range(0, 100)]
        public decimal? OxygenSaturation { get; set; }

        [Range(0, 600)]
        public decimal? BloodSugar { get; set; }

        [Range(-90, 90)]
        public decimal? Latitude { get; set; }

        [Range(-180, 180)]
        public decimal? Longitude { get; set; }
    }
}
