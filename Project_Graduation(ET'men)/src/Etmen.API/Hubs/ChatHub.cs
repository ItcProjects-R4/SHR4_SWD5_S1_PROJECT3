using Etmen.API.DTOs.Request;
using Etmen.Application.Interfaces;
using Etmen.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Etmen.API.Hubs;

/// <summary>
/// SignalR hub for real-time Doctor–Patient messaging.
/// Authentication is required — the Hub uses Context.UserIdentifier (NameIdentifier claim)
/// to route messages to the correct connection group.
///
/// Client events emitted:
///   - "ReceiveMessage" → pushed to recipient's user group on SendMessage
///
/// NOTE: This is the Doctor–Patient chat only. The AI Chat Assistant (AIChatController)
/// is completely separate and does not use SignalR.
/// </summary>
[Authorize]
public class ChatHub : Hub
{
    private readonly IChatRepository _repo;

    public ChatHub(IChatRepository repo) => _repo = repo;

    /// <summary>
    /// Called by Doctor or Patient clients to send a message.
    /// Persists the message via IChatRepository and pushes "ReceiveMessage" to the recipient.
    /// </summary>
    public async Task SendMessage(SendMessageRequest dto)
    {
        throw new NotImplementedException();
    }

    /// <summary>Adds the connected user to their personal SignalR group (userId as group key).</summary>
    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        await Groups.AddToGroupAsync(Context.ConnectionId, userId!);
        await base.OnConnectedAsync();
    }

    /// <summary>Removes the user from their group on disconnect.</summary>
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.UserIdentifier;
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId!);
        await base.OnDisconnectedAsync(exception);
    }
}
