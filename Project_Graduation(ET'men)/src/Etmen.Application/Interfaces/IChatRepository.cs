using Etmen.Domain.Entities;

namespace Etmen.Application.Interfaces;

/// <summary>Repository contract for ChatMessage persistence and conversation queries.</summary>
public interface IChatRepository
{
    Task<ChatMessage> SaveMessageAsync(ChatMessage message, CancellationToken ct = default);
    Task<IEnumerable<ChatMessage>> GetConversationAsync(Guid patientId, Guid doctorId, CancellationToken ct = default);
    Task<IEnumerable<object>> GetAllConversationsForDoctorAsync(Guid doctorId, CancellationToken ct = default);
    Task MarkAsReadAsync(Guid conversationPatientId, Guid readerId, CancellationToken ct = default);
}
