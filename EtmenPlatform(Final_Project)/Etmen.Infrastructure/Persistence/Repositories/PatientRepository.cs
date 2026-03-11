using Etmen.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Etmen.Infrastructure.Persistence.Repositories;

/// <summary>EF Core 9 implementation of IPatientRepository.</summary>
public sealed class PatientRepository : IPatientRepository
{
    private readonly AppDbContext _db;
    public PatientRepository(AppDbContext db) => _db = db;

    // TODO: Implement all interface methods using _db DbSets
}
