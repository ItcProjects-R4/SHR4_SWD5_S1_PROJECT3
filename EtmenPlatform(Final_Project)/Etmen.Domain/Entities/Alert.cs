using Etmen.Domain.Common;
using Etmen.Domain.Enums;

namespace Etmen.Domain.Entities;

/// <summary>
/// System alert fired when the AI detects a High-risk score.
/// Sent to both the patient and their assigned doctor.
/// </summary>
public class Alert : BaseEntity
{
    public Guid        PatientId   { get; private set; }
    public Guid?       DoctorId    { get; private set; }
    public string      Message     { get; private set; } = string.Empty;
    public AlertStatus Status      { get; private set; } = AlertStatus.Unread;
    public RiskLevel   RiskLevel   { get; private set; }

    protected Alert() { }

    public static Alert Create(Guid patientId, Guid? doctorId, string message, RiskLevel riskLevel)
        => new() { PatientId = patientId, DoctorId = doctorId, Message = message, RiskLevel = riskLevel };

    public void MarkAsRead()      { Status = AlertStatus.Read;      MarkUpdated(); }
    public void Dismiss()         { Status = AlertStatus.Dismissed; MarkUpdated(); }
}
