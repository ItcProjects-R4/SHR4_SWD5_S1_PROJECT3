namespace Etmen_BLL.DTOs.CriticalIntelligence
{
    public class CrisisHeatmapPointDto
    {
        public int EmergencyRequestId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Intensity { get; set; }
        public string Label { get; set; } = string.Empty;
    }
}
