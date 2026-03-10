namespace Etmen.Application.Services;

/// <summary>
/// Contract for family account linking operations (v3.0).
/// Handles invite flow, profile switching, and link management.
/// </summary>
public interface IFamilyService
{
    Task<string>          GenerateInviteLinkAsync(Guid primaryUserId, string relationship, string email, CancellationToken ct = default);
    Task                  AcceptInviteAsync(string inviteToken, Guid linkedUserId, CancellationToken ct = default);
    Task<IEnumerable<LinkedProfileSummary>> GetLinkedProfilesAsync(Guid primaryUserId, CancellationToken ct = default);
    Task<string>          GetScopedTokenAsync(Guid primaryUserId, Guid linkedUserId, CancellationToken ct = default);
    Task                  RemoveLinkAsync(Guid linkId, Guid requestingUserId, CancellationToken ct = default);
}

public sealed record LinkedProfileSummary(
    Guid Id, string Name, string Relationship, double LatestRiskScore, string RiskLevel);
