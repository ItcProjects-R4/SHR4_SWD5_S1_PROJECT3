namespace Etmen.Infrastructure.Notifications;

/// <summary>
/// Push notification service (Firebase FCM or Azure Notification Hubs).
/// Complements EmailNotificationService for mobile/browser push alerts.
/// </summary>
public sealed class PushNotificationService
{
    // TODO: Inject IConfiguration and FCM/Azure client
    // TODO: Implement SendAsync(string deviceToken, string title, string body)
}
