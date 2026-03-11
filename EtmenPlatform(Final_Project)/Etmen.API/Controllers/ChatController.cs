using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>Doctor–Patient internal chat REST endpoints (SignalR hub at /hubs/chat).</summary>
[ApiController]
[Route("api/chat")]
[Authorize]
public sealed class ChatController : ControllerBase
{
    private readonly IMediator _mediator;
    public ChatController(IMediator mediator) => _mediator = mediator;

    [HttpGet("conversation/{patientId}")]
    public IActionResult GetConversationAsync(Guid patientId) => throw new NotImplementedException();

    [HttpPost("send")]
    public IActionResult SendMessageAsync() => throw new NotImplementedException();

    [HttpGet("conversations")]
    [Authorize(Roles = "Doctor,Admin")]
    public IActionResult GetConversationsAsync() => throw new NotImplementedException();

    [HttpPost("{conversationId}/read")]
    public IActionResult MarkAsReadAsync(Guid conversationId) => throw new NotImplementedException();
}
