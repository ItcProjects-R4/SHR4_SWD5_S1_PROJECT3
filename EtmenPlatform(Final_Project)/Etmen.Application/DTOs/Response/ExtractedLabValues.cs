namespace Etmen.Application.DTOs.Response;

/// <summary>Response DTO returned by the ExtractedLabValues operation.</summary>
public sealed class ExtractedLabValues
{
    public double?   BloodSugar     { get; set; }
    public double?   HbA1c          { get; set; }
    public double?   Cholesterol    { get; set; }
    public double?   HDL            { get; set; }
    public double?   LDL            { get; set; }
    public double?   Triglycerides  { get; set; }
    public double?   Creatinine     { get; set; }
    public double?   Urea           { get; set; }
    public string?   LabName        { get; set; }
    public DateTime? LabDate        { get; set; }
}
