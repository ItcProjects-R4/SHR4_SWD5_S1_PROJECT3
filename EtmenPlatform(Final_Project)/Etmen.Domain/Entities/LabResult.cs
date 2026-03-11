using Etmen.Domain.Common;

namespace Etmen.Domain.Entities;

/// <summary>
/// Stores an uploaded lab report file and its OCR-extracted values.
/// The extracted values are used to create a new MedicalRecord automatically.
/// </summary>
public class LabResult : BaseEntity
{
    public Guid    PatientId         { get; private set; }
    public Guid    MedicalRecordId   { get; private set; }
    public string  FileUrl           { get; private set; } = string.Empty;
    public string? LabName           { get; private set; }
    public DateTime? LabDate         { get; private set; }
    public string? RawExtractedJson  { get; private set; }

    protected LabResult() { }

    public static LabResult Create(Guid patientId, Guid medicalRecordId,
        string fileUrl, string? labName, DateTime? labDate, string? rawJson)
        => new()
        {
            PatientId = patientId, MedicalRecordId = medicalRecordId,
            FileUrl = fileUrl, LabName = labName, LabDate = labDate,
            RawExtractedJson = rawJson
        };
}
