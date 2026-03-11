using Etmen.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Etmen.Infrastructure.Persistence.Repositories;

/// <summary>EF Core 9 implementation of IFamilyLinkRepository.</summary>
public sealed class FamilyLinkRepository : IFamilyLinkRepository
{
    private readonly AppDbContext _db;
    public FamilyLinkRepository(AppDbContext db) => _db = db;

    // TODO: Implement all interface methods using _db DbSets
}
