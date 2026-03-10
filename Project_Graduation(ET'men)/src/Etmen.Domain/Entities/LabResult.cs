using Etmen.Domain.Common;

namespace Etmen.Domain.Entities;

/// <summary>
/// Stores a patient's uploaded lab report PDF/image and the values extracted by OCR.
/// A new MedicalRecord is automatically created from the extracted values.
/// </summary>
public class LabResult : BaseEntity
{
    public Guid      PatientId          { get; private set; }
    public Guid      MedicalRecordId    { get; private set; } // auto-created from OCR output
    public string    FileUrl            { get; private set; } = default!; // blob storage
    public string?   LabName            { get; private set; }
    public DateTime? LabDate            { get; private set; }
    public string    RawExtractedJson   { get; private set; } = default!; // full OCR audit trail

    private LabResult() { }

    public static LabResult Create(Guid patientId, Guid medicalRecordId,
                                    string fileUrl, string rawJson,
                                    string? labName = null, DateTime? labDate = null)
    {
        throw new NotImplementedException();
    }
}
