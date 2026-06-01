using System.ComponentModel.DataAnnotations;

namespace Etmen_PL.Models.ViewModels.Doctor
{
    public class PatientSearchViewModel
    {
        [StringLength(200)]
        public string? SearchTerm { get; set; }

        public List<PatientSearchResultViewModel> SearchResults { get; set; } = new();
    }

    public class PatientSearchResultViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? BloodType { get; set; }
        public bool HasChronicDiseases { get; set; }
    }
}
