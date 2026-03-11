using Etmen.Application.DTOs.Internal;
using Etmen.Application.DTOs.Response;
using Etmen.Application.Services;
using Microsoft.Extensions.Configuration;

namespace Etmen.Infrastructure.Geo;

/// <summary>
/// Implements IGeoSearchService using Google Places Nearby Search API.
/// Merges registered DB providers with Google Places results.
/// MatchScore = Specialty(40%) + Distance(30%) + Rating(20%) + OpenNow+Slot(10%).
/// Configure: GoogleMaps:ApiKey, GoogleMaps:DefaultRadius in appsettings.
/// </summary>
public sealed class GoogleGeoSearchService : IGeoSearchService
{
    private readonly HttpClient     _http;
    private readonly IConfiguration _config;

    public GoogleGeoSearchService(HttpClient http, IConfiguration config)
    {
        _http   = http;
        _config = config;
    }

    public Task<IEnumerable<NearbyProviderResponse>> SearchProvidersAsync(
        GeoSearchCriteria criteria, CancellationToken ct = default)
        => throw new NotImplementedException();

    public Task<ProviderDetails?> GetProviderDetailsAsync(
        string googlePlaceId, CancellationToken ct = default)
        => throw new NotImplementedException();

    public Task<GeoCoordinate?> GeocodeAddressAsync(
        string address, CancellationToken ct = default)
        => throw new NotImplementedException();

    public double CalculateDistanceMeters(GeoCoordinate from, GeoCoordinate to)
        => throw new NotImplementedException();

    /// <summary>
    /// MatchScore = Specialty(0.4) + Distance(0.3) + Rating(0.2) + OpenNow+Slot(0.1)
    /// Distance decay: 1.0 at 0m → 0.0 at 5000m (linear).
    /// </summary>
    private static double CalculateMatchScore(
        NearbyProviderResponse provider, GeoSearchCriteria criteria)
        => throw new NotImplementedException();
}
