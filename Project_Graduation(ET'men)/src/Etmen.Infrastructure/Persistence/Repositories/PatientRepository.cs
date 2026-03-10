using Etmen.Application.Interfaces;
using Etmen.Domain.Entities;
using Etmen.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Etmen.Infrastructure.Persistence.Repositories;

/// <summary>EF Core implementation of IPatientRepository.</summary>
public class PatientRepository : IPatientRepository
{
    private readonly AppDbContext _db;
    public PatientRepository(AppDbContext db) => _db = db;

    public Task<PatientProfile?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _db.PatientProfiles.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, ct);

    public Task<PatientProfile?> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
        => _db.PatientProfiles.FirstOrDefaultAsync(p => p.UserId == userId && !p.IsDeleted, ct);

    public async Task<IEnumerable<PatientProfile>> GetAllAsync(string? filter = null, int page = 1, int pageSize = 20, CancellationToken ct = default)
        => throw new NotImplementedException();

    public async Task<IEnumerable<PatientProfile>> GetHighRiskAsync(CancellationToken ct = default)
        => throw new NotImplementedException();

    public async Task<PatientProfile> CreateAsync(PatientProfile profile, CancellationToken ct = default)
    {
        _db.PatientProfiles.Add(profile);
        await _db.SaveChangesAsync(ct);
        return profile;
    }

    public async Task UpdateAsync(PatientProfile profile, CancellationToken ct = default)
    {
        _db.PatientProfiles.Update(profile);
        await _db.SaveChangesAsync(ct);
    }
}
