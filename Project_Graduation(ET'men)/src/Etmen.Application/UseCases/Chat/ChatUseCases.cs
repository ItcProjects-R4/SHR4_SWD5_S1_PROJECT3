using Etmen.Application.Interfaces;
using Etmen.Application.Services;
using Etmen.Domain.Entities;
using MediatR;

namespace Etmen.Application.UseCases.Chat;

// ════════════════════════════════════════════════════════════════════════════
// GetConversationQuery
// ════════════════════════════════════════════════════════════════════════════

/// <summary>Returns all messages for a patient-doctor pair, ordered chronologically.</summary>
public sealed record GetConversationQuery(Guid PatientId, Guid DoctorId) : IRequest<IEnumerable<ChatMessage>>;

public sealed class GetConversationQueryHandler : IRequestHandler<GetConversationQuery, IEnumerable<ChatMessage>>
{
    private readonly IChatRepository _chatRepo;

    public GetConversationQueryHandler(IChatRepository chatRepo) => _chatRepo = chatRepo;

    public async Task<IEnumerable<ChatMessage>> Handle(GetConversationQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// SendMessageCommand
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Persists a new chat message and publishes a SignalR push event to the recipient.
/// NOTE: For real-time delivery, ChatHub.cs calls IChatRepository directly via WebSocket.
/// This command handles REST fallback and audit logging.
/// </summary>
public sealed record SendMessageCommand(
    Guid SenderId, Guid RecipientId, Guid PatientId,
    string Text, string SenderRole
) : IRequest<ChatMessage>;

public sealed class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, ChatMessage>
{
    private readonly IChatRepository _chatRepo;

    public SendMessageCommandHandler(IChatRepository chatRepo) => _chatRepo = chatRepo;

    public async Task<ChatMessage> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// GetDoctorConversationsQuery
// ════════════════════════════════════════════════════════════════════════════

/// <summary>Returns the list of all patient conversations for a doctor, with unread counts.</summary>
public sealed record GetDoctorConversationsQuery(Guid DoctorId) : IRequest<IEnumerable<ConversationSummary>>;

public sealed class GetDoctorConversationsQueryHandler : IRequestHandler<GetDoctorConversationsQuery, IEnumerable<ConversationSummary>>
{
    private readonly IChatRepository _chatRepo;
    private readonly IPatientRepository _patientRepo;

    public GetDoctorConversationsQueryHandler(IChatRepository chatRepo, IPatientRepository patientRepo)
    {
        _chatRepo = chatRepo;
        _patientRepo = patientRepo;
    }

    public async Task<IEnumerable<ConversationSummary>> Handle(GetDoctorConversationsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// MarkMessagesReadCommand
// ════════════════════════════════════════════════════════════════════════════

/// <summary>Bulk-marks all unread messages in a conversation as IsRead=true for the given reader.</summary>
public sealed record MarkMessagesReadCommand(Guid ConversationPatientId, Guid ReaderId) : IRequest<Unit>;

public sealed class MarkMessagesReadCommandHandler : IRequestHandler<MarkMessagesReadCommand, Unit>
{
    private readonly IChatRepository _chatRepo;

    public MarkMessagesReadCommandHandler(IChatRepository chatRepo) => _chatRepo = chatRepo;

    public async Task<Unit> Handle(MarkMessagesReadCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
