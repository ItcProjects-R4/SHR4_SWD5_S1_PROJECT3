namespace Etmen.Application.DTOs.Request;

/// <summary>Request DTO for the SendChatMessageRequest operation.</summary>
public sealed class SendChatMessageRequest
{
    public Guid   ConversationPatientId { get; set; }
    public Guid   SenderId              { get; set; }
    public Guid   RecipientId           { get; set; }
    public string Text                  { get; set; } = string.Empty;
    public string SenderRole            { get; set; } = string.Empty;
}
