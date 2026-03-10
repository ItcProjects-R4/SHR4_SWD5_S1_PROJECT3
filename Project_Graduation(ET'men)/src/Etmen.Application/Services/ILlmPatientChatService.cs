namespace Etmen.Application.Services;

/// <summary>
/// Contract for the AI Chat Assistant widget (v3.0).
/// Completely separate from the Doctor–Patient SignalR chat.
/// Implemented by LlmPatientChatService in Infrastructure/AI.
/// </summary>
public interface ILlmPatientChatService
{
    Task<PatientChatResponse> AskAsync(
        string         patientMessage,
        PatientContext context,
        ChatHistory    history,
        CancellationToken ct = default);
}

public sealed record PatientContext(
    string PatientName, string RiskLevel, double RiskScore,
    string BloodPressure, double BloodSugar, double BMI, string PrimaryIssue);

public sealed record ChatHistory(List<ChatTurn> Turns);
public sealed record ChatTurn(string Role, string Text);

public sealed record PatientChatResponse(
    string Reply,
    bool   SuggestDoctorChat,  // true if question needs clinical decision
    bool   DetectedCrisis      // true if message contains crisis language
);
