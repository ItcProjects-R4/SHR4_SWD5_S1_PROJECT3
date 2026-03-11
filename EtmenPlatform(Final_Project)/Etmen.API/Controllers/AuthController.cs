using Etmen.Application.Common;
using Etmen.Application.DTOs.Request;
using Etmen.Application.UseCases.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>
/// Handles authentication: register, login, token refresh, and Google OAuth2.
/// All endpoints are public (no [Authorize]).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator) => _mediator = mediator;

    /// <summary>Register a new Patient or Doctor account.</summary>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new RegisterCommand(request), ct);
        return result.Match<IActionResult>(
            onSuccess: auth => CreatedAtAction(nameof(Register), auth),
            onFailure: err  => err.Type switch
            {
                ErrorType.Conflict => Conflict(new { err.Code, err.Message }),
                _                  => StatusCode(500, new { err.Code, err.Message })
            });
    }

    /// <summary>Login with email and password.</summary>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new LoginQuery(request), ct);
        return result.Match<IActionResult>(
            onSuccess: Ok,
            onFailure: err => err.Type switch
            {
                ErrorType.Unauthorized => Unauthorized(new { err.Code, err.Message }),
                ErrorType.NotFound     => Unauthorized(new { err.Code, err.Message }),
                _                      => StatusCode(500, new { err.Code, err.Message })
            });
    }

    /// <summary>Exchange a refresh token for a new access token.</summary>
    [HttpPost("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh([FromBody] string refreshToken, CancellationToken ct)
        => Ok(await _mediator.Send(new RefreshTokenCommand(refreshToken), ct));

    /// <summary>Login via Google OAuth2 ID token.</summary>
    [HttpPost("google")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GoogleLogin([FromBody] string idToken, CancellationToken ct)
        => Ok(await _mediator.Send(new GoogleLoginCommand(idToken), ct));
}
