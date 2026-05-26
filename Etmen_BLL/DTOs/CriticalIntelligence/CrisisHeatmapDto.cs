namespace Etmen_BLL.DTOs.CriticalIntelligence
{
    public class CrisisHeatmapDto
    {
        public int? CrisisId { get; set; }
        public DateTime GeneratedAt { get; set; }
        public int TotalGeoTaggedCriticalCases { get; set; }
        public List<CrisisHeatmapPointDto> Points { get; set; } = new();
        public List<CrisisHeatmapZoneDto> Zones { get; set; } = new();
    }
}
