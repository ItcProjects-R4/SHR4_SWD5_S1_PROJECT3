namespace Etmen.Application.DTOs.Internal;

/// <summary>Patient health context injected into the LLM system prompt.</summary>
public sealed class PatientContext
{
    public string PatientName    { get; set; } = string.Empty;
    public string RiskLevel      { get; set; } = string.Empty;
    public string RiskScore      { get; set; } = string.Empty;
    public string BloodPressure  { get; set; } = string.Empty;
    public double BloodSugar     { get; set; }
    public double BMI            { get; set; }
    public string? PrimaryIssue  { get; set; }
}
