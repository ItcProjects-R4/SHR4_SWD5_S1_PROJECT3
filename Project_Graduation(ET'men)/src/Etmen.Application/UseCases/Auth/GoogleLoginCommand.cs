using MediatR;

namespace Etmen.Application.UseCases.Auth;

/// <summary>Command: verifies a Google IdToken and upserts the user, then issues a JWT.</summary>
public sealed record GoogleLoginCommand(string IdToken) : IRequest<AuthResult>;

public sealed class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommand, AuthResult>
{
    public GoogleLoginCommandHandler() { throw new NotImplementedException(); }

    /// <summary>1) Verify Google IdToken  2) Upsert user  3) Issue JWT</summary>
    public Task<AuthResult> Handle(GoogleLoginCommand request, CancellationToken ct)
        => throw new NotImplementedException();
}
