namespace Etmen.Domain.ValueObjects;

/// <summary>
/// Value object representing a blood pressure reading (systolic/diastolic in mmHg).
/// Immutable — equality is based on value.
/// </summary>
public sealed record BloodPressure
{
    public double Systolic  { get; }
    public double Diastolic { get; }

    public BloodPressure(double systolic, double diastolic)
    {
        if (systolic  <= 0) throw new ArgumentOutOfRangeException(nameof(systolic));
        if (diastolic <= 0) throw new ArgumentOutOfRangeException(nameof(diastolic));
        Systolic  = systolic;
        Diastolic = diastolic;
    }

    public bool IsHypertensive => Systolic > 140 || Diastolic > 90;
    public override string ToString() => $"{Systolic}/{Diastolic} mmHg";
}
