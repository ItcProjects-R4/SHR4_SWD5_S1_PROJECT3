using Etmen.Application.Interfaces;
using Etmen.Domain.Entities;
using Etmen.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Etmen.Infrastructure.Persistence.Repositories;

/// <summary>EF Core implementation of IFamilyLinkRepository (v3.0 Family Linking feature).</summary>
public class FamilyLinkRepository : IFamilyLinkRepository
{
    private readonly AppDbContext _db;
    public FamilyLinkRepository(AppDbContext db) => _db = db;

    public async Task<FamilyLink> CreateAsync(FamilyLink link, CancellationToken ct = default)
    {
        _db.FamilyLinks.Add(link);
        await _db.SaveChangesAsync(ct);
        return link;
    }

    public Task<FamilyLink?> GetByIdAsync(Guid id, CancellationToken ct = default) => _db.FamilyLinks.FindAsync(new object[] { id }, ct).AsTask();
    public Task<FamilyLink?> GetByInviteTokenAsync(string token, CancellationToken ct = default) => _db.FamilyLinks.FirstOrDefaultAsync(l => l.InviteToken == token, ct);

    public async Task<IEnumerable<FamilyLink>> GetByPrimaryUserIdAsync(Guid primaryUserId, CancellationToken ct = default)
        => await _db.FamilyLinks.Where(l => l.PrimaryUserId == primaryUserId && !l.IsDeleted).ToListAsync(ct);

    public async Task DeleteAsync(Guid linkId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
