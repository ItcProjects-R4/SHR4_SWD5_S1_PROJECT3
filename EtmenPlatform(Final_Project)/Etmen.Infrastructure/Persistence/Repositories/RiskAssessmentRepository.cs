using Etmen.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Etmen.Infrastructure.Persistence.Repositories;

/// <summary>EF Core 9 implementation of IRiskAssessmentRepository.</summary>
public sealed class RiskAssessmentRepository : IRiskAssessmentRepository
{
    private readonly AppDbContext _db;
    public RiskAssessmentRepository(AppDbContext db) => _db = db;

    // TODO: Implement all interface methods using _db DbSets
}
