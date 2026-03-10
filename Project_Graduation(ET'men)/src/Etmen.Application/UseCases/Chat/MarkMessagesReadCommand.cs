using MediatR;

namespace Etmen.Application.UseCases.Chat;

/// <summary>Command: bulk-marks all unread messages as IsRead=true for a given reader.</summary>
public sealed record MarkMessagesReadCommand(Guid ConversationPatientId, Guid ReaderId) : IRequest<bool>;

public sealed class MarkMessagesReadCommandHandler : IRequestHandler<MarkMessagesReadCommand, bool>
{
    public MarkMessagesReadCommandHandler() { throw new NotImplementedException(); }
    public Task<bool> Handle(MarkMessagesReadCommand request, CancellationToken ct)
        => throw new NotImplementedException();
}
