using Etmen.Application.DTOs.Request;
using Etmen.Application.DTOs.Response;
using MediatR;

namespace Etmen.Application.UseCases.Lab;

/// <summary>Uploads and OCR-processes a lab result PDF/image. Extracts blood values and triggers risk recalculation.</summary>
public sealed record UploadLabResultCommand(UploadLabResultRequest Request) : IRequest<RiskAssessmentResponse>;

public sealed class UploadLabResultCommandHandler : IRequestHandler<UploadLabResultCommand, RiskAssessmentResponse>
{
    public Task<RiskAssessmentResponse> Handle(UploadLabResultCommand request, CancellationToken ct)
        => throw new NotImplementedException();
}
