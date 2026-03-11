namespace Etmen.Application.DTOs.Response;

/// <summary>Response DTO returned by the ProviderDetails operation.</summary>
public sealed class ProviderDetails
{
    public Guid    ProviderId    { get; set; }
    public string  Name          { get; set; } = string.Empty;
    public string  Specialty     { get; set; } = string.Empty;
    public string  Address       { get; set; } = string.Empty;
    public string? Phone         { get; set; }
    public decimal Rating        { get; set; }
    public List<string> AvailableSlots { get; set; } = new();
    public List<string> Reviews        { get; set; } = new();
}
