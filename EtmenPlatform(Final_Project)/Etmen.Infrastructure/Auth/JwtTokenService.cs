using System.Security.Claims;
using System.Security.Cryptography;
using Etmen.Application.Services;
using Etmen.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Etmen.Infrastructure.Auth;

/// <summary>
/// Implements ITokenService.
/// Generates HS256 JWT access tokens (15 min) and 64-byte random refresh tokens (7 days).
/// Configure via appsettings: JwtSettings:SecretKey, Issuer, Audience, ExpiryMinutes.
/// </summary>
public sealed class JwtTokenService : ITokenService
{
    private readonly IConfiguration _config;
    public JwtTokenService(IConfiguration config) => _config = config;

    public string GenerateAccessToken(User user)
        => throw new NotImplementedException();

    public string GenerateRefreshToken()
        => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

    public ClaimsPrincipal? ValidateToken(string token)
        => throw new NotImplementedException();
}
