using Etmen.Domain.Entities;

namespace Etmen.Application.Interfaces;

/// <summary>Repository contract for HealthcareProvider geo-queries and management.</summary>
public interface IHealthcareProviderRepository
{
    Task<IEnumerable<HealthcareProvider>> GetByRadiusAsync(double lat, double lng, double radiusMeters, CancellationToken ct = default);
    Task<IEnumerable<HealthcareProvider>> GetBySpecialtyAsync(string specialty, CancellationToken ct = default);
    Task<HealthcareProvider?>             GetByGooglePlaceIdAsync(string placeId, CancellationToken ct = default);
    Task<HealthcareProvider?>             GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<HealthcareProvider>              UpsertAsync(HealthcareProvider provider, CancellationToken ct = default);
}
