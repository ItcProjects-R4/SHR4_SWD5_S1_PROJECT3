namespace Etmen.Domain.Enums;

/// <summary>Appointment lifecycle: Pending → Confirmed → Completed / Cancelled.</summary>
public enum AppointmentStatus { Pending = 0, Confirmed = 1, Cancelled = 2, Completed = 3 }
