using Etmen.Domain.Common;

namespace Etmen.Domain.Entities;

/// <summary>
/// A single health data submission by a patient.
/// Triggers AI risk scoring when created.
/// One-to-one with RiskAssessment.
/// </summary>
public class MedicalRecord : BaseEntity
{
    public Guid    PatientId                 { get; private set; }
    public double  BloodPressureSystolic     { get; private set; }
    public double  BloodPressureDiastolic    { get; private set; }
    public double  BloodSugar               { get; private set; } // mg/dL
    public double  BMI                      { get; private set; }
    public string? Symptoms                 { get; private set; }
    public string? DoctorNote               { get; private set; }
    public Guid?   DoctorNoteByDoctorId     { get; private set; }

    // Navigation
    public RiskAssessment? RiskAssessment { get; private set; }

    protected MedicalRecord() { }

    public static MedicalRecord Create(Guid patientId, double systolic, double diastolic,
        double bloodSugar, double bmi, string? symptoms)
    {
        if (systolic  <= 0) throw new ArgumentOutOfRangeException(nameof(systolic));
        if (diastolic <= 0) throw new ArgumentOutOfRangeException(nameof(diastolic));
        if (bloodSugar < 0) throw new ArgumentOutOfRangeException(nameof(bloodSugar));
        if (bmi        <= 0) throw new ArgumentOutOfRangeException(nameof(bmi));

        return new()
        {
            PatientId = patientId,
            BloodPressureSystolic  = systolic,
            BloodPressureDiastolic = diastolic,
            BloodSugar = bloodSugar,
            BMI = bmi,
            Symptoms = symptoms
        };
    }

    /// <summary>Factory used when OCR extracts values from an uploaded lab report.</summary>
    public static MedicalRecord CreateFromLabResult(Guid patientId, double bloodSugar,
        double systolic, double diastolic, double bmi, string? labNotes)
        => Create(patientId, systolic, diastolic, bloodSugar, bmi, labNotes);

    public void AttachRiskAssessment(RiskAssessment assessment)
        => RiskAssessment = assessment;

    public void AddDoctorNote(Guid doctorId, string note)
    {
        DoctorNote           = note;
        DoctorNoteByDoctorId = doctorId;
        MarkUpdated();
    }
}
