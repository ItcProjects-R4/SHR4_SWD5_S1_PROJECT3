using Etmen.Domain.Entities;

namespace Etmen.Application.Interfaces;

/// <summary>Repository contract for User aggregate root.</summary>
public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task<User?> GetByGoogleIdAsync(string googleId, CancellationToken ct = default);
    Task<User>  CreateAsync(User user, CancellationToken ct = default);
    Task        UpdateRefreshTokenAsync(Guid userId, string? token, DateTime? expiry, CancellationToken ct = default);
    Task<bool>  EmailExistsAsync(string email, CancellationToken ct = default);
}
