using Etmen.Application.UseCases.MedicalRecord;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>
/// Medical record submission and retrieval. Route: /api/medical-records
/// POSTing a record triggers the full AI scoring pipeline.
/// </summary>
[ApiController]
[Route("api/medical-records")]
[Authorize]
public class MedicalRecordController : ControllerBase
{
    private readonly IMediator _mediator;
    public MedicalRecordController(IMediator mediator) => _mediator = mediator;

    /// <summary>POST /api/medical-records — Patient submits health data → triggers AI scoring.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateRecordAsync([FromBody] CreateMedicalRecordCommand command, CancellationToken ct)
        => Ok(await _mediator.Send(command, ct));

    /// <summary>GET /api/medical-records/{id} — Single record with embedded risk assessment.</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRecordByIdAsync(Guid id, CancellationToken ct)
        => Ok(await _mediator.Send(new GetMedicalRecordQuery(id), ct));

    /// <summary>GET /api/medical-records/patient/{id} — Paginated medical history.</summary>
    [HttpGet("patient/{id:guid}")]
    public async Task<IActionResult> GetPatientRecordsAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    /// <summary>POST /api/medical-records/{id}/notes — Doctor appends clinical note.</summary>
    [HttpPost("{id:guid}/notes")]
    [Authorize(Roles = "Doctor,Admin")]
    public async Task<IActionResult> AddDoctorNoteAsync(Guid id,
        [FromBody] AddDoctorNoteCommand command, CancellationToken ct)
        => Ok(await _mediator.Send(command with { RecordId = id }, ct));
}
