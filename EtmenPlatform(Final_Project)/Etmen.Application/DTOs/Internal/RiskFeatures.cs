namespace Etmen.Application.DTOs.Internal;

/// <summary>Feature vector passed to the AI risk model for scoring.</summary>
public sealed class RiskFeatures
{
    public float Age                    { get; set; }
    public float BMI                    { get; set; }
    public float BloodPressureSystolic  { get; set; }
    public float BloodPressureDiastolic { get; set; }
    public float BloodSugar             { get; set; }
    public float FamilyHistory          { get; set; } // 0 or 1
    public float IsSmoker               { get; set; } // 0 or 1
    public float ActivityLevel          { get; set; } // 0–4
}
