namespace Etmen.Domain.ValueObjects;

/// <summary>
/// Value object representing a validated risk probability score (0.0 to 1.0).
/// Immutable — equality is based on value, not identity.
/// </summary>
public sealed record RiskScore
{
    public double Value { get; }

    public RiskScore(double value)
    {
        if (value < 0.0 || value > 1.0)
            throw new ArgumentOutOfRangeException(nameof(value), "Risk score must be between 0.0 and 1.0.");
        Value = value;
    }

    public static implicit operator double(RiskScore rs) => rs.Value;
    public override string ToString() => $"{Value:P0}";
}
