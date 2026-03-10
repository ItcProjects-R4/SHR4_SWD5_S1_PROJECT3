using Etmen.Domain.Entities;

namespace Etmen.Application.Interfaces;

/// <summary>Repository contract for FamilyLink join table (v3.0 Family Linking feature).</summary>
public interface IFamilyLinkRepository
{
    Task<FamilyLink>  CreateAsync(FamilyLink link, CancellationToken ct = default);
    Task<FamilyLink?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<FamilyLink?> GetByInviteTokenAsync(string token, CancellationToken ct = default);
    Task<IEnumerable<FamilyLink>> GetByPrimaryUserIdAsync(Guid primaryUserId, CancellationToken ct = default);
    Task DeleteAsync(Guid linkId, CancellationToken ct = default);
}
