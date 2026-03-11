using Etmen.Domain.Entities;

namespace Etmen.Application.Interfaces;

/// <summary>Repository contract for RiskAssessment persistence and history queries.</summary>
public interface IRiskAssessmentRepository
{
    Task<RiskAssessment>  CreateAsync(RiskAssessment assessment, CancellationToken ct = default);
    Task<RiskAssessment?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<RiskAssessment?> GetLatestByPatientIdAsync(Guid patientId, CancellationToken ct = default);
    Task<IEnumerable<RiskAssessment>> GetByPatientIdAsync(Guid patientId, int page, int pageSize, CancellationToken ct = default);
    Task<IEnumerable<RiskAssessment>> GetByPatientIdInRangeAsync(Guid patientId, DateTime from, DateTime to, CancellationToken ct = default);
}
