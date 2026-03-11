using Etmen.Domain.Entities;

namespace Etmen.Application.Interfaces;

/// <summary>Repository contract for Alert creation and status management.</summary>
public interface IAlertRepository
{
    Task<Alert>               CreateAsync(Alert alert, CancellationToken ct = default);
    Task<IEnumerable<Alert>>  GetByPatientIdAsync(Guid patientId, CancellationToken ct = default);
    Task                      MarkAsReadAsync(Guid alertId, CancellationToken ct = default);
    Task<int>                 GetUnreadCountAsync(Guid patientId, CancellationToken ct = default);
}
