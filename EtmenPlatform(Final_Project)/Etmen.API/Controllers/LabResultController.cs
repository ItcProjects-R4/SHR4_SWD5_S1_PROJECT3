using Etmen.Application.DTOs.Request;
using Etmen.Application.UseCases.Lab;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>Lab result OCR upload endpoint (NEW v3.0).</summary>
[ApiController]
[Route("api/lab-results")]
[Authorize(Roles = "Patient")]
public sealed class LabResultController : ControllerBase
{
    private readonly IMediator _mediator;
    public LabResultController(IMediator mediator) => _mediator = mediator;

    /// <summary>POST /api/lab-results/upload — Accept PDF/image, run OCR, re-score risk.</summary>
    [HttpPost("upload")]
    public async Task<IActionResult> UploadLabResultAsync(
        [FromForm] IFormFile file,
        [FromForm] Guid patientId,
        CancellationToken ct)
        => Ok(await _mediator.Send(new UploadLabResultCommand(
            new UploadLabResultRequest { PatientId = patientId, File = file }), ct));
}
