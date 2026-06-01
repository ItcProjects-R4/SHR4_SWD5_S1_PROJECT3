using Etmen_BLL.DTOs.CriticalIntelligence;

namespace Etmen_PL.Models.ViewModels.Crisis
{
    public class CrisisHeatmapViewModel
    {
        public int? CrisisId { get; set; }
        public int TotalGeoTaggedCriticalCases { get; set; }
        public List<CrisisHeatmapPointDto> Points { get; set; } = new();
        public List<CrisisHeatmapZoneDto> Zones { get; set; } = new();
    }
}
