using Etmen.Domain.Entities;

namespace Etmen.Application.Interfaces;

/// <summary>Repository contract for HealthcareProvider geo-queries (v3.0 Nearby feature).</summary>
public interface IHealthcareProviderRepository
{
    Task<HealthcareProvider?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<HealthcareProvider>> GetNearbyAsync(double lat, double lng, double radiusMeters, string? specialty = null, CancellationToken ct = default);
    Task<HealthcareProvider> CreateAsync(HealthcareProvider provider, CancellationToken ct = default);
}
