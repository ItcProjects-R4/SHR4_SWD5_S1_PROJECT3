using Etmen.Application.DTOs.Internal;
using Etmen.Application.DTOs.Response;

namespace Etmen.Application.Services;

/// <summary>
/// Contract for GPS-based healthcare provider search.
/// Implemented by GoogleGeoSearchService using Google Places API.
/// </summary>
public interface IGeoSearchService
{
    Task<IEnumerable<NearbyProviderResponse>> SearchProvidersAsync(GeoSearchCriteria criteria, CancellationToken ct = default);
    Task<ProviderDetails?>                    GetProviderDetailsAsync(string googlePlaceId, CancellationToken ct = default);
    Task<GeoCoordinate?>                      GeocodeAddressAsync(string address, CancellationToken ct = default);
    double                                    CalculateDistanceMeters(GeoCoordinate from, GeoCoordinate to);
}
