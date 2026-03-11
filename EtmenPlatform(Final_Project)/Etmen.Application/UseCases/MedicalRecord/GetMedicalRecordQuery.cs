using MediatR;
using Etmen.Application.DTOs.Response;

namespace Etmen.Application.UseCases.MedicalRecord;

/// <summary>Returns a single medical record with its associated risk assessment.</summary>
public sealed record GetMedicalRecordQuery(Guid RecordId) : IRequest<RiskAssessmentResponse>;

public sealed class GetMedicalRecordQueryHandler : IRequestHandler<GetMedicalRecordQuery, RiskAssessmentResponse>
{
    public Task<RiskAssessmentResponse> Handle(GetMedicalRecordQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
