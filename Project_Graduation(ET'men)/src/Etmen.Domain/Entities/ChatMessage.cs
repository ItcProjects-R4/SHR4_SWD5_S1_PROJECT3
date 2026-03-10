using Etmen.Domain.Common;
using Etmen.Domain.Enums;

namespace Etmen.Domain.Entities;

/// <summary>
/// Represents a single message in the Doctor–Patient internal chat.
/// Persisted to DB; also pushed via SignalR to connected clients.
/// </summary>
public class ChatMessage : BaseEntity
{
    public Guid     SenderId    { get; private set; }
    public Guid     RecipientId { get; private set; }
    public Guid     PatientId   { get; private set; } // always the patient side of the conversation
    public string   Text        { get; private set; } = default!;
    public bool     IsRead      { get; private set; } = false;
    public UserRole SenderRole  { get; private set; }

    private ChatMessage() { }

    /// <summary>
    /// Creates a validated chat message. Throws DomainException if text is empty.
    /// </summary>
    public static ChatMessage Create(Guid senderId, Guid recipientId,
                                     Guid patientId, string text, UserRole senderRole)
    {
        throw new NotImplementedException();
    }

    /// <summary>Marks the message as read and sets UpdatedAt.</summary>
    public void MarkAsRead() { throw new NotImplementedException(); }
}
