namespace Etmen.Application.DTOs.Response;

/// <summary>Response DTO returned by the AIChatResponse operation.</summary>
public sealed class AIChatResponse
{
    public string Reply             { get; set; } = string.Empty;
    public bool   SuggestDoctorChat { get; set; } = false;
    public bool   DetectedCrisis    { get; set; } = false;
}
