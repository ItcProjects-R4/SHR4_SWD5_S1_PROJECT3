namespace Etmen.Application.DTOs.Internal;

/// <summary>Last N messages passed to the LLM for conversation continuity.</summary>
public sealed class ChatHistory
{
    public List<ChatHistoryMessage> Messages { get; set; } = new();
}

public sealed class ChatHistoryMessage
{
    public string Role    { get; set; } = string.Empty; // "user" or "assistant"
    public string Content { get; set; } = string.Empty;
}
