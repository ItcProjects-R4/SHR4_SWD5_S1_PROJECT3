namespace Etmen.Application.Services;

/// <summary>
/// Contract for all outbound notifications: email, push, and in-app alerts.
/// Fires automatically when CreateMedicalRecordCommandHandler receives RiskLevel.High.
/// </summary>
public interface INotificationService
{
    Task SendAlertToPatientAsync(Guid patientId, AlertMessage message, CancellationToken ct = default);
    Task SendAlertToDoctorAsync(Guid doctorId,   AlertMessage message, CancellationToken ct = default);
    Task SendEmailAsync(string toEmail, string subject, string body, CancellationToken ct = default);
    Task MarkAlertAsReadAsync(Guid alertId, CancellationToken ct = default);
    Task SendRiskUpdatedAsync(Guid patientId, object riskAssessment, CancellationToken ct = default);
}

public sealed record AlertMessage(string Title, string Body, string? ActionUrl = null);
