using Etmen.Domain.Common;
using Etmen.Domain.Enums;

namespace Etmen.Domain.Entities;

/// <summary>
/// A single message in the Doctor–Patient internal chat channel.
/// PatientId always refers to the patient side of the conversation.
/// </summary>
public class ChatMessage : BaseEntity
{
    public Guid     SenderId    { get; private set; }
    public Guid     RecipientId { get; private set; }
    public Guid     PatientId   { get; private set; }
    public string   Text        { get; private set; } = string.Empty;
    public bool     IsRead      { get; private set; } = false;
    public UserRole SenderRole  { get; private set; }

    protected ChatMessage() { }

    public static ChatMessage Create(Guid senderId, Guid recipientId,
        Guid patientId, string text, UserRole senderRole)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Message text cannot be empty.", nameof(text));

        return new()
        {
            SenderId = senderId, RecipientId = recipientId,
            PatientId = patientId, Text = text, SenderRole = senderRole
        };
    }

    public void MarkAsRead() { IsRead = true; MarkUpdated(); }
}
