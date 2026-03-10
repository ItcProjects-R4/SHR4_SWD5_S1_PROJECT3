namespace Etmen.Domain.ValueObjects;

/// <summary>
/// Encapsulates systolic/diastolic blood pressure as an immutable pair.
/// Provides hypertension classification helpers.
/// </summary>
public sealed record BloodPressure(double Systolic, double Diastolic)
{
    public bool IsHypertensive => Systolic > 140 || Diastolic > 90;
    public override string ToString() => $"{Systolic}/{Diastolic} mmHg";
}
