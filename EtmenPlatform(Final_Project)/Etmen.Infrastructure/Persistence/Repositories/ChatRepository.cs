using Etmen.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Etmen.Infrastructure.Persistence.Repositories;

/// <summary>EF Core 9 implementation of IChatRepository.</summary>
public sealed class ChatRepository : IChatRepository
{
    private readonly AppDbContext _db;
    public ChatRepository(AppDbContext db) => _db = db;

    // TODO: Implement all interface methods using _db DbSets
}
