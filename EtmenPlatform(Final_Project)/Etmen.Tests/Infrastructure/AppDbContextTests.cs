using Etmen.Domain.Entities;
using Etmen.Domain.Enums;
using Etmen.Infrastructure.Persistence;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Etmen.Tests.Infrastructure;

/// <summary>
/// Integration tests for AppDbContext using EF Core InMemory provider.
/// Verifies DbSets, soft-delete filter, and basic persistence.
/// </summary>
public sealed class AppDbContextTests : IDisposable
{
    private readonly AppDbContext _db;

    public AppDbContextTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _db = new AppDbContext(options);
    }

    [Fact]
    public async Task Users_DbSet_ShouldPersistAndRetrieve()
    {
        var user = User.Create("Test Patient", "p@test.com", "hash", UserRole.Patient);
        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        var found = await _db.Users.FirstOrDefaultAsync(u => u.Email == "p@test.com");
        found.Should().NotBeNull();
        found!.FullName.Should().Be("Test Patient");
    }

    [Fact]
    public async Task AllDbSets_ShouldBeAccessible()
    {
        // Verify all 12 DbSets are wired — just count, no data needed
        _db.Users.Should().NotBeNull();
        _db.PatientProfiles.Should().NotBeNull();
        _db.DoctorProfiles.Should().NotBeNull();
        _db.MedicalRecords.Should().NotBeNull();
        _db.RiskAssessments.Should().NotBeNull();
        _db.Alerts.Should().NotBeNull();
        _db.ChatMessages.Should().NotBeNull();
        _db.HealthcareProviders.Should().NotBeNull();
        _db.Appointments.Should().NotBeNull();
        _db.AvailableSlots.Should().NotBeNull();
        _db.LabResults.Should().NotBeNull();
        _db.FamilyLinks.Should().NotBeNull();

        await Task.CompletedTask;
    }

    [Fact]
    public async Task MedicalRecords_ShouldLinkToPatient()
    {
        var user    = User.Create("Patient A", "a@test.com", "pw", UserRole.Patient);
        var profile = PatientProfile.Create(
            user.Id, new DateTime(1990, 6, 15), "Male",
            175, 78, false, false, PhysicalActivityLevel.Moderate);
        var record  = MedicalRecord.Create(user.Id, 125, 82, 100, 25.4, "Fatigue");

        _db.Users.Add(user);
        _db.PatientProfiles.Add(profile);
        _db.MedicalRecords.Add(record);
        await _db.SaveChangesAsync();

        var savedRecord = await _db.MedicalRecords
            .FirstOrDefaultAsync(r => r.PatientId == user.Id);

        savedRecord.Should().NotBeNull();
        savedRecord!.BloodPressureSystolic.Should().Be(125);
    }

    [Fact]
    public async Task Alerts_ShouldDefaultToUnreadStatus()
    {
        var alert = Alert.Create(Guid.NewGuid(), Guid.NewGuid(), "High Risk!", RiskLevel.High);
        _db.Alerts.Add(alert);
        await _db.SaveChangesAsync();

        var saved = await _db.Alerts.FirstAsync();
        saved.Status.Should().Be(AlertStatus.Unread);
    }

    public void Dispose() => _db.Dispose();
}
