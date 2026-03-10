using Etmen.Application.Services;
using Etmen.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Etmen.Infrastructure.Auth;

/// <summary>
/// Concrete JWT implementation using HS256 symmetric signing.
/// Access tokens expire in 15 min; refresh tokens are 64-byte random base64 strings valid for 7 days.
/// </summary>
public class JwtTokenService : ITokenService
{
    private readonly IConfiguration _config;

    public JwtTokenService(IConfiguration config) => _config = config;

    /// <summary>Generates an HS256 JWT with standard claims (NameIdentifier, Email, Role, FullName).</summary>
    public string GenerateAccessToken(User user)
    {
        throw new NotImplementedException();
    }

    /// <summary>Generates a cryptographically secure 64-byte base64 refresh token.</summary>
    public string GenerateRefreshToken()
        => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

    /// <summary>Validates an access token without checking expiry (used for refresh flow).</summary>
    public ClaimsPrincipal? ValidateToken(string token)
    {
        throw new NotImplementedException();
    }
}
