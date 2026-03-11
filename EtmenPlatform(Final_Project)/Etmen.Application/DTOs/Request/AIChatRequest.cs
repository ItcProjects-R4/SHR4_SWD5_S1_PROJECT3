using Etmen.Application.DTOs.Internal;

namespace Etmen.Application.DTOs.Request;

/// <summary>Request DTO for the AIChatRequest operation.</summary>
public sealed class AIChatRequest
{
    public Guid        PatientId { get; set; }
    public string      Message   { get; set; } = string.Empty;
    public ChatHistory History   { get; set; } = new();
}
