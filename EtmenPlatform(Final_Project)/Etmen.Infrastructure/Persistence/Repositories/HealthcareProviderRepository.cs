using Etmen.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Etmen.Infrastructure.Persistence.Repositories;

/// <summary>EF Core 9 implementation of IHealthcareProviderRepository.</summary>
public sealed class HealthcareProviderRepository : IHealthcareProviderRepository
{
    private readonly AppDbContext _db;
    public HealthcareProviderRepository(AppDbContext db) => _db = db;

    // TODO: Implement all interface methods using _db DbSets
}
