using Etmen.Application.DTOs.Request;
using Etmen.Application.DTOs.Response;
using MediatR;

namespace Etmen.Application.UseCases.AIChat;

/// <summary>Sends a patient message to the LLM with full medical context. Returns AI response.</summary>
public sealed record AskPatientChatQuery(AIChatRequest Request) : IRequest<AIChatResponse>;

public sealed class AskPatientChatQueryHandler : IRequestHandler<AskPatientChatQuery, AIChatResponse>
{
    public Task<AIChatResponse> Handle(AskPatientChatQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
