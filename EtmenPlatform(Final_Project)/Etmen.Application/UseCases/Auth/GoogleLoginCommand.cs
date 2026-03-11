using Etmen.Application.DTOs.Response;
using MediatR;

namespace Etmen.Application.UseCases.Auth;

/// <summary>Authenticates via Google OAuth2 ID token. Creates account on first login.</summary>
public sealed record GoogleLoginCommand(string IdToken) : IRequest<AuthResponse>;

public sealed class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommand, AuthResponse>
{
    public Task<AuthResponse> Handle(GoogleLoginCommand request, CancellationToken ct)
        => throw new NotImplementedException();
}
