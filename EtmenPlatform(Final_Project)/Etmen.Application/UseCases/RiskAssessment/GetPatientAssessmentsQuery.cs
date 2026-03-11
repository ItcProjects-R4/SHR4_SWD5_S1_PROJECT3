using MediatR;
using Etmen.Application.DTOs.Response;

namespace Etmen.Application.UseCases.RiskAssessment;

/// <summary>Returns all risk assessments for a patient, newest first.</summary>
public sealed record GetPatientAssessmentsQuery(Guid Id) : IRequest<RiskAssessmentResponse>;

public sealed class GetPatientAssessmentsQueryHandler : IRequestHandler<GetPatientAssessmentsQuery, RiskAssessmentResponse>
{
    public Task<RiskAssessmentResponse> Handle(GetPatientAssessmentsQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
