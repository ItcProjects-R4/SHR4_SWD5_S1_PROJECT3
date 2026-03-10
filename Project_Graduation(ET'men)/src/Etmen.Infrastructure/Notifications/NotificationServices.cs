using Etmen.Application.Services;
using Etmen.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;

namespace Etmen.Infrastructure.Notifications;

// ════════════════════════════════════════════════════════════════════════════
// EmailNotificationService — implements INotificationService
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Sends email alerts via SMTP and persists Alert entities in the database.
/// Configured via appSettings: SmtpSettings:Host, Port, User.
/// </summary>
public class EmailNotificationService : INotificationService
{
    private readonly IConfiguration _config;
    private readonly Application.Interfaces.IAlertRepository _alertRepo;
    private readonly Application.Interfaces.IUserRepository _userRepo;

    public EmailNotificationService(
        IConfiguration config,
        Application.Interfaces.IAlertRepository alertRepo,
        Application.Interfaces.IUserRepository userRepo)
    {
        _config = config;
        _alertRepo = alertRepo;
        _userRepo = userRepo;
    }

    /// <summary>Creates an Alert entity for the patient and sends an SMTP email.</summary>
    public Task SendAlertToPatientAsync(Guid patientId, AlertMessage message, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>Creates an Alert entity for the doctor and sends an SMTP email.</summary>
    public Task SendAlertToDoctorAsync(Guid doctorId, AlertMessage message, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>Sends a plain SMTP email (used for invite links, password resets, etc.).</summary>
    public Task SendEmailAsync(string toEmail, string subject, string body, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task MarkAlertAsReadAsync(Guid alertId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// PushNotificationService — alternative/complementary push channel
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Sends mobile push notifications (Firebase Cloud Messaging or similar).
/// Can be used alongside EmailNotificationService for high-risk alerts.
/// </summary>
public class PushNotificationService : INotificationService
{
    private readonly IConfiguration _config;
    public PushNotificationService(IConfiguration config) => _config = config;

    public Task SendAlertToPatientAsync(Guid patientId, AlertMessage message, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task SendAlertToDoctorAsync(Guid doctorId, AlertMessage message, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task SendEmailAsync(string toEmail, string subject, string body, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task MarkAlertAsReadAsync(Guid alertId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
