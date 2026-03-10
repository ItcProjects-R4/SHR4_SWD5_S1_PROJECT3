using Etmen.Domain.Common;
using Etmen.Domain.Enums;

namespace Etmen.Domain.Entities;

/// <summary>
/// Doctor, hospital, or clinic returned by the Nearby Provider Search (v3.0).
/// Geo-indexed on Latitude/Longitude for spatial queries.
/// </summary>
public class HealthcareProvider : BaseEntity
{
    public string                 Name           { get; private set; } = default!;
    public HealthcareProviderType Type           { get; private set; }
    public string                 Specialty      { get; private set; } = default!;
    public string                 Address        { get; private set; } = default!;
    public double                 Latitude       { get; private set; }
    public double                 Longitude      { get; private set; }
    public string?                Phone          { get; private set; }
    public string?                GooglePlaceId  { get; private set; }
    public decimal                Rating         { get; private set; }
    public bool                   IsRegistered   { get; private set; } // platform-registered → instant booking
    public bool                   IsActive       { get; private set; } = true;

    // Navigation
    public IReadOnlyCollection<Appointment>    Appointments    => _appointments;
    public IReadOnlyCollection<AvailableSlot>  AvailableSlots  => _slots;
    private readonly List<Appointment>   _appointments = new();
    private readonly List<AvailableSlot> _slots        = new();

    private HealthcareProvider() { }

    public static HealthcareProvider Create(string name, HealthcareProviderType type,
                                             string specialty, string address,
                                             double lat, double lng)
    {
        throw new NotImplementedException();
    }
}
