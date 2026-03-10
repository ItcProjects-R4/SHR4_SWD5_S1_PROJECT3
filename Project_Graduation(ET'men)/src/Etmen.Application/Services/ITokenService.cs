using Etmen.Domain.Entities;
using System.Security.Claims;

namespace Etmen.Application.Services;

/// <summary>Contract for JWT access token and refresh token generation/validation.</summary>
public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    ClaimsPrincipal? ValidateToken(string token);
}
