namespace Etmen_BLL.DTOs.CriticalIntelligence
{
    public class RiskContributionDto
    {
        public string Factor { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public int ImpactPercent { get; set; }
        public string Severity { get; set; } = string.Empty;
        public string Explanation { get; set; } = string.Empty;
    }
}
