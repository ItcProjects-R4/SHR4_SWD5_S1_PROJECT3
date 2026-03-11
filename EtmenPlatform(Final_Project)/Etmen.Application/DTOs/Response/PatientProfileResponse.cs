namespace Etmen.Application.DTOs.Response;

/// <summary>Response DTO returned by the PatientProfileResponse operation.</summary>
public sealed class PatientProfileResponse
{
    public Guid   PatientId       { get; set; }
    public string FullName        { get; set; } = string.Empty;
    public int    Age             { get; set; }
    public double BMI             { get; set; }
    public bool   IsSmoker        { get; set; }
    public double LatestRiskScore { get; set; }
    public string RiskLevel       { get; set; } = string.Empty;
}
