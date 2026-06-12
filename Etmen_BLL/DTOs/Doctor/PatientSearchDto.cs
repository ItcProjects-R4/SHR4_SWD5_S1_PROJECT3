namespace Etmen_BLL.DTOs.Doctor
{
    public class PatientSearchDto
    {
        public int PatientId { get; set; }
        public string? PatientName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? SearchTerm { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? FilterBy { get; set; } // "Name", "Phone", "LastVisit"
    }
}