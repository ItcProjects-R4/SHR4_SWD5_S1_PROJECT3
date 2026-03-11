using Etmen.Application.DTOs.Response;
using MediatR;

namespace Etmen.Application.UseCases.Auth;

/// <summary>Exchanges a valid refresh token for a new JWT access token.</summary>
public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<AuthResponse>;

public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
{
    public Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken ct)
        => throw new NotImplementedException();
}
