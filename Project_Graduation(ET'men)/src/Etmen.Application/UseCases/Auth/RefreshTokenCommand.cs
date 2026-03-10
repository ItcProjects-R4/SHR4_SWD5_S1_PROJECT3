using MediatR;

namespace Etmen.Application.UseCases.Auth;

/// <summary>Command: exchanges a valid refresh token for a new JWT access token.</summary>
public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<AuthResult>;

public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResult>
{
    public RefreshTokenCommandHandler() { throw new NotImplementedException(); }

    /// <summary>1) Validate refresh token  2) Check expiry  3) Issue new JWT</summary>
    public Task<AuthResult> Handle(RefreshTokenCommand request, CancellationToken ct)
        => throw new NotImplementedException();
}
