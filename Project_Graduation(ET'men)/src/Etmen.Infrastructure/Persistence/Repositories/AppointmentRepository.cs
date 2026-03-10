using Etmen.Application.Interfaces;
using Etmen.Domain.Entities;
using Etmen.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Etmen.Infrastructure.Persistence.Repositories;

/// <summary>EF Core implementation of IAppointmentRepository (v3.0).</summary>
public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _db;
    public AppointmentRepository(AppDbContext db) => _db = db;

    public async Task<Appointment> CreateAsync(Appointment appointment, CancellationToken ct = default)
    {
        _db.Appointments.Add(appointment);
        await _db.SaveChangesAsync(ct);
        return appointment;
    }

    public Task<Appointment?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _db.Appointments.FirstOrDefaultAsync(a => a.Id == id, ct);

    public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId, CancellationToken ct = default)
        => await _db.Appointments.Where(a => a.PatientId == patientId).OrderByDescending(a => a.AppointmentAt).ToListAsync(ct);

    public async Task UpdateAsync(Appointment appointment, CancellationToken ct = default)
    {
        _db.Appointments.Update(appointment);
        await _db.SaveChangesAsync(ct);
    }
}
