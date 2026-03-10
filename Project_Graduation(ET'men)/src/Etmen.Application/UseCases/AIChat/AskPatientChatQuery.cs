using Etmen.Application.Services;
using MediatR;

namespace Etmen.Application.UseCases.AIChat;

/// <summary>Query: builds patient context, calls LLM, and escalates to doctor if crisis detected.</summary>
public sealed record AskPatientChatQuery(Guid PatientId, string Message, ChatHistory History) : IRequest<PatientChatResponse>;

public sealed class AskPatientChatQueryHandler : IRequestHandler<AskPatientChatQuery, PatientChatResponse>
{
    public AskPatientChatQueryHandler() { throw new NotImplementedException(); }
    public Task<PatientChatResponse> Handle(AskPatientChatQuery request, CancellationToken ct) => throw new NotImplementedException();
}
