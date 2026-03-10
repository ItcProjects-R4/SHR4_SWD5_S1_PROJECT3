using Etmen.Application.UseCases.MedicalRecord;
using Microsoft.AspNetCore.Http;
using MediatR;

namespace Etmen.Application.UseCases.Lab;

/// <summary>Command: store file → OCR → create MedicalRecord → re-run AI → notify if score changed >5%.</summary>
public sealed record UploadLabResultCommand(Guid PatientId, IFormFile File) : IRequest<RiskAssessmentResultDto>;

public sealed class UploadLabResultCommandHandler : IRequestHandler<UploadLabResultCommand, RiskAssessmentResultDto>
{
    public UploadLabResultCommandHandler() { throw new NotImplementedException(); }
    public Task<RiskAssessmentResultDto> Handle(UploadLabResultCommand request, CancellationToken ct)
        => throw new NotImplementedException();
}
