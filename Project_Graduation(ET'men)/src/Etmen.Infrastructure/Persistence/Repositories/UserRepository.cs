using Etmen.Application.Interfaces;
using Etmen.Domain.Entities;
using Etmen.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Etmen.Infrastructure.Persistence.Repositories;

/// <summary>EF Core implementation of IUserRepository.</summary>
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    public UserRepository(AppDbContext db) => _db = db;

    public Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default) => _db.Users.FindAsync(new object[] { id }, ct).AsTask();
    public Task<User?> GetByEmailAsync(string email, CancellationToken ct = default) => _db.Users.FirstOrDefaultAsync(u => u.Email == email, ct);
    public Task<User?> GetByGoogleIdAsync(string googleId, CancellationToken ct = default) => _db.Users.FirstOrDefaultAsync(u => u.GoogleId == googleId, ct);

    public async Task<User> CreateAsync(User user, CancellationToken ct = default)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync(ct);
        return user;
    }

    public async Task UpdateRefreshTokenAsync(Guid userId, string? token, DateTime? expiry, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(User user, CancellationToken ct = default)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync(ct);
    }
}
