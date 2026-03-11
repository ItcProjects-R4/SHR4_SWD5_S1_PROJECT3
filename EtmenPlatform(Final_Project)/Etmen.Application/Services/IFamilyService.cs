using Etmen.Domain.Entities;

namespace Etmen.Application.Services;

/// <summary>
/// Contract for family account linking business logic
/// (invite generation, token validation, profile switching).
/// </summary>
public interface IFamilyService
{
    Task<string>       GenerateInviteTokenAsync(Guid primaryUserId, string relationship, CancellationToken ct = default);
    Task<FamilyLink?>  ValidateAndAcceptInviteAsync(string token, Guid linkedUserId, CancellationToken ct = default);
    Task<string>       GenerateScopedTokenAsync(Guid primaryUserId, Guid linkedUserId, CancellationToken ct = default);
}
