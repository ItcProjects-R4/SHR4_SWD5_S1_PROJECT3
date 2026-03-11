using Etmen.Application.DTOs.Internal;

namespace Etmen.Application.Services;

/// <summary>
/// Contract for LLM-powered patient chat assistant.
/// Implemented by LlmPatientChatService (Anthropic Claude / OpenAI GPT).
/// </summary>
public interface ILlmPatientChatService
{
    Task<PatientChatResponse> AskAsync(
        string patientMessage,
        PatientContext context,
        ChatHistory history,
        CancellationToken ct = default);
}
