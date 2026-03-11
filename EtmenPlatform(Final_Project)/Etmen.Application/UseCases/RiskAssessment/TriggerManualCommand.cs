using MediatR;
using Etmen.Application.DTOs.Response;

namespace Etmen.Application.UseCases.RiskAssessment;

/// <summary>Manually triggers an AI risk assessment outside the scheduled cycle.</summary>
public sealed record TriggerManualCommand(Guid Id) : IRequest<RiskAssessmentResponse>;

public sealed class TriggerManualCommandHandler : IRequestHandler<TriggerManualCommand, RiskAssessmentResponse>
{
    public Task<RiskAssessmentResponse> Handle(TriggerManualCommand request, CancellationToken ct)
        => throw new NotImplementedException();
}
