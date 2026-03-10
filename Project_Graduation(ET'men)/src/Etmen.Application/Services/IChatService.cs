using Etmen.Application.UseCases.Chat;
using Etmen.Domain.Entities;

namespace Etmen.Application.Services;

/// <summary>
/// Service contract for the Doctor–Patient internal chat feature.
/// Separate from ILlmPatientChatService (AI assistant) — different channel entirely.
/// </summary>
public interface IChatService
{
    Task<IEnumerable<ChatMessage>>       GetConversationAsync(Guid patientId, Guid doctorId, CancellationToken ct = default);
    Task<ChatMessage>                    SendMessageAsync(SendMessageCommand cmd, CancellationToken ct = default);
    Task                                 MarkAsReadAsync(Guid conversationPatientId, Guid readerId, CancellationToken ct = default);
    Task<IEnumerable<ConversationSummary>> GetDoctorConversationsAsync(Guid doctorId, CancellationToken ct = default);
}

public sealed record ConversationSummary(
    Guid PatientId, string PatientName, string RiskLevel,
    string LastMessage, int UnreadCount);
