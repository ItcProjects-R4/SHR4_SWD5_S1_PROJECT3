using Etmen.Domain.Entities;

namespace Etmen.Application.Interfaces;

/// <summary>Repository contract for MedicalRecord CRUD and patient history queries.</summary>
public interface IMedicalRecordRepository
{
    Task<MedicalRecord>  CreateAsync(MedicalRecord record, CancellationToken ct = default);
    Task<MedicalRecord?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<MedicalRecord>> GetByPatientIdAsync(Guid patientId, int page, int pageSize, CancellationToken ct = default);
    Task<MedicalRecord?> GetLatestByPatientIdAsync(Guid patientId, CancellationToken ct = default);
    Task<IEnumerable<MedicalRecord>> GetVitalsTimelineAsync(Guid patientId, CancellationToken ct = default);
}
