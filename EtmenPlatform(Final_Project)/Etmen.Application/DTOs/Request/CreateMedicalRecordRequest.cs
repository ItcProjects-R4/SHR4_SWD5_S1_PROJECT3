namespace Etmen.Application.DTOs.Request;

/// <summary>Request DTO for the CreateMedicalRecordRequest operation.</summary>
public sealed class CreateMedicalRecordRequest
{
    public Guid   PatientId              { get; set; }
    public double BloodPressureSystolic  { get; set; }
    public double BloodPressureDiastolic { get; set; }
    public double BloodSugar             { get; set; }
    public double BMI                    { get; set; }
    public string? Symptoms              { get; set; }
    public int    ActivityLevel          { get; set; } // 0–4
}
