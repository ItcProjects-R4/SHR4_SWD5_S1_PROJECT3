namespace Etmen.Application.DTOs.Internal;

/// <summary>Search parameters passed to IGeoSearchService.</summary>
public sealed class GeoSearchCriteria
{
    public double  Latitude     { get; set; }
    public double  Longitude    { get; set; }
    public double  RadiusMeters { get; set; } = 5000;
    public string? Specialty    { get; set; }
    public bool    OpenNow      { get; set; } = false;
    public string? RiskLevel    { get; set; }
}
