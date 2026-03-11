using Etmen.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Etmen.Infrastructure.Persistence.Repositories;

/// <summary>EF Core 9 implementation of IMedicalRecordRepository.</summary>
public sealed class MedicalRecordRepository : IMedicalRecordRepository
{
    private readonly AppDbContext _db;
    public MedicalRecordRepository(AppDbContext db) => _db = db;

    // TODO: Implement all interface methods using _db DbSets
}
