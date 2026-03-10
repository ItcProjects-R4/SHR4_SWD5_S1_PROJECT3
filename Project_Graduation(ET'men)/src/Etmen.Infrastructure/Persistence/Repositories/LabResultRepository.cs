using Etmen.Application.Interfaces;
using Etmen.Domain.Entities;
using Etmen.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Etmen.Infrastructure.Persistence.Repositories;

/// <summary>EF Core implementation of ILabResultRepository (v3.0 OCR feature).</summary>
public class LabResultRepository : ILabResultRepository
{
    private readonly AppDbContext _db;
    public LabResultRepository(AppDbContext db) => _db = db;

    public async Task<LabResult> CreateAsync(LabResult labResult, CancellationToken ct = default)
    {
        _db.LabResults.Add(labResult);
        await _db.SaveChangesAsync(ct);
        return labResult;
    }

    public async Task<IEnumerable<LabResult>> GetByPatientIdAsync(Guid patientId, CancellationToken ct = default)
        => await _db.LabResults.Where(l => l.PatientId == patientId).OrderByDescending(l => l.CreatedAt).ToListAsync(ct);
}
