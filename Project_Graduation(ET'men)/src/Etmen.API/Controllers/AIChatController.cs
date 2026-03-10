using Etmen.Application.Services;
using Etmen.Application.UseCases.AIChat;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>
/// AI Chat Assistant endpoint (v3.0). Route: /api/ai-chat
/// Completely separate from the Doctor–Patient SignalR chat.
/// Powered by an LLM (Anthropic/OpenAI) with context-aware patient data injected.
/// </summary>
[ApiController]
[Route("api/ai-chat")]
[Authorize(Roles = "Patient")]
public class AIChatController : ControllerBase
{
    private readonly IMediator _mediator;
    public AIChatController(IMediator mediator) => _mediator = mediator;

    /// <summary>POST /api/ai-chat/ask — Returns LLM reply with optional doctor escalation flag.</summary>
    [HttpPost("ask")]
    public async Task<IActionResult> AskAsync([FromBody] AIChatRequest request, CancellationToken ct)
        => Ok(await _mediator.Send(new AskPatientChatQuery(request.PatientId, request.Message, request.History), ct));
}

public sealed record AIChatRequest(Guid PatientId, string Message, ChatHistory History);
