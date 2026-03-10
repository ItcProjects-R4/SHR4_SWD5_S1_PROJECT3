using MediatR;

namespace Etmen.Application.UseCases.Auth;

/// <summary>Query: validates credentials and returns a JWT + refresh token pair.</summary>
public sealed record LoginQuery(string Email, string Password) : IRequest<AuthResult>;

public sealed class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResult>
{
    public LoginQueryHandler() { throw new NotImplementedException(); }

    /// <summary>
    /// 1) Find user  2) Verify BCrypt  3) Generate JWT+Refresh  4) Save refresh token
    /// </summary>
    public Task<AuthResult> Handle(LoginQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
