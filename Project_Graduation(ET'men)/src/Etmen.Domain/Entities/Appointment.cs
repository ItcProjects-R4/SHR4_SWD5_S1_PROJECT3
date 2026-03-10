using Etmen.Domain.Common;
using Etmen.Domain.Enums;

namespace Etmen.Domain.Entities;

/// <summary>
/// Confirmed booking created by a patient after the Nearby Provider feature suggests a provider.
/// Links the booking back to the RiskAssessment that prompted it.
/// </summary>
public class Appointment : BaseEntity
{
    public Guid              PatientId      { get; private set; }
    public Guid              ProviderId     { get; private set; }
    public Guid              AssessmentId   { get; private set; }
    public DateTime          AppointmentAt  { get; private set; }
    public AppointmentStatus Status         { get; private set; } = AppointmentStatus.Pending;
    public bool              IsEmergency    { get; private set; }
    public string?           Notes          { get; private set; }

    private Appointment() { }

    public static Appointment Create(Guid patientId, Guid providerId,
                                      Guid assessmentId, DateTime appointmentAt,
                                      bool isEmergency = false, string? notes = null)
    {
        throw new NotImplementedException();
    }

    public void Confirm()  { throw new NotImplementedException(); }
    public void Cancel()   { throw new NotImplementedException(); }
    public void Complete() { throw new NotImplementedException(); }
}
