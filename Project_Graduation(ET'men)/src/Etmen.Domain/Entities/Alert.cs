using Etmen.Domain.Common;
using Etmen.Domain.Enums;

namespace Etmen.Domain.Entities;

/// <summary>
/// Notification record created when a RiskAssessment returns RiskLevel.High.
/// Sent to both the patient and their assigned doctor.
/// </summary>
public class Alert : BaseEntity
{
    public Guid        PatientId   { get; private set; }
    public Guid?       DoctorId    { get; private set; }
    public string      Title       { get; private set; } = default!;
    public string      Message     { get; private set; } = default!;
    public AlertStatus Status      { get; private set; } = AlertStatus.Unread;

    private Alert() { }

    public static Alert Create(Guid patientId, Guid? doctorId, string title, string message)
    {
        throw new NotImplementedException();
    }

    public void MarkAsRead()    { throw new NotImplementedException(); }
    public void Dismiss()       { throw new NotImplementedException(); }
}
