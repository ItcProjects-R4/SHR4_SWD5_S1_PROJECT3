using Etmen.Application.DTOs.Request;
using Etmen.Application.DTOs.Response;
using MediatR;

namespace Etmen.Application.UseCases.MedicalRecord;

/// <summary>Creates a new medical record and triggers an AI risk assessment automatically.</summary>
public sealed record CreateMedicalRecordCommand(CreateMedicalRecordRequest Request) : IRequest<RiskAssessmentResponse>;

public sealed class CreateMedicalRecordCommandHandler : IRequestHandler<CreateMedicalRecordCommand, RiskAssessmentResponse>
{
    public Task<RiskAssessmentResponse> Handle(CreateMedicalRecordCommand request, CancellationToken ct)
        => throw new NotImplementedException();
}
