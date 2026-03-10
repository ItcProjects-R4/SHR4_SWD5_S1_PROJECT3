using Etmen.Domain.Enums;

namespace Etmen.API.DTOs.Request;

// ── Auth ─────────────────────────────────────────────────────────────────────

/// <summary>Payload for POST /api/auth/register</summary>
public sealed record RegisterRequest(string FullName, string Email, string Password, string Role);

/// <summary>Payload for POST /api/auth/login</summary>
public sealed record LoginRequest(string Email, string Password);

/// <summary>Payload for POST /api/auth/refresh</summary>
public sealed record RefreshTokenRequest(string AccessToken, string RefreshToken);

/// <summary>Payload for POST /api/auth/google</summary>
public sealed record GoogleLoginRequest(string IdToken);

// ── Patient ───────────────────────────────────────────────────────────────────

/// <summary>Payload for PUT /api/patients/{id}/profile</summary>
public sealed record UpdatePatientProfileRequest(
    double Height, double Weight, bool IsSmoker, PhysicalActivityLevel ActivityLevel);

// ── Medical Record ────────────────────────────────────────────────────────────

/// <summary>Payload for POST /api/medical-records — triggers the full AI scoring pipeline.</summary>
public sealed record CreateMedicalRecordRequest(
    Guid PatientId,
    double BloodPressureSystolic,
    double BloodPressureDiastolic,
    double BloodSugar,
    double BMI,
    string? Symptoms);

/// <summary>Payload for POST /api/medical-records/{id}/doctor-note</summary>
public sealed record AddDoctorNoteRequest(string Note);

// ── Chat ──────────────────────────────────────────────────────────────────────

/// <summary>Payload for POST /api/chat/messages</summary>
public sealed record SendMessageRequest(
    Guid RecipientId, Guid PatientId, string Text, string SenderRole);

// ── Nearby (v3.0) ─────────────────────────────────────────────────────────────

/// <summary>Query params for GET /api/nearby/providers</summary>
public sealed record NearbySearchRequest(
    double Latitude, double Longitude,
    double RadiusMeters = 5000,
    string? Specialty = null,
    ProviderType? ProviderType = null);

/// <summary>Payload for POST /api/nearby/book</summary>
public sealed record BookAppointmentRequest(
    Guid ProviderId, Guid SlotId, Guid? AssessmentId, bool IsEmergency = false);

// ── Lab Results (v3.0) ────────────────────────────────────────────────────────
// File upload is handled as multipart/form-data — no request record needed.

// ── Family (v3.0) ─────────────────────────────────────────────────────────────

/// <summary>Payload for POST /api/family/invite</summary>
public sealed record InviteMemberRequest(
    string MemberEmail, FamilyRelationship Relationship, bool CanManage = false);

/// <summary>Payload for POST /api/family/accept</summary>
public sealed record AcceptInviteRequest(string InviteToken);

// ── AI Chat (v3.0) ────────────────────────────────────────────────────────────

/// <summary>Payload for POST /api/ai-chat/ask</summary>
public sealed record AIChatRequest(
    Guid PatientId,
    string Message,
    IEnumerable<AIChatHistoryItem> History);

/// <summary>A single message in the AI chat history sent to the LLM for context continuity.</summary>
public sealed record AIChatHistoryItem(string Role, string Text);

// ══════════════════════════════════════════════════════════════════════════════
namespace Etmen.API.DTOs.Response;

// ── Auth ──────────────────────────────────────────────────────────────────────

/// <summary>Returned by all auth endpoints.</summary>
public sealed record AuthResponse(
    string AccessToken, string RefreshToken,
    DateTime ExpiresAt, Guid UserId, string Role, string FullName);

// ── Patient ───────────────────────────────────────────────────────────────────

/// <summary>Returned by GET /api/patients/{id}/profile</summary>
public sealed record PatientProfileResponse(
    Guid PatientId, string FullName, int Age, double BMI,
    bool IsSmoker, PhysicalActivityLevel ActivityLevel,
    double? LatestRiskScore, RiskLevel? LatestRiskLevel);

// ── Risk Assessment ───────────────────────────────────────────────────────────

/// <summary>Returned by POST /api/medical-records and GET /api/risk-assessments/{id}</summary>
public sealed record RiskAssessmentResponse(
    Guid AssessmentId, double Score, RiskLevel RiskLevel,
    IEnumerable<RequiredAnalysisResponse> RequiredAnalyses,
    IEnumerable<RecommendedDoctorResponse> RecommendedDoctors,
    IEnumerable<string> Recommendations);

public sealed record RequiredAnalysisResponse(string Name, string Description, string Priority, string Icon);
public sealed record RecommendedDoctorResponse(string Speciality, string Reason);

// ── Admin Dashboard ───────────────────────────────────────────────────────────

/// <summary>Returned by GET /api/admin/dashboard</summary>
public sealed record AdminDashboardResponse(
    int TotalPatients, int HighRiskCount, int MediumRiskCount, int LowRiskCount,
    int UrgentAlerts,
    IEnumerable<PatientSummaryResponse> Patients);

public sealed record PatientSummaryResponse(
    Guid PatientId, string Name, int Age, double? RiskScore,
    RiskLevel? RiskLevel, string BloodPressure, double BloodSugar, string? DoctorName);

// ── Chat ──────────────────────────────────────────────────────────────────────

/// <summary>Returned by GET /api/chat/conversations/{patientId}/{doctorId}</summary>
public sealed record ChatMessageResponse(
    Guid MessageId, string SenderName, string SenderRole,
    string Text, DateTime SentAt, bool IsRead);

/// <summary>Returned by GET /api/chat/conversations (doctor's list)</summary>
public sealed record ConversationResponse(
    Guid PatientId, string PatientName, RiskLevel? RiskLevel,
    string? LastMessage, int UnreadCount);

// ── Nearby (v3.0) ─────────────────────────────────────────────────────────────

/// <summary>Returned by GET /api/nearby/providers</summary>
public sealed record NearbyProviderResponse(
    Guid? PlatformId, string? GooglePlaceId,
    string Name, string Address, double DistanceMeters,
    double Rating, bool IsRegistered, bool IsOpenNow, double MatchScore);

// ── Health History (v3.0) ─────────────────────────────────────────────────────

public sealed record RiskHistoryItemResponse(
    DateTime AssessmentDate, double RiskScore, RiskLevel RiskLevel, string? PrimaryIssue);

public sealed record VitalsDataPointResponse(
    DateTime Date, double BpSystolic, double BpDiastolic, double BloodSugar, double BMI);

// ── Lab Results (v3.0) ────────────────────────────────────────────────────────

public sealed record LabResultResponse(
    Guid LabResultId, string FileName, DateTime UploadedAt,
    double? HbA1c, double? BloodSugar, double? Cholesterol,
    bool TriggeredReassessment, Guid? LinkedAssessmentId);

// ── Family (v3.0) ─────────────────────────────────────────────────────────────

public sealed record FamilyMemberResponse(
    Guid LinkId, Guid LinkedUserId, string LinkedUserName,
    string Relationship, bool CanView, bool CanManage);

// ── AI Chat (v3.0) ────────────────────────────────────────────────────────────

/// <summary>Returned by POST /api/ai-chat/ask</summary>
public sealed record AIChatResponse(
    string Reply, bool SuggestDoctorChat, bool DetectedCrisis);

// ── Generic ───────────────────────────────────────────────────────────────────

/// <summary>Standard error envelope returned by ExceptionHandlingMiddleware.</summary>
public sealed record ErrorResponse(string Message, string? Detail = null, int StatusCode = 500);
