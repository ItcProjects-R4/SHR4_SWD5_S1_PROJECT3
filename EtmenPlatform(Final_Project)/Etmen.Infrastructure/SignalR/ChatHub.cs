using Etmen.Application.Interfaces;
using Etmen.Domain.Entities;
using Etmen.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Etmen.Infrastructure.SignalR;

/// <summary>
/// SignalR Hub for real-time Doctor–Patient messaging.
/// JWT is passed via query string (?access_token=...) for WebSocket upgrade.
/// Each user joins a group matching their UserId.
/// Route: /hubs/chat
/// </summary>
[Authorize]
public sealed class ChatHub : Hub
{
    private readonly IChatRepository _repo;

    public ChatHub(IChatRepository repo) => _repo = repo;

    /// <summary>Persist message to DB and push to recipient's group.</summary>
    public async Task SendMessage(SendMessageDto dto)
    {
        var msg = ChatMessage.Create(
            dto.SenderId,
            dto.RecipientId,
            dto.PatientId,
            dto.Text,
            dto.SenderRole);

        await _repo.SaveMessageAsync(msg);
        await Clients.User(dto.RecipientId.ToString()).SendAsync("ReceiveMessage", msg);
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier ?? Context.ConnectionId;
        await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.UserIdentifier ?? Context.ConnectionId;
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
        await base.OnDisconnectedAsync(exception);
    }
}

/// <summary>DTO received from the SignalR client when sending a message.</summary>
public sealed class SendMessageDto
{
    public Guid     SenderId    { get; set; }
    public Guid     RecipientId { get; set; }
    public Guid     PatientId   { get; set; }
    public string   Text        { get; set; } = string.Empty;
    public UserRole SenderRole  { get; set; }
}
