using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>Patient health history timeline endpoints (NEW v3.0).</summary>
[ApiController]
[Route("api/health-history")]
[Authorize(Roles = "Patient,Doctor")]
public sealed class HealthHistoryController : ControllerBase
{
    private readonly IMediator _mediator;
    public HealthHistoryController(IMediator mediator) => _mediator = mediator;

    [HttpGet("risk")]
    public IActionResult GetRiskHistoryAsync([FromQuery] Guid patientId,
        [FromQuery] DateTime? from, [FromQuery] DateTime? to)
        => throw new NotImplementedException();

    [HttpGet("vitals")]
    public IActionResult GetVitalsTimelineAsync([FromQuery] Guid patientId)
        => throw new NotImplementedException();
}
