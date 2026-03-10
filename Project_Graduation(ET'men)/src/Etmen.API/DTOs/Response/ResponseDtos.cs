using Etmen.Domain.Enums;

namespace Etmen.API.DTOs.Response;

/// <summary>Used in Login/Register responses.</summary>
public sealed record AuthResponse(
    string AccessToken, string RefreshToken, DateTime ExpiresAt,
    Guid UserId, string Role, string FullName);

/// <summary>Patient profile endpoint response.</summary>
public sealed record PatientProfileResponse(
    Guid PatientId, string FullName, int Age, double BMI,
    bool SmokingStatus, double LatestRiskScore, string RiskLevel);

/// <summary>Risk assessment endpoint response — includes analyses and doctor suggestions.</summary>
public sealed record RiskAssessmentResponse(
    Guid AssessmentId, double Score, string RiskLevel,
    RequiredAnalysisResponse[] RequiredAnalyses,
    string[] RecommendedDoctors, string[] Recommendations);

/// <summary>GET /api/admin/dashboard</summary>
public sealed record AdminDashboardResponse(
    int TotalPatients, int HighRiskCount, int MediumRiskCount,
    int LowRiskCount, int UrgentAlerts);

/// <summary>Chat message response.</summary>
public sealed record ChatMessageResponse(
    Guid MessageId, string SenderName, string SenderRole,
    string Text, DateTime SentAt, bool IsRead);

/// <summary>Doctor conversation summary for the conversation list.</summary>
public sealed record ConversationResponse(
    Guid PatientId, string PatientName, string RiskLevel,
    string LastMessage, int UnreadCount);

/// <summary>Lab test recommendation from the AI engine.</summary>
public sealed record RequiredAnalysisResponse(
    string Name, string Description, AnalysisPriority Priority, string Icon);
