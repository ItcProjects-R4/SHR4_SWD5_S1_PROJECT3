using Etmen.Application.DTOs.Request;
using Etmen.Application.UseCases.MedicalRecord;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>Medical record submission and doctor note management.</summary>
[ApiController]
[Route("api/medical-records")]
[Authorize]
public sealed class MedicalRecordController : ControllerBase
{
    private readonly IMediator _mediator;
    public MedicalRecordController(IMediator mediator) => _mediator = mediator;

    /// <summary>POST — Patient submits health data → triggers AI scoring.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateRecordAsync([FromBody] CreateMedicalRecordRequest request, CancellationToken ct)
        => Ok(await _mediator.Send(new CreateMedicalRecordCommand(request), ct));

    [HttpGet("{id}")]
    public IActionResult GetRecordByIdAsync(Guid id) => throw new NotImplementedException();

    [HttpGet("patient/{id}")]
    public IActionResult GetPatientRecordsAsync(Guid id) => throw new NotImplementedException();

    [HttpPost("{id}/notes")]
    [Authorize(Roles = "Doctor")]
    public IActionResult AddDoctorNoteAsync(Guid id) => throw new NotImplementedException();
}
