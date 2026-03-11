using Etmen.Domain.Entities;

namespace Etmen.Application.Interfaces;

/// <summary>Repository contract for FamilyLink join-table management.</summary>
public interface IFamilyLinkRepository
{
    Task<FamilyLink>              CreateAsync(FamilyLink link, CancellationToken ct = default);
    Task<FamilyLink?>             GetByInviteTokenAsync(string token, CancellationToken ct = default);
    Task<IEnumerable<FamilyLink>> GetByPrimaryUserIdAsync(Guid primaryUserId, CancellationToken ct = default);
    Task<IEnumerable<FamilyLink>> GetByLinkedUserIdAsync(Guid linkedUserId, CancellationToken ct = default);
    Task                          DeleteAsync(Guid linkId, CancellationToken ct = default);
}
