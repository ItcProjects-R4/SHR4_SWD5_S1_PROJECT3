using Etmen.Application.UseCases.History;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>
/// Patient risk and vitals timeline endpoints (v3.0). Route: /api/health-history
/// Reads from existing RiskAssessments + MedicalRecords — no new DB tables required.
/// </summary>
[ApiController]
[Route("api/health-history")]
[Authorize]
public class HealthHistoryController : ControllerBase
{
    private readonly IMediator _mediator;
    public HealthHistoryController(IMediator mediator) => _mediator = mediator;

    /// <summary>GET /api/health-history/risk?patientId=&from=&to= — Risk score timeline.</summary>
    [HttpGet("risk")]
    public async Task<IActionResult> GetRiskHistoryAsync(
        [FromQuery] Guid patientId, [FromQuery] DateTime from, [FromQuery] DateTime to,
        CancellationToken ct)
        => Ok(await _mediator.Send(new GetRiskHistoryQuery(patientId, from, to), ct));

    /// <summary>GET /api/health-history/vitals?patientId= — BP, Sugar, BMI over time.</summary>
    [HttpGet("vitals")]
    public async Task<IActionResult> GetVitalsTimelineAsync([FromQuery] Guid patientId, CancellationToken ct)
        => Ok(await _mediator.Send(new GetVitalsTimelineQuery(patientId), ct));
}
