using Etmen.Domain.Entities;

namespace Etmen.Application.Interfaces;

/// <summary>Repository contract for LabResult storage and retrieval (v3.0 OCR feature).</summary>
public interface ILabResultRepository
{
    Task<LabResult>  CreateAsync(LabResult labResult, CancellationToken ct = default);
    Task<IEnumerable<LabResult>> GetByPatientIdAsync(Guid patientId, CancellationToken ct = default);
}
