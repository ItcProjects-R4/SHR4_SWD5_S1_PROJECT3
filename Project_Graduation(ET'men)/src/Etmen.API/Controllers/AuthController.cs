using Etmen.Application.UseCases.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>
/// Handles all authentication flows: email/password, Google OAuth2, token refresh, logout.
/// Route: /api/auth — No business logic; delegates everything to MediatR.
/// </summary>
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator) => _mediator = mediator;

    /// <summary>POST /api/auth/register — Register Patient or Doctor, returns JWT.</summary>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterCommand command, CancellationToken ct)
        => Ok(await _mediator.Send(command, ct));

    /// <summary>POST /api/auth/login — Validate credentials, return JWT + refresh token.</summary>
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginQuery query, CancellationToken ct)
        => Ok(await _mediator.Send(query, ct));

    /// <summary>POST /api/auth/refresh — Exchange refresh token for new JWT.</summary>
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenCommand command, CancellationToken ct)
        => Ok(await _mediator.Send(command, ct));

    /// <summary>POST /api/auth/logout — Revoke refresh token.</summary>
    [HttpPost("logout")]
    public async Task<IActionResult> LogoutAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    /// <summary>GET /api/auth/google/callback — OAuth2 Google callback → issues JWT.</summary>
    [HttpGet("google/callback")]
    public async Task<IActionResult> GoogleCallbackAsync([FromQuery] string idToken, CancellationToken ct)
        => Ok(await _mediator.Send(new GoogleLoginCommand(idToken), ct));
}
