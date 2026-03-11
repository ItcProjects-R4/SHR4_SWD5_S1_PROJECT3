namespace Etmen.Application.DTOs.Internal;

/// <summary>Structured response from the LLM patient chat service.</summary>
public sealed class PatientChatResponse
{
    public string Reply              { get; set; } = string.Empty;
    public bool   SuggestDoctorChat  { get; set; } = false;
    public bool   DetectedCrisis     { get; set; } = false;
}
