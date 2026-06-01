using System.ComponentModel.DataAnnotations;

namespace Etmen_PL.Models.ViewModels.Patient
{
    public class MedicalRecordCreateViewModel
    {
        public int PatientId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime RecordDate { get; set; } = DateTime.Now;

        [Range(0, 300)]
        public decimal? SystolicBP { get; set; }

        [Range(0, 300)]
        public decimal? DiastolicBP { get; set; }

        [Range(0, 600)]
        public decimal? BloodSugar { get; set; }

        [Range(0, 300)]
        public decimal? HeartRate { get; set; }

        [Range(20, 45)]
        public decimal? Temperature { get; set; }

        [Range(0, 100)]
        public decimal? OxygenSaturation { get; set; }

        [StringLength(500)]
        public string? Symptoms { get; set; }

        [StringLength(1000)]
        public string? Diagnosis { get; set; }

        [StringLength(1000)]
        public string? Treatment { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        public List<string>? PrescribedMedications { get; set; }
    }
}
