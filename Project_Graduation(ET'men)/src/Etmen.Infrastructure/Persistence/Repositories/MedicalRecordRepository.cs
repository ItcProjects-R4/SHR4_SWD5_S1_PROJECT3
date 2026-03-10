using Etmen.Application.Interfaces;
using Etmen.Domain.Entities;
using Etmen.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Etmen.Infrastructure.Persistence.Repositories;

/// <summary>EF Core implementation of IMedicalRecordRepository.</summary>
public class MedicalRecordRepository : IMedicalRecordRepository
{
    private readonly AppDbContext _db;
    public MedicalRecordRepository(AppDbContext db) => _db = db;

    public async Task<MedicalRecord> CreateAsync(MedicalRecord record, CancellationToken ct = default)
    {
        _db.MedicalRecords.Add(record);
        await _db.SaveChangesAsync(ct);
        return record;
    }

    public Task<MedicalRecord?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _db.MedicalRecords.Include(r => r.RiskAssessment).FirstOrDefaultAsync(r => r.Id == id, ct);

    public async Task<IEnumerable<MedicalRecord>> GetByPatientIdAsync(Guid patientId, int page = 1, int pageSize = 20, CancellationToken ct = default)
        => await _db.MedicalRecords
            .Where(r => r.PatientId == patientId)
            .OrderByDescending(r => r.CreatedAt)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync(ct);

    public Task<MedicalRecord?> GetLatestByPatientIdAsync(Guid patientId, CancellationToken ct = default)
        => _db.MedicalRecords.Where(r => r.PatientId == patientId).OrderByDescending(r => r.CreatedAt).FirstOrDefaultAsync(ct);

    public async Task AddAsync(MedicalRecord record, CancellationToken ct = default)
    {
        _db.MedicalRecords.Add(record);
        await _db.SaveChangesAsync(ct);
    }
}
