using Etmen.Application.DTOs.Request;
using Etmen.Application.UseCases.AIChat;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>LLM-powered patient chat assistant endpoint (NEW v3.0).</summary>
[ApiController]
[Route("api/ai-chat")]
[Authorize(Roles = "Patient")]
public sealed class AIChatController : ControllerBase
{
    private readonly IMediator _mediator;
    public AIChatController(IMediator mediator) => _mediator = mediator;

    /// <summary>POST /api/ai-chat/ask — Send message + history, get plain-language AI reply.</summary>
    [HttpPost("ask")]
    public async Task<IActionResult> AskAsync([FromBody] AIChatRequest request, CancellationToken ct)
        => Ok(await _mediator.Send(new AskPatientChatQuery(request), ct));
}
