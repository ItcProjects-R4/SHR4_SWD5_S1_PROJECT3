using Etmen.Domain.Entities;

namespace Etmen.Application.Services;

/// <summary>Contract for sending alerts via email, SMS, and push notifications.</summary>
public interface INotificationService
{
    Task SendAlertToPatientAsync(Guid patientId, string message, CancellationToken ct = default);
    Task SendAlertToDoctorAsync(Guid doctorId, string message, CancellationToken ct = default);
    Task SendEmailAsync(string toEmail, string subject, string body, CancellationToken ct = default);
    Task MarkAlertAsReadAsync(Guid alertId, CancellationToken ct = default);
    Task SendRiskUpdatedAsync(Guid patientId, RiskAssessment risk, CancellationToken ct = default);
    Task SendAppointmentConfirmationAsync(Appointment appointment, CancellationToken ct = default);
}
