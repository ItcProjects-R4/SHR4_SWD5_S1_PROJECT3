using Etmen.Domain.Common;
using Etmen.Domain.Enums;

namespace Etmen.Domain.Entities;

/// <summary>
/// Health profile for a patient user.
/// Computed properties (Age, BMI) are not persisted in the database.
/// </summary>
public class PatientProfile : BaseEntity
{
    public Guid                  UserId                        { get; private set; }
    public DateTime              DateOfBirth                   { get; private set; }
    public string                Gender                        { get; private set; } = default!;
    public double                Height                        { get; private set; } // cm
    public double                Weight                        { get; private set; } // kg
    public bool                  FamilyHistoryOfChronicDisease { get; private set; }
    public bool                  IsSmoker                      { get; private set; }
    public PhysicalActivityLevel ActivityLevel                 { get; private set; }

    // Computed — no DB column
    public int    Age => DateTime.UtcNow.Year - DateOfBirth.Year;
    public double BMI => Weight / Math.Pow(Height / 100.0, 2);

    // Navigation
    public IReadOnlyCollection<MedicalRecord> MedicalRecords => _medicalRecords;
    private readonly List<MedicalRecord> _medicalRecords = new();

    private PatientProfile() { }

    /// <summary>Creates a new patient profile with initial health metrics.</summary>
    public static PatientProfile Create(Guid userId, DateTime dob, string gender,
                                        double height, double weight,
                                        bool familyHistory, bool isSmoker,
                                        PhysicalActivityLevel activity)
    {
        throw new NotImplementedException();
    }

    /// <summary>Updates mutable health metrics and triggers BMI recalculation.</summary>
    public void UpdateMetrics(double height, double weight, bool isSmoker,
                              PhysicalActivityLevel activity)
    {
        throw new NotImplementedException();
    }
}
