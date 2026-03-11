using System.Reflection;
using Etmen.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Etmen.Infrastructure.Persistence;

/// <summary>
/// EF Core 10 DbContext for the Et'men Platform.
/// Applies all IEntityTypeConfiguration files from this assembly automatically.
/// Global soft-delete query filter is set on User.
/// </summary>
public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // ── Core v2.0 Tables ──────────────────────────────────────────
    public DbSet<User>             Users             { get; set; }
    public DbSet<PatientProfile>   PatientProfiles   { get; set; }
    public DbSet<DoctorProfile>    DoctorProfiles    { get; set; }
    public DbSet<MedicalRecord>    MedicalRecords    { get; set; }
    public DbSet<RiskAssessment>   RiskAssessments   { get; set; }
    public DbSet<Alert>            Alerts            { get; set; }
    public DbSet<ChatMessage>      ChatMessages      { get; set; }

    // ── New v3.0 Tables ───────────────────────────────────────────
    public DbSet<HealthcareProvider> HealthcareProviders { get; set; }
    public DbSet<Appointment>        Appointments        { get; set; }
    public DbSet<AvailableSlot>      AvailableSlots      { get; set; }
    public DbSet<LabResult>          LabResults          { get; set; }
    public DbSet<FamilyLink>         FamilyLinks         { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Global soft-delete filter
        builder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<PatientProfile>().HasQueryFilter(p => !p.IsDeleted);
    }
}
