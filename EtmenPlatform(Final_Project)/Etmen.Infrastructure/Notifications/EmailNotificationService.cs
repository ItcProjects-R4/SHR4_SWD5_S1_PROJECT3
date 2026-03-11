using Etmen.Application.Services;
using Etmen.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Etmen.Infrastructure.Notifications;

/// <summary>
/// Implements INotificationService.
/// Email: SmtpClient (TLS port 587), configure via appsettings SmtpSettings section.
/// SMS:   Vonage SDK, configure via appsettings Sms section.
/// Triggered on: High-risk alert, lab score change > 5%, appointment confirmation.
/// </summary>
public sealed class EmailNotificationService : INotificationService
{
    private readonly IConfiguration _config;

    public EmailNotificationService(IConfiguration config) => _config = config;

    public Task SendAlertToPatientAsync(Guid patientId, string message, CancellationToken ct = default)
        => throw new NotImplementedException();

    public Task SendAlertToDoctorAsync(Guid doctorId, string message, CancellationToken ct = default)
        => throw new NotImplementedException();

    public Task SendEmailAsync(string toEmail, string subject, string body, CancellationToken ct = default)
        => throw new NotImplementedException();

    public Task MarkAlertAsReadAsync(Guid alertId, CancellationToken ct = default)
        => throw new NotImplementedException();

    public Task SendRiskUpdatedAsync(Guid patientId, RiskAssessment risk, CancellationToken ct = default)
        => throw new NotImplementedException();

    public Task SendAppointmentConfirmationAsync(Appointment appointment, CancellationToken ct = default)
        => throw new NotImplementedException();
}
