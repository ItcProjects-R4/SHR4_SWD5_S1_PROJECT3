using Etmen.Application.UseCases.Patient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>
/// Patient profile management and risk history. Route: /api/patients
/// </summary>
[ApiController]
[Route("api/patients")]
[Authorize]
public class PatientController : ControllerBase
{
    private readonly IMediator _mediator;
    public PatientController(IMediator mediator) => _mediator = mediator;

    /// <summary>GET /api/patients/{id} — Returns profile + latest risk score. Roles: Patient, Doctor, Admin.</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProfileAsync(Guid id, CancellationToken ct)
        => Ok(await _mediator.Send(new GetPatientProfileQuery(id), ct));

    /// <summary>PUT /api/patients/{id} — Updates health metrics. Roles: Patient (own), Admin.</summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProfileAsync(Guid id,
        [FromBody] UpdatePatientProfileCommand command, CancellationToken ct)
        => Ok(await _mediator.Send(command with { PatientId = id }, ct));

    /// <summary>GET /api/patients/{id}/risk-history — Paginated past assessments. Roles: Patient, Doctor.</summary>
    [HttpGet("{id:guid}/risk-history")]
    public async Task<IActionResult> GetMyRiskHistoryAsync(Guid id, CancellationToken ct)
        => Ok(await _mediator.Send(new GetPatientRiskHistoryQuery(id), ct));

    /// <summary>GET /api/patients — All patients. Roles: Doctor, Admin.</summary>
    [HttpGet]
    [Authorize(Roles = "Doctor,Admin")]
    public async Task<IActionResult> GetAllPatientsAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    /// <summary>GET /api/patients/high-risk — Patients with RiskLevel.High. Roles: Doctor, Admin.</summary>
    [HttpGet("high-risk")]
    [Authorize(Roles = "Doctor,Admin")]
    public async Task<IActionResult> GetHighRiskPatientsAsync(CancellationToken ct)
        => Ok(await _mediator.Send(new GetHighRiskPatientsQuery(), ct));
}

// Placeholder query used by GetMyRiskHistoryAsync — implement fully in Application layer
public sealed record GetPatientRiskHistoryQuery(Guid PatientId) : MediatR.IRequest<object>;
