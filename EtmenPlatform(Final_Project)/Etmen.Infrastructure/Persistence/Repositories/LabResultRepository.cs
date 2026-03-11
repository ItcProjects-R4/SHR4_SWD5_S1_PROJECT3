using Etmen.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Etmen.Infrastructure.Persistence.Repositories;

/// <summary>EF Core 9 implementation of ILabResultRepository.</summary>
public sealed class LabResultRepository : ILabResultRepository
{
    private readonly AppDbContext _db;
    public LabResultRepository(AppDbContext db) => _db = db;

    // TODO: Implement all interface methods using _db DbSets
}
