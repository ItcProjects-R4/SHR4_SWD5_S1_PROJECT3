using Etmen.Application.UseCases.Lab;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>
/// Lab result OCR upload pipeline (v3.0). Route: /api/lab-results
/// POST triggers: blob storage → OCR → MedicalRecord → AI re-score → notify.
/// </summary>
[ApiController]
[Route("api/lab-results")]
[Authorize(Roles = "Patient")]
public class LabResultController : ControllerBase
{
    private readonly IMediator _mediator;
    public LabResultController(IMediator mediator) => _mediator = mediator;

    /// <summary>POST /api/lab-results/upload — Accepts PDF or image, runs full OCR pipeline.</summary>
    [HttpPost("upload")]
    public async Task<IActionResult> UploadLabResultAsync(
        [FromForm] IFormFile file, [FromForm] Guid patientId, CancellationToken ct)
        => Ok(await _mediator.Send(new UploadLabResultCommand(patientId, file), ct));
}
