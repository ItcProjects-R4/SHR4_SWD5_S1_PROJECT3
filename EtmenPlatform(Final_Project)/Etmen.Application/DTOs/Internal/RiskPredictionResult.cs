using Etmen.Domain.Enums;

namespace Etmen.Application.DTOs.Internal;

/// <summary>Output from the AI model including probability and classified level.</summary>
public sealed class RiskPredictionResult
{
    public double    Score        { get; set; } // 0.0 – 1.0
    public RiskLevel RiskLevel    { get; set; }
    public string    ModelVersion { get; set; } = string.Empty;
    public string?   PrimaryIssue { get; set; }
}
