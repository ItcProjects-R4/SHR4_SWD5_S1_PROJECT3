using Etmen.Application.DTOs.Internal;
using Etmen.Application.Services;
using Microsoft.Extensions.Configuration;

namespace Etmen.Infrastructure.AI;

/// <summary>
/// Implements ILlmPatientChatService.
/// Supports: Anthropic Claude, OpenAI GPT, Azure OpenAI.
/// Configure via appsettings: LlmChat:Provider, LlmChat:ApiKey, LlmChat:Model.
/// Builds a context-aware system prompt from PatientContext before each API call.
/// </summary>
public sealed class LlmPatientChatService : ILlmPatientChatService
{
    private readonly HttpClient     _http;
    private readonly IConfiguration _config;

    public LlmPatientChatService(HttpClient http, IConfiguration config)
    {
        _http   = http;
        _config = config;
    }

    public Task<PatientChatResponse> AskAsync(
        string patientMessage,
        PatientContext context,
        ChatHistory history,
        CancellationToken ct = default)
        => throw new NotImplementedException();

    /// <summary>
    /// Builds the context-aware system prompt injected before every LLM request.
    /// Includes patient risk level, vitals, primary issue, and safety guardrails.
    /// </summary>
    private static string BuildSystemPrompt(PatientContext ctx)
        => throw new NotImplementedException();
}
