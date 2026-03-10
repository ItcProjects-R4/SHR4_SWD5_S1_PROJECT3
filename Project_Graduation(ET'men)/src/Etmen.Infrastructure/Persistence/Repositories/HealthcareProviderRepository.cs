using Etmen.Application.Interfaces;
using Etmen.Domain.Entities;
using Etmen.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Etmen.Infrastructure.Persistence.Repositories;

/// <summary>EF Core implementation for geo-spatial provider queries (v3.0).</summary>
public class HealthcareProviderRepository : IHealthcareProviderRepository
{
    private readonly AppDbContext _db;
    public HealthcareProviderRepository(AppDbContext db) => _db = db;

    public Task<HealthcareProvider?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _db.HealthcareProviders.FindAsync(new object[] { id }, ct).AsTask();

    /// <summary>
    /// Returns providers within the bounding-box approximation.
    /// For production, use NetTopologySuite spatial queries or Azure Cosmos Geo.
    /// </summary>
    public async Task<IEnumerable<HealthcareProvider>> GetNearbyAsync(double lat, double lng, double radiusMeters, string? specialty = null, CancellationToken ct = default)
        => throw new NotImplementedException();

    public async Task<HealthcareProvider> CreateAsync(HealthcareProvider provider, CancellationToken ct = default)
    {
        _db.HealthcareProviders.Add(provider);
        await _db.SaveChangesAsync(ct);
        return provider;
    }
}
