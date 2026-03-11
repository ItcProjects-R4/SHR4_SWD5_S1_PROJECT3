using Etmen.Domain.Common;

namespace Etmen.Domain.Entities;

/// <summary>
/// Represents a doctor, hospital, or clinic that can be found via GPS search.
/// IsRegistered = true means the provider has a platform account (instant booking).
/// </summary>
public class HealthcareProvider : BaseEntity
{
    public string  Name           { get; private set; } = string.Empty;
    public int     Type           { get; private set; } // 0=Doctor, 1=Hospital, 2=Clinic
    public string  Specialty      { get; private set; } = string.Empty;
    public string  Address        { get; private set; } = string.Empty;
    public double  Latitude       { get; private set; }
    public double  Longitude      { get; private set; }
    public string? Phone          { get; private set; }
    public string? GooglePlaceId  { get; private set; }
    public decimal Rating         { get; private set; }
    public bool    IsRegistered   { get; private set; } = false;
    public bool    IsActive       { get; private set; } = true;

    // Navigation
    private readonly List<AvailableSlot> _availableSlots = new();
    public IReadOnlyCollection<AvailableSlot> AvailableSlots => _availableSlots;

    protected HealthcareProvider() { }

    public static HealthcareProvider Create(string name, int type, string specialty,
        string address, double lat, double lng, string? phone, string? googlePlaceId)
        => new()
        {
            Name = name, Type = type, Specialty = specialty,
            Address = address, Latitude = lat, Longitude = lng,
            Phone = phone, GooglePlaceId = googlePlaceId
        };
}
