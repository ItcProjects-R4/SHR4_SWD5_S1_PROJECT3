using Etmen.Application.Interfaces;
using Etmen.Application.Services;
using Etmen.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Etmen.Infrastructure.Auth;

/// <summary>
/// Implements IFamilyService.
/// Handles invite token generation, validation, and scoped JWT for profile switching.
/// </summary>
public sealed class FamilyService : IFamilyService
{
    private readonly IFamilyLinkRepository _familyLinkRepo;
    private readonly ITokenService         _tokenService;
    private readonly IUserRepository       _userRepo;

    public FamilyService(
        IFamilyLinkRepository familyLinkRepo,
        ITokenService tokenService,
        IUserRepository userRepo)
    {
        _familyLinkRepo = familyLinkRepo;
        _tokenService   = tokenService;
        _userRepo       = userRepo;
    }

    public Task<string> GenerateInviteTokenAsync(
        Guid primaryUserId, string relationship, CancellationToken ct = default)
        => throw new NotImplementedException();

    public Task<FamilyLink?> ValidateAndAcceptInviteAsync(
        string token, Guid linkedUserId, CancellationToken ct = default)
        => throw new NotImplementedException();

    public Task<string> GenerateScopedTokenAsync(
        Guid primaryUserId, Guid linkedUserId, CancellationToken ct = default)
        => throw new NotImplementedException();
}
