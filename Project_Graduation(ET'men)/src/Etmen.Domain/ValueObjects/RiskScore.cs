using Etmen.Domain.Enums;

namespace Etmen.Domain.ValueObjects;

/// <summary>
/// Wraps the raw 0.0–1.0 probability value and exposes the derived RiskLevel.
/// Immutable value object — equality is by value, not identity.
/// </summary>
public sealed record RiskScore(double Value)
{
    public RiskLevel Level =>
        Value < 0.3 ? RiskLevel.Low :
        Value < 0.6 ? RiskLevel.Medium :
                      RiskLevel.High;

    public string AsPercentage() => $"{Value:P0}";
}
