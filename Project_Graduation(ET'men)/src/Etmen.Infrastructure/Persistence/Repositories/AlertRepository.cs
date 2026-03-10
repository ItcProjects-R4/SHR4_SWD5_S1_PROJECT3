using Etmen.Application.Interfaces;
using Etmen.Domain.Entities;
using Etmen.Domain.Enums;
using Etmen.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Etmen.Infrastructure.Persistence.Repositories;

/// <summary>EF Core implementation of IAlertRepository.</summary>
public class AlertRepository : IAlertRepository
{
    private readonly AppDbContext _db;
    public AlertRepository(AppDbContext db) => _db = db;

    public async Task<Alert> CreateAsync(Alert alert, CancellationToken ct = default)
    {
        _db.Alerts.Add(alert);
        await _db.SaveChangesAsync(ct);
        return alert;
    }

    public async Task<IEnumerable<Alert>> GetByPatientIdAsync(Guid patientId, CancellationToken ct = default)
        => await _db.Alerts.Where(a => a.PatientId == patientId).OrderByDescending(a => a.CreatedAt).ToListAsync(ct);

    public async Task MarkAsReadAsync(Guid alertId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetUnreadCountAsync(Guid patientId, CancellationToken ct = default)
        => _db.Alerts.CountAsync(a => a.PatientId == patientId && a.Status == AlertStatus.Unread, ct);
}
