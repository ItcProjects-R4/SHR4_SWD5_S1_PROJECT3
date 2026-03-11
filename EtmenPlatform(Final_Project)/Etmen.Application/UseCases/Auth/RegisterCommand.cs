using Etmen.Application.Common;
using Etmen.Application.DTOs.Request;
using Etmen.Application.DTOs.Response;
using MediatR;

namespace Etmen.Application.UseCases.Auth;

/// <summary>
/// Registers a new Patient or Doctor account. Hashes password, issues JWT and refresh token.
/// Returns Result.Failure(Error.Conflict) if the email is already registered.
/// </summary>
public sealed record RegisterCommand(RegisterRequest Request) : IRequest<Result<AuthResponse>>;

public sealed class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, Result<AuthResponse>>
{
    public Task<Result<AuthResponse>> Handle(RegisterCommand request, CancellationToken ct)
        => throw new NotImplementedException();
    // Implementation hint:
    // 1. Check email uniqueness → return Error.Conflict("Auth.DuplicateEmail", "...")
    // 2. Hash password with BCrypt
    // 3. Create User entity
    // 4. Generate JWT + refresh token
    // 5. return Result<AuthResponse>.Success(new AuthResponse { ... })
}
