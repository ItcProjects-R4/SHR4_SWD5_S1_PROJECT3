using Etmen.Domain.Entities;

namespace Etmen.Application.Interfaces;

/// <summary>Repository contract for Appointment booking and status management (v3.0).</summary>
public interface IAppointmentRepository
{
    Task<Appointment>  CreateAsync(Appointment appointment, CancellationToken ct = default);
    Task<Appointment?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId, CancellationToken ct = default);
    Task UpdateAsync(Appointment appointment, CancellationToken ct = default);
}
