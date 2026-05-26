namespace Etmen_BLL.DTOs.CriticalIntelligence
{
    public class CrisisHeatmapZoneDto
    {
        public int ZoneId { get; set; }
        public string ZoneName { get; set; } = string.Empty;
        public decimal CenterLatitude { get; set; }
        public decimal CenterLongitude { get; set; }
        public decimal RadiusInKm { get; set; }
        public int RiskLevel { get; set; }
        public int CriticalCasesInside { get; set; }
    }
}
