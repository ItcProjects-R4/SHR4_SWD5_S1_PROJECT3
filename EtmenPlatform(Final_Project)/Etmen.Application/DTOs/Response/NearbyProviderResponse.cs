namespace Etmen.Application.DTOs.Response;

/// <summary>Response DTO returned by the NearbyProviderResponse operation.</summary>
public sealed class NearbyProviderResponse
{
    public Guid    ProviderId   { get; set; }
    public string  Name         { get; set; } = string.Empty;
    public string  Type         { get; set; } = string.Empty;
    public string  Specialty    { get; set; } = string.Empty;
    public string  Address      { get; set; } = string.Empty;
    public double  Distance     { get; set; } // meters
    public decimal Rating       { get; set; }
    public double  MatchScore   { get; set; }
    public bool    OpenNow      { get; set; }
    public bool    IsRegistered { get; set; }
    public string? Phone        { get; set; }
    public List<string> Slots   { get; set; } = new();
}
