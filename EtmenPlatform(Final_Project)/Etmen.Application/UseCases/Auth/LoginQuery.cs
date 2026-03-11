using Etmen.Application.Common;
using Etmen.Application.DTOs.Request;
using Etmen.Application.DTOs.Response;
using MediatR;

namespace Etmen.Application.UseCases.Auth;

/// <summary>
/// Authenticates with email and password. Returns JWT and refresh token.
/// Returns Result.Failure(Error.Unauthorized) on wrong credentials.
/// </summary>
public sealed record LoginQuery(LoginRequest Request) : IRequest<Result<AuthResponse>>;

public sealed class LoginQueryHandler
    : IRequestHandler<LoginQuery, Result<AuthResponse>>
{
    public Task<Result<AuthResponse>> Handle(LoginQuery request, CancellationToken ct)
        => throw new NotImplementedException();
    // Implementation hint:
    // 1. Find user by email → Error.NotFound if missing
    // 2. BCrypt.Verify password → Error.Unauthorized if wrong
    // 3. Generate tokens
    // 4. return Result<AuthResponse>.Success(...)
}
