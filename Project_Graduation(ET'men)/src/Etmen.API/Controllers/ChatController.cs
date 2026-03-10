using Etmen.Application.UseCases.Chat;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>
/// Doctor–Patient chat REST endpoints. Route: /api/chat
/// Real-time push is handled by ChatHub (SignalR) — this controller covers persistence and history.
/// </summary>
[ApiController]
[Route("api/chat")]
[Authorize]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;
    public ChatController(IMediator mediator) => _mediator = mediator;

    /// <summary>GET /api/chat/conversation/{patientId} — Full message history for a patient.</summary>
    [HttpGet("conversation/{patientId:guid}")]
    public async Task<IActionResult> GetConversationAsync(Guid patientId,
        [FromQuery] Guid doctorId, CancellationToken ct)
        => Ok(await _mediator.Send(new GetConversationQuery(patientId, doctorId), ct));

    /// <summary>POST /api/chat/send — Persist a new message to DB.</summary>
    [HttpPost("send")]
    public async Task<IActionResult> SendMessageAsync([FromBody] SendMessageCommand command, CancellationToken ct)
        => Ok(await _mediator.Send(command, ct));

    /// <summary>GET /api/chat/conversations — Doctor: list all patient conversations with unread counts.</summary>
    [HttpGet("conversations")]
    [Authorize(Roles = "Doctor,Admin")]
    public async Task<IActionResult> GetConversationsAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    /// <summary>POST /api/chat/{conversationId}/read — Mark all messages as read.</summary>
    [HttpPost("{conversationId:guid}/read")]
    public async Task<IActionResult> MarkAsReadAsync(Guid conversationId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
