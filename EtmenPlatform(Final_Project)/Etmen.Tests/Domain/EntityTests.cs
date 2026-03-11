using Etmen.Domain.Entities;
using Etmen.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace Etmen.Tests.Domain;

/// <summary>Unit tests for the User domain entity.</summary>
public sealed class UserTests
{
    [Fact]
    public void Create_ShouldSetAllProperties()
    {
        var user = User.Create("Ahmed Ali", "ahmed@test.com", "hashed_pw", UserRole.Patient);

        user.FullName.Should().Be("Ahmed Ali");
        user.Email.Should().Be("ahmed@test.com");
        user.Role.Should().Be(UserRole.Patient);
        user.IsDeleted.Should().BeFalse();
        user.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void SetRefreshToken_ShouldUpdateTokenAndExpiry()
    {
        var user    = User.Create("Test", "t@t.com", "pw", UserRole.Doctor);
        var expiry  = DateTime.UtcNow.AddDays(7);
        var token   = "refresh-token-abc";

        user.SetRefreshToken(token, expiry);

        user.RefreshToken.Should().Be(token);
        user.RefreshTokenExpiry.Should().Be(expiry);
        user.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public void RevokeRefreshToken_ShouldClearToken()
    {
        var user = User.Create("Test", "t@t.com", "pw", UserRole.Patient);
        user.SetRefreshToken("token", DateTime.UtcNow.AddDays(7));

        user.RevokeRefreshToken();

        user.RefreshToken.Should().BeNull();
        user.RefreshTokenExpiry.Should().BeNull();
    }
}

/// <summary>Unit tests for the PatientProfile domain entity.</summary>
public sealed class PatientProfileTests
{
    [Fact]
    public void BMI_ShouldBeCalculatedCorrectly()
    {
        var profile = PatientProfile.Create(
            Guid.NewGuid(), new DateTime(1990, 1, 1), "Male",
            170, 70, false, false, PhysicalActivityLevel.Moderate);

        // BMI = 70 / (1.70)^2 = 24.22
        profile.BMI.Should().BeApproximately(24.22, 0.1);
    }

    [Fact]
    public void Age_ShouldBeCalculatedFromDateOfBirth()
    {
        var dob     = DateTime.UtcNow.AddYears(-30);
        var profile = PatientProfile.Create(
            Guid.NewGuid(), dob, "Female", 165, 60, false, false, PhysicalActivityLevel.Light);

        profile.Age.Should().Be(30);
    }

    [Fact]
    public void UpdateMetrics_ShouldUpdateValuesAndMarkUpdated()
    {
        var profile = PatientProfile.Create(
            Guid.NewGuid(), new DateTime(1985, 5, 10), "Male",
            175, 80, false, true, PhysicalActivityLevel.Sedentary);

        profile.UpdateMetrics(175, 75, false, PhysicalActivityLevel.Active, false);

        profile.Weight.Should().Be(75);
        profile.IsSmoker.Should().BeFalse();
        profile.ActivityLevel.Should().Be(PhysicalActivityLevel.Active);
        profile.UpdatedAt.Should().NotBeNull();
    }
}

/// <summary>Unit tests for the MedicalRecord domain entity.</summary>
public sealed class MedicalRecordTests
{
    [Fact]
    public void Create_WithValidData_ShouldSucceed()
    {
        var patientId = Guid.NewGuid();
        var record    = MedicalRecord.Create(patientId, 130, 85, 110, 24.5, "Headache");

        record.PatientId.Should().Be(patientId);
        record.BloodPressureSystolic.Should().Be(130);
        record.BloodSugar.Should().Be(110);
        record.Symptoms.Should().Be("Headache");
    }

    [Fact]
    public void Create_WithNegativeSystolic_ShouldThrow()
    {
        var act = () => MedicalRecord.Create(Guid.NewGuid(), -10, 80, 100, 22.0, null);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Create_WithZeroBMI_ShouldThrow()
    {
        var act = () => MedicalRecord.Create(Guid.NewGuid(), 120, 80, 100, 0, null);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void AddDoctorNote_ShouldSetNoteAndDoctorId()
    {
        var record   = MedicalRecord.Create(Guid.NewGuid(), 120, 80, 95, 22.0, null);
        var doctorId = Guid.NewGuid();

        record.AddDoctorNote(doctorId, "Patient should rest for 3 days.");

        record.DoctorNote.Should().Be("Patient should rest for 3 days.");
        record.DoctorNoteByDoctorId.Should().Be(doctorId);
    }
}

/// <summary>Unit tests for the RiskAssessment domain entity.</summary>
public sealed class RiskAssessmentTests
{
    [Fact]
    public void IsHighRisk_WhenLevelIsHigh_ShouldReturnTrue()
    {
        var assessment = RiskAssessment.Create(
            Guid.NewGuid(), Guid.NewGuid(), 0.85, RiskLevel.High,
            "v1.0", "High blood sugar", new(), new(), new());

        assessment.IsHighRisk().Should().BeTrue();
    }

    [Fact]
    public void IsHighRisk_WhenLevelIsLow_ShouldReturnFalse()
    {
        var assessment = RiskAssessment.Create(
            Guid.NewGuid(), Guid.NewGuid(), 0.15, RiskLevel.Low,
            "v1.0", null, new(), new(), new());

        assessment.IsHighRisk().Should().BeFalse();
    }
}

/// <summary>Unit tests for the Alert domain entity.</summary>
public sealed class AlertTests
{
    [Fact]
    public void Create_ShouldDefaultToUnread()
    {
        var alert = Alert.Create(Guid.NewGuid(), Guid.NewGuid(), "High risk detected", RiskLevel.High);
        alert.Status.Should().Be(AlertStatus.Unread);
    }

    [Fact]
    public void MarkAsRead_ShouldChangeStatusToRead()
    {
        var alert = Alert.Create(Guid.NewGuid(), null, "Message", RiskLevel.Medium);
        alert.MarkAsRead();
        alert.Status.Should().Be(AlertStatus.Read);
    }

    [Fact]
    public void Dismiss_ShouldChangeStatusToDismissed()
    {
        var alert = Alert.Create(Guid.NewGuid(), null, "Message", RiskLevel.Low);
        alert.Dismiss();
        alert.Status.Should().Be(AlertStatus.Dismissed);
    }
}

/// <summary>Unit tests for the ChatMessage domain entity.</summary>
public sealed class ChatMessageTests
{
    [Fact]
    public void Create_WithEmptyText_ShouldThrow()
    {
        var act = () => ChatMessage.Create(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "", UserRole.Patient);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void MarkAsRead_ShouldSetIsReadTrue()
    {
        var msg = ChatMessage.Create(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Hello doc", UserRole.Patient);
        msg.MarkAsRead();
        msg.IsRead.Should().BeTrue();
    }
}
