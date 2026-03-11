namespace Etmen.Application.DTOs.Response;

/// <summary>Response DTO returned by the ChatMessageResponse operation.</summary>
public sealed class ChatMessageResponse
{
    public Guid     MessageId   { get; set; }
    public string   SenderName  { get; set; } = string.Empty;
    public string   SenderRole  { get; set; } = string.Empty;
    public string   Text        { get; set; } = string.Empty;
    public DateTime SentAt      { get; set; }
    public bool     IsRead      { get; set; }
}
