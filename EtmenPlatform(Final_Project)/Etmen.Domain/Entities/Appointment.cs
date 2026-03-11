using Etmen.Domain.Common;

namespace Etmen.Domain.Entities;

/// <summary>
/// A confirmed booking linking a patient to a healthcare provider.
/// Created via BookAppointmentCommand; linked to the RiskAssessment that prompted it.
/// </summary>
public class Appointment : BaseEntity
{
    public Guid      PatientId      { get; private set; }
    public Guid      ProviderId     { get; private set; }
    public Guid?     AssessmentId   { get; private set; }
    public DateTime  AppointmentAt  { get; private set; }
    public int       Status         { get; private set; } = 0; // 0=Pending, 1=Confirmed, 2=Cancelled, 3=Completed
    public bool      IsEmergency    { get; private set; } = false;
    public string?   Notes          { get; private set; }

    protected Appointment() { }

    public static Appointment Create(Guid patientId, Guid providerId, Guid? assessmentId,
        DateTime appointmentAt, bool isEmergency = false, string? notes = null)
        => new()
        {
            PatientId = patientId, ProviderId = providerId, AssessmentId = assessmentId,
            AppointmentAt = appointmentAt, IsEmergency = isEmergency, Notes = notes
        };

    public void Confirm()   { Status = 1; MarkUpdated(); }
    public void Cancel()    { Status = 2; MarkUpdated(); }
    public void Complete()  { Status = 3; MarkUpdated(); }
}
