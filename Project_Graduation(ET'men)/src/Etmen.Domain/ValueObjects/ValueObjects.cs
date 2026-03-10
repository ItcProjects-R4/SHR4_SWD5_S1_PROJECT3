namespace Etmen.Domain.ValueObjects;

/// <summary>
/// Immutable value object representing the output of the ML risk model.
/// Score is a probability in [0, 1]. Level is derived by IRiskCalculationService.ClassifyRisk().
/// </summary>
public sealed record RiskScore
{
    /// <summary>Raw ML probability score between 0.0 and 1.0.</summary>
    public double Score { get; }

    /// <summary>Human-readable percentage, e.g. "72%".</summary>
    public string Percentage => $"{Score:P0}";

    public RiskScore(double score)
    {
        if (score < 0 || score > 1)
            throw new ArgumentOutOfRangeException(nameof(score), "Risk score must be between 0 and 1.");
        Score = score;
    }

    public static RiskScore Zero => new(0.0);
}

/// <summary>
/// Immutable value object encapsulating a blood pressure reading (systolic / diastolic in mmHg).
/// </summary>
public sealed record BloodPressure
{
    /// <summary>Systolic pressure in mmHg (top number).</summary>
    public double Systolic { get; }

    /// <summary>Diastolic pressure in mmHg (bottom number).</summary>
    public double Diastolic { get; }

    /// <summary>Human-readable string, e.g. "120/80".</summary>
    public string Display => $"{Systolic}/{Diastolic}";

    public BloodPressure(double systolic, double diastolic)
    {
        if (systolic <= 0) throw new ArgumentOutOfRangeException(nameof(systolic));
        if (diastolic <= 0) throw new ArgumentOutOfRangeException(nameof(diastolic));
        Systolic = systolic;
        Diastolic = diastolic;
    }
}

/// <summary>
/// Immutable value object representing a geographic coordinate (latitude / longitude).
/// Used by the Nearby Doctor & Hospital Finder feature.
/// </summary>
public sealed record GeoCoordinate
{
    public double Latitude { get; }
    public double Longitude { get; }

    public GeoCoordinate(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}
