using Etmen.Domain.Common;

namespace Etmen.Domain.Entities;

/// <summary>
/// Bookable time slot for a HealthcareProvider.
/// IsBooked is set to true when an Appointment is confirmed for this slot.
/// </summary>
public class AvailableSlot : BaseEntity
{
    public Guid     ProviderId { get; private set; }
    public DateTime SlotStart  { get; private set; }
    public DateTime SlotEnd    { get; private set; }
    public bool     IsBooked   { get; private set; } = false;

    private AvailableSlot() { }

    public static AvailableSlot Create(Guid providerId, DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }

    public void Book()    { throw new NotImplementedException(); }
    public void Release() { throw new NotImplementedException(); }
}
