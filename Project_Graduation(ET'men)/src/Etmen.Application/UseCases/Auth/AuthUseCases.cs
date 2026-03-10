using Etmen.Application.Interfaces;
using Etmen.Application.Services;
using MediatR;

namespace Etmen.Application.UseCases.Auth;

// ════════════════════════════════════════════════════════════════════════════
// RegisterCommand
// ════════════════════════════════════════════════════════════════════════════

/// <summary>Creates a new Patient or Doctor account and issues initial JWT tokens.</summary>
public sealed record RegisterCommand(
    string FullName,
    string Email,
    string Password,
    string Role
) : IRequest<AuthResult>;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResult>
{
    private readonly IUserRepository _userRepo;
    private readonly ITokenService _tokenService;

    public RegisterCommandHandler(IUserRepository userRepo, ITokenService tokenService)
    {
        _userRepo = userRepo;
        _tokenService = tokenService;
    }

    /// <summary>
    /// Steps: 1) Check email is unique  2) Hash password (BCrypt)
    ///        3) Create User + Profile  4) Issue JWT + refresh token
    /// </summary>
    public async Task<AuthResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// LoginQuery
// ════════════════════════════════════════════════════════════════════════════

/// <summary>Authenticates an existing user with email/password and returns JWT tokens.</summary>
public sealed record LoginQuery(string Email, string Password) : IRequest<AuthResult>;

public sealed class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResult>
{
    private readonly IUserRepository _userRepo;
    private readonly ITokenService _tokenService;

    public LoginQueryHandler(IUserRepository userRepo, ITokenService tokenService)
    {
        _userRepo = userRepo;
        _tokenService = tokenService;
    }

    /// <summary>
    /// Steps: 1) Find user by email  2) Verify BCrypt hash
    ///        3) Generate JWT + refresh token  4) Persist refresh token
    /// </summary>
    public async Task<AuthResult> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// RefreshTokenCommand
// ════════════════════════════════════════════════════════════════════════════

/// <summary>Issues a new access token using a valid, non-expired refresh token.</summary>
public sealed record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<AuthResult>;

public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResult>
{
    private readonly IUserRepository _userRepo;
    private readonly ITokenService _tokenService;

    public RefreshTokenCommandHandler(IUserRepository userRepo, ITokenService tokenService)
    {
        _userRepo = userRepo;
        _tokenService = tokenService;
    }

    /// <summary>
    /// Steps: 1) Validate access token (ignore expiry)  2) Find user + verify stored refresh token
    ///        3) Check refresh token expiry  4) Issue new JWT + rotate refresh token
    /// </summary>
    public async Task<AuthResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// GoogleLoginCommand
// ════════════════════════════════════════════════════════════════════════════

/// <summary>Verifies a Google ID token and upserts the user, then issues platform JWT tokens.</summary>
public sealed record GoogleLoginCommand(string IdToken) : IRequest<AuthResult>;

public sealed class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommand, AuthResult>
{
    private readonly IUserRepository _userRepo;
    private readonly ITokenService _tokenService;

    public GoogleLoginCommandHandler(IUserRepository userRepo, ITokenService tokenService)
    {
        _userRepo = userRepo;
        _tokenService = tokenService;
    }

    /// <summary>
    /// Steps: 1) Verify Google IdToken via Google API
    ///        2) Upsert user (CreateFromGoogle or update existing)
    ///        3) Issue JWT + refresh token
    /// </summary>
    public async Task<AuthResult> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

// ── Shared result record ─────────────────────────────────────────────────────
/// <summary>Returned by all auth commands/queries. Maps directly to AuthResponse DTO.</summary>
public sealed record AuthResult(
    string AccessToken, string RefreshToken,
    DateTime ExpiresAt, Guid UserId, string Role, string FullName);
