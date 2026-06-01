using System.ComponentModel.DataAnnotations;

namespace Etmen_PL.Models.ViewModels.Patient
{
    public class FamilyInviteViewModel
    {
        [Required]
        public int LinkedPatientId { get; set; }

        [Required(ErrorMessage = "نوع العلاقة مطلوب")]
        [StringLength(50)]
        public string Relationship { get; set; } = string.Empty;

        public bool CanViewRecords { get; set; }
        public bool CanViewRisk { get; set; }
        public bool CanBookAppointments { get; set; }
    }
}
