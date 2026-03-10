using Etmen.Domain.Common;

namespace Etmen.Domain.Entities;

/// <summary>
/// Snapshot of a patient's vital signs at a point in time.
/// Owns a one-to-one RiskAssessment created by the AI engine after submission.
/// </summary>
public class MedicalRecord : BaseEntity
{
    public Guid    PatientId                 { get; private set; }
    public double  BloodPressureSystolic     { get; private set; }
    public double  BloodPressureDiastolic    { get; private set; }
    public double  BloodSugar                { get; private set; } // mg/dL
    public double  BMI                       { get; private set; }
    public string? Symptoms                  { get; private set; }
    public string? DoctorNote               { get; private set; }
    public Guid?   DoctorNoteByDoctorId     { get; private set; }
    public string? LabResultFileUrl         { get; private set; } // set when created via OCR

    // Navigation
    public RiskAssessment? RiskAssessment   { get; private set; }

    private MedicalRecord() { }

    /// <summary>Standard factory — creates a record from manually entered vitals.</summary>
    public static MedicalRecord Create(Guid patientId, double systolic, double diastolic,
                                       double bloodSugar, double bmi, string? symptoms)
    {
        throw new NotImplementedException();
    }

    /// <summary>Factory used when a LabResult OCR pipeline creates a record automatically.</summary>
    public static MedicalRecord CreateFromLabResult(Guid patientId,
                                                     object extractedLabValues,
                                                     string fileUrl)
    {
        throw new NotImplementedException();
    }

    /// <summary>Links the completed RiskAssessment to this record.</summary>
    public void AttachRiskAssessment(RiskAssessment assessment)
    {
        throw new NotImplementedException();
    }

    /// <summary>Allows a doctor to append a clinical note to this record.</summary>
    public void AddDoctorNote(Guid doctorId, string note)
    {
        throw new NotImplementedException();
    }
}
