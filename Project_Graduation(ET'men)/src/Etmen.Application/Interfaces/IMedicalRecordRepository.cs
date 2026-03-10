using Etmen.Domain.Entities;

namespace Etmen.Application.Interfaces;

/// <summary>Repository contract for MedicalRecord persistence.</summary>
public interface IMedicalRecordRepository
{
    Task<MedicalRecord>  CreateAsync(MedicalRecord record, CancellationToken ct = default);
    Task<MedicalRecord?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<MedicalRecord>> GetByPatientIdAsync(Guid patientId, int page = 1, int pageSize = 20, CancellationToken ct = default);
    Task<MedicalRecord?> GetLatestByPatientIdAsync(Guid patientId, CancellationToken ct = default);
    Task AddAsync(MedicalRecord record, CancellationToken ct = default);
}
