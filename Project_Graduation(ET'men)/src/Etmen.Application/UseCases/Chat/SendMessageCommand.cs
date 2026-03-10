using Etmen.Domain.Enums;
using MediatR;

namespace Etmen.Application.UseCases.Chat;

/// <summary>Command: persists a chat message to the DB and publishes a SignalR event.</summary>
public sealed record SendMessageCommand(
    Guid SenderId, Guid RecipientId, Guid PatientId,
    string Text, UserRole SenderRole) : IRequest<ChatMessageDto>;

public sealed record ChatMessageDto(
    Guid MessageId, string SenderName, string SenderRole,
    string Text, DateTime SentAt, bool IsRead);

public sealed class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, ChatMessageDto>
{
    public SendMessageCommandHandler() { throw new NotImplementedException(); }
    public Task<ChatMessageDto> Handle(SendMessageCommand request, CancellationToken ct)
        => throw new NotImplementedException();
}
