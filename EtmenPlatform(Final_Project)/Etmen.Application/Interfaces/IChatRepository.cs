using Etmen.Domain.Entities;

namespace Etmen.Application.Interfaces;

/// <summary>Repository contract for Doctor–Patient chat persistence.</summary>
public interface IChatRepository
{
    Task<ChatMessage>              SaveMessageAsync(ChatMessage message, CancellationToken ct = default);
    Task<IEnumerable<ChatMessage>> GetConversationAsync(Guid patientId, Guid doctorId, CancellationToken ct = default);
    Task<IEnumerable<ChatMessage>> GetAllConversationsForDoctorAsync(Guid doctorId, CancellationToken ct = default);
    Task                           MarkAsReadAsync(Guid conversationPatientId, Guid readerId, CancellationToken ct = default);
}
