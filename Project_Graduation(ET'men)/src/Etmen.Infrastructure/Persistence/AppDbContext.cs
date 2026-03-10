using Etmen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Etmen.Infrastructure.Persistence;

/// <summary>
/// EF Core 8 database context. Applies all IEntityTypeConfiguration classes
/// from the current assembly and registers a global soft-delete query filter on User.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User>               Users               { get; set; }
    public DbSet<PatientProfile>     PatientProfiles     { get; set; }
    public DbSet<DoctorProfile>      DoctorProfiles      { get; set; }
    public DbSet<MedicalRecord>      MedicalRecords      { get; set; }
    public DbSet<RiskAssessment>     RiskAssessments     { get; set; }
    public DbSet<Alert>              Alerts              { get; set; }
    public DbSet<ChatMessage>        ChatMessages        { get; set; }
    public DbSet<HealthcareProvider> HealthcareProviders { get; set; }
    public DbSet<Appointment>        Appointments        { get; set; }
    public DbSet<AvailableSlot>      AvailableSlots      { get; set; }
    public DbSet<LabResult>          LabResults          { get; set; }
    public DbSet<FamilyLink>         FamilyLinks         { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
    }
}
