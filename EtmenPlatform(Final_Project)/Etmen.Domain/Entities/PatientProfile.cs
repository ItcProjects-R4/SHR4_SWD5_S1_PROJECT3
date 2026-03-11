using Etmen.Domain.Common;
using Etmen.Domain.Enums;

namespace Etmen.Domain.Entities;

/// <summary>
/// Holds health demographics and lifestyle data for a Patient.
/// BMI and Age are computed properties — not stored in DB columns.
/// </summary>
public class PatientProfile : BaseEntity
{
    public Guid   UserId                        { get; private set; }
    public DateTime DateOfBirth                 { get; private set; }
    public string Gender                        { get; private set; } = string.Empty;
    public double Height                        { get; private set; } // cm
    public double Weight                        { get; private set; } // kg
    public bool   FamilyHistoryOfChronicDisease { get; private set; }
    public bool   IsSmoker                      { get; private set; }
    public PhysicalActivityLevel ActivityLevel  { get; private set; }

    // Computed — no DB column
    public int    Age => DateTime.UtcNow.Year - DateOfBirth.Year;
    public double BMI => Weight / Math.Pow(Height / 100.0, 2);

    // Navigation
    private readonly List<MedicalRecord> _medicalRecords = new();
    public IReadOnlyCollection<MedicalRecord> MedicalRecords => _medicalRecords;

    protected PatientProfile() { }

    public static PatientProfile Create(Guid userId, DateTime dob, string gender,
        double height, double weight, bool familyHistory, bool isSmoker,
        PhysicalActivityLevel activityLevel)
        => new()
        {
            UserId = userId, DateOfBirth = dob, Gender = gender,
            Height = height, Weight = weight,
            FamilyHistoryOfChronicDisease = familyHistory,
            IsSmoker = isSmoker, ActivityLevel = activityLevel
        };

    public void UpdateMetrics(double height, double weight, bool isSmoker,
        PhysicalActivityLevel activityLevel, bool familyHistory)
    {
        Height = height; Weight = weight;
        IsSmoker = isSmoker; ActivityLevel = activityLevel;
        FamilyHistoryOfChronicDisease = familyHistory;
        MarkUpdated();
    }
}
