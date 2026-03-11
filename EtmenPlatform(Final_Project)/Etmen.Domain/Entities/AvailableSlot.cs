using Etmen.Domain.Common;

namespace Etmen.Domain.Entities;

/// <summary>A bookable time slot belonging to a HealthcareProvider.</summary>
public class AvailableSlot : BaseEntity
{
    public Guid     ProviderId { get; private set; }
    public DateTime SlotStart  { get; private set; }
    public DateTime SlotEnd    { get; private set; }
    public bool     IsBooked   { get; private set; } = false;

    protected AvailableSlot() { }

    public static AvailableSlot Create(Guid providerId, DateTime start, DateTime end)
        => new() { ProviderId = providerId, SlotStart = start, SlotEnd = end };

    public void Book()    { IsBooked = true;  MarkUpdated(); }
    public void Release() { IsBooked = false; MarkUpdated(); }
}
