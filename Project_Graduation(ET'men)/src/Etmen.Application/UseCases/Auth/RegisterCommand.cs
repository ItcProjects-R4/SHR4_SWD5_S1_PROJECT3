using Etmen.Domain.Enums;
using MediatR;

namespace Etmen.Application.UseCases.Auth;

/// <summary>Command: registers a new Patient or Doctor and issues a JWT on success.</summary>
public sealed record RegisterCommand(
    string FullName, string Email, string Password, UserRole Role,
    DateTime DateOfBirth, string Gender) : IRequest<AuthResult>;

public sealed record AuthResult(
    string AccessToken, string RefreshToken, DateTime ExpiresAt,
    Guid UserId, string Role, string FullName);

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResult>
{
    // Inject: IUserRepository, IPatientRepository, ITokenService, IPasswordHasher
    public RegisterCommandHandler() { throw new NotImplementedException(); }

    /// <summary>
    /// 1) Check email unique  2) Hash password  3) Create User+Profile  4) Issue JWT
    /// </summary>
    public Task<AuthResult> Handle(RegisterCommand request, CancellationToken ct)
        => throw new NotImplementedException();
}
