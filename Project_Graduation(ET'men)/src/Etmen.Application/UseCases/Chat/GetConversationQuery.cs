using MediatR;

namespace Etmen.Application.UseCases.Chat;

/// <summary>Query: returns all messages for a patient-doctor conversation pair.</summary>
public sealed record GetConversationQuery(Guid PatientId, Guid DoctorId) : IRequest<IEnumerable<ChatMessageDto>>;

public sealed class GetConversationQueryHandler : IRequestHandler<GetConversationQuery, IEnumerable<ChatMessageDto>>
{
    public GetConversationQueryHandler() { throw new NotImplementedException(); }
    public Task<IEnumerable<ChatMessageDto>> Handle(GetConversationQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
