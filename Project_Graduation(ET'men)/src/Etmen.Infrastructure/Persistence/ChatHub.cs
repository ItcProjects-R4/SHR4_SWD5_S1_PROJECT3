using Etmen.Application.Interfaces;
using Etmen.Domain.Entities;
using Etmen.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Etmen.Infrastructure.Persistence;

/// <summary>
/// SignalR hub for real-time Doctor–Patient messaging.
/// Clients authenticate via JWT passed as query string (?access_token=...).
/// Each user is added to a group keyed by their UserId for targeted pushes.
/// </summary>
[Authorize]
public class ChatHub : Hub
{
    private readonly IChatRepository _repo;
    public ChatHub(IChatRepository repo) => _repo = repo;

    /// <summary>Persists the message then pushes it to the recipient's connection group.</summary>
    public async Task SendMessage(SendMessageDto dto)
    {
        var msg = ChatMessage.Create(dto.SenderId, dto.RecipientId,
                                     dto.PatientId, dto.Text, dto.SenderRole);
        await _repo.SaveMessageAsync(msg);
        await Clients.User(dto.RecipientId.ToString()).SendAsync("ReceiveMessage", msg);
    }

    /// <summary>Adds the authenticated user's connection to their personal group on connect.</summary>
    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        await Groups.AddToGroupAsync(Context.ConnectionId, userId!);
        await base.OnConnectedAsync();
    }
}

/// <summary>DTO received from the SignalR client when sending a message.</summary>
public sealed record SendMessageDto(
    Guid SenderId, Guid RecipientId, Guid PatientId,
    string Text, UserRole SenderRole);
