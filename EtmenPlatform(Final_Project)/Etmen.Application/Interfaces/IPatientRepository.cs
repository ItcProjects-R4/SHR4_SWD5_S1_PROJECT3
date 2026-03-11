using Etmen.Domain.Entities;

namespace Etmen.Application.Interfaces;

/// <summary>Repository contract for PatientProfile queries and updates.</summary>
public interface IPatientRepository
{
    Task<PatientProfile?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<PatientProfile?> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
    Task<IEnumerable<PatientProfile>> GetAllAsync(int page, int pageSize, CancellationToken ct = default);
    Task<IEnumerable<PatientProfile>> GetHighRiskAsync(CancellationToken ct = default);
    Task<PatientProfile>  CreateAsync(PatientProfile profile, CancellationToken ct = default);
    Task                  UpdateAsync(PatientProfile profile, CancellationToken ct = default);
}
