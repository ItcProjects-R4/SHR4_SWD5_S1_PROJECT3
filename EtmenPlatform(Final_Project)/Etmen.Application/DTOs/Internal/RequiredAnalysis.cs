using Etmen.Domain.Enums;

namespace Etmen.Application.DTOs.Internal;

/// <summary>A lab test recommended based on elevated risk features.</summary>
public sealed class RequiredAnalysis
{
    public string           Name        { get; set; } = string.Empty;
    public string           Description { get; set; } = string.Empty;
    public AnalysisPriority Priority    { get; set; }
    public string           Icon        { get; set; } = string.Empty;
}
