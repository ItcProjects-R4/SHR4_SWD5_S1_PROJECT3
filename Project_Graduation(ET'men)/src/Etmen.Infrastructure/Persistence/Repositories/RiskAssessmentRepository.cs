using Etmen.Application.Interfaces;
using Etmen.Domain.Entities;
using Etmen.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Etmen.Infrastructure.Persistence.Repositories;

/// <summary>EF Core implementation of IRiskAssessmentRepository.</summary>
public class RiskAssessmentRepository : IRiskAssessmentRepository
{
    private readonly AppDbContext _db;
    public RiskAssessmentRepository(AppDbContext db) => _db = db;

    public async Task<RiskAssessment> CreateAsync(RiskAssessment assessment, CancellationToken ct = default)
    {
        _db.RiskAssessments.Add(assessment);
        await _db.SaveChangesAsync(ct);
        return assessment;
    }

    public Task<RiskAssessment?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _db.RiskAssessments.FirstOrDefaultAsync(a => a.Id == id, ct);

    public async Task<IEnumerable<RiskAssessment>> GetByPatientIdAsync(Guid patientId, int page = 1, int pageSize = 20, CancellationToken ct = default)
        => await _db.RiskAssessments.Where(a => a.PatientId == patientId)
            .OrderByDescending(a => a.CreatedAt).Skip((page-1)*pageSize).Take(pageSize).ToListAsync(ct);

    public async Task<IEnumerable<RiskAssessment>> GetByPatientIdAsync(Guid patientId, DateTime from, DateTime to, CancellationToken ct = default)
        => await _db.RiskAssessments.Where(a => a.PatientId == patientId && a.CreatedAt >= from && a.CreatedAt <= to)
            .OrderBy(a => a.CreatedAt).ToListAsync(ct);

    public Task<RiskAssessment?> GetLatestByPatientIdAsync(Guid patientId, CancellationToken ct = default)
        => _db.RiskAssessments.Where(a => a.PatientId == patientId).OrderByDescending(a => a.CreatedAt).FirstOrDefaultAsync(ct);

    public async Task AddAsync(RiskAssessment assessment, CancellationToken ct = default)
    {
        _db.RiskAssessments.Add(assessment);
        await _db.SaveChangesAsync(ct);
    }
}
