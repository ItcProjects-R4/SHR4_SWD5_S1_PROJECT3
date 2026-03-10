using Etmen.Domain.Entities;

namespace Etmen.Application.Interfaces;

/// <summary>Repository contract for RiskAssessment persistence and history queries.</summary>
public interface IRiskAssessmentRepository
{
    Task<RiskAssessment>  CreateAsync(RiskAssessment assessment, CancellationToken ct = default);
    Task<RiskAssessment?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<RiskAssessment>> GetByPatientIdAsync(Guid patientId, int page = 1, int pageSize = 20, CancellationToken ct = default);
    Task<IEnumerable<RiskAssessment>> GetByPatientIdAsync(Guid patientId, DateTime from, DateTime to, CancellationToken ct = default);
    Task<RiskAssessment?> GetLatestByPatientIdAsync(Guid patientId, CancellationToken ct = default);
    Task AddAsync(RiskAssessment assessment, CancellationToken ct = default);
}
