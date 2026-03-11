namespace Etmen.Application.DTOs.Internal;

/// <summary>A doctor specialty recommended for the patient's risk level.</summary>
public sealed class RecommendedDoctor
{
    public string Specialty { get; set; } = string.Empty;
    public string Reason    { get; set; } = string.Empty;
}
