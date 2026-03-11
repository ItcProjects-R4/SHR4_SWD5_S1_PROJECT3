namespace Etmen.Application.DTOs.Request;

/// <summary>Request DTO for the UpdatePatientProfileRequest operation.</summary>
public sealed class UpdatePatientProfileRequest
{
    public double Height         { get; set; }
    public double Weight         { get; set; }
    public bool   IsSmoker       { get; set; }
    public int    ActivityLevel  { get; set; }
    public bool   FamilyHistory  { get; set; }
}
