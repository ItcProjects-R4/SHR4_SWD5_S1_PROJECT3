namespace Etmen.Application.Services;

/// <summary>
/// Contract for GPS-based provider search.
/// Implemented by GoogleGeoSearchService in Infrastructure/Geo.
/// </summary>
public interface IGeoSearchService
{
    Task<IEnumerable<NearbyProvider>> SearchProvidersAsync(GeoSearchCriteria criteria, CancellationToken ct = default);
    Task<ProviderDetails?>            GetProviderDetailsAsync(string googlePlaceId, CancellationToken ct = default);
    Task<GeoCoordinate?>             GeocodeAddressAsync(string address, CancellationToken ct = default);
    double CalculateDistanceMeters(GeoCoordinate from, GeoCoordinate to);
}

public sealed record GeoCoordinate(double Latitude, double Longitude);

public sealed record GeoSearchCriteria(
    GeoCoordinate Center, double RadiusMeters,
    string? Specialty, string? RiskLevel, bool OpenNow);

public sealed record NearbyProvider(
    string Id, string Name, string Type, string Specialty,
    string Address, double Lat, double Lng, double Rating,
    double DistanceMeters, double MatchScore, bool IsRegistered);

public sealed record ProviderDetails(
    string GooglePlaceId, string Name, string Phone,
    string Address, List<string> OpeningHours, double Rating);
