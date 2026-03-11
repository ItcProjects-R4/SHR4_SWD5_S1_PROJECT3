namespace Etmen.Application.DTOs.Request;

/// <summary>Request DTO for the BookAppointmentRequest operation.</summary>
public sealed class BookAppointmentRequest
{
    public Guid     PatientId     { get; set; }
    public Guid     ProviderId    { get; set; }
    public Guid?    AssessmentId  { get; set; }
    public Guid     SlotId        { get; set; }
    public bool     IsEmergency   { get; set; } = false;
    public string?  Notes         { get; set; }
}
