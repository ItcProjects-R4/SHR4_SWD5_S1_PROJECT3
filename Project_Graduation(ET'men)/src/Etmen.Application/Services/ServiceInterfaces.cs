using Etmen.Domain.Entities;
using Etmen.Domain.Enums;
using Etmen.Domain.ValueObjects;

namespace Etmen.Application.Services;

// ════════════════════════════════════════════════════════════════════════════
// IRiskCalculationService
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Abstraction over the ML inference engine (ML.NET or Python FastAPI).
/// Implemented by Infrastructure/AI/MLModelService.cs.
/// </summary>
public interface IRiskCalculationService
{
    /// <summary>Runs the trained model and returns a probability score with metadata.</summary>
    Task<RiskPredictionResult> CalculateRiskAsync(RiskFeatures features);

    /// <summary>Converts a raw probability score to a discrete RiskLevel bucket.</summary>
    RiskLevel ClassifyRisk(double probabilityScore);

    /// <summary>Returns the list of required lab analyses for a given risk level and features.</summary>
    List<RequiredAnalysis> GetRequiredAnalyses(RiskLevel level, RiskFeatures features);

    /// <summary>Returns recommended doctor specialities based on risk level.</summary>
    List<RecommendedDoctor> GetRecommendedDoctors(RiskLevel level);

    /// <summary>Returns personalised text recommendations for the patient result page.</summary>
    List<string> GetRecommendations(RiskLevel level, RiskFeatures features);
}

// ════════════════════════════════════════════════════════════════════════════
// INotificationService
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Sends alerts, emails, and push notifications.
/// Implemented by Infrastructure/Notifications/EmailNotificationService.cs.
/// </summary>
public interface INotificationService
{
    Task SendAlertToPatientAsync(Guid patientId, AlertMessage message, CancellationToken ct = default);
    Task SendAlertToDoctorAsync(Guid doctorId, AlertMessage message, CancellationToken ct = default);
    Task SendEmailAsync(string toEmail, string subject, string body, CancellationToken ct = default);
    Task MarkAlertAsReadAsync(Guid alertId, CancellationToken ct = default);
}

// ════════════════════════════════════════════════════════════════════════════
// ITokenService
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Issues and validates JWT access tokens and opaque refresh tokens.
/// Implemented by Infrastructure/Auth/JwtTokenService.cs.
/// </summary>
public interface ITokenService
{
    /// <summary>Generates a short-lived HS256 JWT (15 min) containing role, email, and name claims.</summary>
    string GenerateAccessToken(User user);

    /// <summary>Generates a 64-byte cryptographically random refresh token (base64).</summary>
    string GenerateRefreshToken();

    /// <summary>Validates a JWT and returns the ClaimsPrincipal, or null if invalid/expired.</summary>
    System.Security.Claims.ClaimsPrincipal? ValidateToken(string token);

    /// <summary>Generates a scoped JWT for family profile switching (v3.0).</summary>
    string GenerateScopedToken(User primaryUser, Guid targetPatientId);
}

// ════════════════════════════════════════════════════════════════════════════
// IChatService
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Application-level orchestration for the Doctor–Patient real-time chat.
/// Works alongside ChatHub.cs (SignalR) — ChatHub calls SaveMessageAsync via IChatRepository directly.
/// </summary>
public interface IChatService
{
    Task<IEnumerable<ChatMessage>> GetConversationAsync(Guid patientId, Guid doctorId, CancellationToken ct = default);
    Task<ChatMessage> SendMessageAsync(Guid senderId, Guid recipientId, Guid patientId, string text, string senderRole, CancellationToken ct = default);
    Task MarkAsReadAsync(Guid conversationPatientId, Guid readerId, CancellationToken ct = default);
    Task<IEnumerable<ConversationSummary>> GetDoctorConversationsAsync(Guid doctorId, CancellationToken ct = default);
}

// ════════════════════════════════════════════════════════════════════════════
// IGeoSearchService  (NEW — v3.0)
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Abstracts geographic provider search (Google Places API + registered DB providers).
/// Implemented by Infrastructure/Geo/GoogleGeoSearchService.cs.
/// </summary>
public interface IGeoSearchService
{
    /// <summary>Returns nearby providers ranked by the MatchScore algorithm (specialty 40%, distance 30%, rating 20%, availability 10%).</summary>
    Task<IEnumerable<NearbyProvider>> SearchProvidersAsync(GeoSearchCriteria criteria, CancellationToken ct = default);

    /// <summary>Fetches full details for a Google Places provider by placeId.</summary>
    Task<ProviderDetails> GetProviderDetailsAsync(string googlePlaceId, CancellationToken ct = default);

    /// <summary>Converts a human-readable address to lat/lng coordinates.</summary>
    Task<GeoCoordinate?> GeocodeAddressAsync(string address, CancellationToken ct = default);

    /// <summary>Calculates the Haversine distance in metres between two coordinates.</summary>
    double CalculateDistanceMeters(GeoCoordinate from, GeoCoordinate to);
}

// ════════════════════════════════════════════════════════════════════════════
// IOcrService  (NEW — v3.0)
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Extracts structured values from a lab result image/PDF.
/// Implemented by Infrastructure/AI/OcrService.cs (Tesseract or Azure Computer Vision).
/// </summary>
public interface IOcrService
{
    /// <summary>Extracts raw text and key health values from the supplied file stream.</summary>
    Task<OcrResult> ExtractLabValuesAsync(Stream fileStream, string fileName, CancellationToken ct = default);
}

// ════════════════════════════════════════════════════════════════════════════
// ILlmPatientChatService  (NEW — v3.0)
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Sends patient questions to an LLM (Anthropic Claude or OpenAI) with patient context
/// injected into the system prompt. Implemented by Infrastructure/AI/LlmPatientChatService.cs.
/// </summary>
public interface ILlmPatientChatService
{
    /// <summary>
    /// Sends a patient message to the LLM and returns a plain-language reply.
    /// SuggestDoctorChat=true when the answer requires a clinical decision.
    /// DetectedCrisis=true when crisis language is detected.
    /// </summary>
    Task<PatientChatResponse> AskAsync(
        string patientMessage,
        PatientContext context,
        ChatHistory history,
        CancellationToken ct = default);
}

// ════════════════════════════════════════════════════════════════════════════
// IFamilyService  (NEW — v3.0)
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Orchestrates family account linking, invite flow, and profile switching.
/// Implemented by a concrete class in the Application layer (or Infrastructure if needed).
/// </summary>
public interface IFamilyService
{
    Task<IEnumerable<FamilyLink>> GetLinkedProfilesAsync(Guid primaryUserId, CancellationToken ct = default);
    Task<string> InviteMemberAsync(Guid primaryUserId, string memberEmail, FamilyRelationship relationship, bool canManage, CancellationToken ct = default);
    Task<FamilyLink> AcceptInviteAsync(string inviteToken, Guid linkedUserId, CancellationToken ct = default);
    Task<string> SwitchProfileAsync(Guid primaryUserId, Guid linkedUserId, CancellationToken ct = default);
    Task RemoveLinkAsync(Guid linkId, Guid requestingUserId, CancellationToken ct = default);
}

// ════════════════════════════════════════════════════════════════════════════
// Supporting DTOs / Records used by service interfaces
// ════════════════════════════════════════════════════════════════════════════

/// <summary>Input feature vector passed to the ML risk model.</summary>
public sealed record RiskFeatures(
    int Age, double BMI, double BloodSugar,
    double BloodPressureSystolic, double BloodPressureDiastolic,
    bool IsSmoker, bool FamilyHistory, int ActivityLevel);

/// <summary>Output of the ML inference call.</summary>
public sealed record RiskPredictionResult(double Score, string? PrimaryIssue);

/// <summary>A single required lab test or clinical analysis.</summary>
public sealed record RequiredAnalysis(string Name, string Description, string Priority, string Icon);

/// <summary>A recommended doctor speciality derived from the risk level.</summary>
public sealed record RecommendedDoctor(string Speciality, string Reason);

/// <summary>Alert payload sent to patient / doctor notification channels.</summary>
public sealed record AlertMessage(string Title, string Body, Guid? RelatedAssessmentId = null);

/// <summary>Summary view of a doctor's conversation list (unread counts, last message).</summary>
public sealed record ConversationSummary(
    Guid PatientId, string PatientName, RiskLevel RiskLevel,
    string? LastMessage, int UnreadCount);

/// <summary>Criteria for the Nearby Finder geographic search.</summary>
public sealed record GeoSearchCriteria(
    double Latitude, double Longitude, double RadiusMeters,
    string? Specialty, ProviderType? ProviderType);

/// <summary>A provider returned by the Nearby Finder search.</summary>
public sealed record NearbyProvider(
    Guid? PlatformId, string? GooglePlaceId,
    string Name, string Address, double DistanceMeters,
    double Rating, bool IsRegistered, bool IsOpenNow,
    double MatchScore);

/// <summary>Full detail view of a provider from Google Places.</summary>
public sealed record ProviderDetails(
    string GooglePlaceId, string Name, string Address,
    string? Phone, string? Website, double Rating,
    IEnumerable<string> OpeningHours);

/// <summary>Structured output from the OCR extraction service.</summary>
public sealed record OcrResult(
    string RawText,
    double? HbA1c, double? BloodSugar,
    double? Cholesterol, double? Triglycerides, double? Creatinine);

/// <summary>Patient health context injected into the LLM system prompt.</summary>
public sealed record PatientContext(
    string PatientName, RiskLevel RiskLevel, double RiskScore,
    string BloodPressure, double BloodSugar, double BMI, string? PrimaryIssue);

/// <summary>Conversation history passed to the LLM for context continuity (max 10 messages).</summary>
public sealed record ChatHistory(IEnumerable<ChatHistoryMessage> Messages);
public sealed record ChatHistoryMessage(string Role, string Text);

/// <summary>LLM response returned by ILlmPatientChatService.AskAsync.</summary>
public sealed record PatientChatResponse(
    string Reply,
    bool SuggestDoctorChat,
    bool DetectedCrisis);
