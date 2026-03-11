using Etmen.Domain.Entities;

namespace Etmen.Application.Interfaces;

/// <summary>Repository contract for uploaded lab result records.</summary>
public interface ILabResultRepository
{
    Task<LabResult>              CreateAsync(LabResult labResult, CancellationToken ct = default);
    Task<LabResult?>             GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<LabResult>> GetByPatientIdAsync(Guid patientId, CancellationToken ct = default);
}
