using Etmen.Domain.Enums;

namespace Etmen.API.DTOs.Request;

/// <summary>POST /api/auth/register</summary>
public sealed record RegisterRequest(string FullName, string Email, string Password,
    UserRole Role, DateTime DateOfBirth, string Gender);

/// <summary>POST /api/auth/login</summary>
public sealed record LoginRequest(string Email, string Password);

/// <summary>POST /api/auth/google/callback</summary>
public sealed record GoogleLoginRequest(string IdToken);

/// <summary>POST /api/medical-records</summary>
public sealed record CreateMedicalRecordRequest(
    Guid PatientId, double BloodPressureSystolic, double BloodPressureDiastolic,
    double BloodSugar, double BMI, string? Symptoms, PhysicalActivityLevel ActivityLevel);

/// <summary>PUT /api/patients/{id}</summary>
public sealed record UpdatePatientProfileRequest(
    double Height, double Weight, bool FamilyHistory,
    bool SmokingStatus, PhysicalActivityLevel ActivityLevel);

/// <summary>POST /api/chat/send</summary>
public sealed record SendChatMessageRequest(
    Guid ConversationId, Guid SenderId, Guid RecipientId, string Text);

/// <summary>POST /api/risk-assessments/trigger</summary>
public sealed record TriggerAssessmentRequest(Guid PatientId, Guid? DoctorId, string? Reason);
