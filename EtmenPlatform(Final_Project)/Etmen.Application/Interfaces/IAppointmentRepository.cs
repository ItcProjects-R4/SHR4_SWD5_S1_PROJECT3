using Etmen.Domain.Entities;

namespace Etmen.Application.Interfaces;

/// <summary>Repository contract for Appointment booking and status tracking.</summary>
public interface IAppointmentRepository
{
    Task<Appointment>              CreateAsync(Appointment appointment, CancellationToken ct = default);
    Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId, CancellationToken ct = default);
    Task<IEnumerable<Appointment>> GetByProviderIdAsync(Guid providerId, CancellationToken ct = default);
    Task                           UpdateStatusAsync(Guid appointmentId, int status, CancellationToken ct = default);
}
