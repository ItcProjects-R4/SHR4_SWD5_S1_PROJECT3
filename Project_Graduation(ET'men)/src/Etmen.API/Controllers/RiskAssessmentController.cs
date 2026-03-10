using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>
/// Risk assessment retrieval and manual triggers. Route: /api/risk-assessments
/// </summary>
[ApiController]
[Route("api/risk-assessments")]
[Authorize]
public class RiskAssessmentController : ControllerBase
{
    private readonly IMediator _mediator;
    public RiskAssessmentController(IMediator mediator) => _mediator = mediator;

    /// <summary>GET /api/risk-assessments/{id} — Single assessment result.</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAssessmentAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    /// <summary>GET /api/risk-assessments/patient/{id} — Patient history (paginated).</summary>
    [HttpGet("patient/{id:guid}")]
    public async Task<IActionResult> GetPatientAssessmentsAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    /// <summary>POST /api/risk-assessments/trigger — Doctor/Admin triggers re-assessment.</summary>
    [HttpPost("trigger")]
    [Authorize(Roles = "Doctor,Admin")]
    public async Task<IActionResult> TriggerManualAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    /// <summary>GET /api/risk-assessments/{id}/required-analyses — Recommended lab tests.</summary>
    [HttpGet("{id:guid}/required-analyses")]
    public async Task<IActionResult> GetRequiredAnalysesAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    /// <summary>GET /api/risk-assessments/{id}/doctors — Doctor suggestions for risk level.</summary>
    [HttpGet("{id:guid}/doctors")]
    public async Task<IActionResult> GetRecommendedDoctorsAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    /// <summary>GET /api/risk-assessments/history — Patient risk score timeline (v3.0).</summary>
    [HttpGet("history")]
    public async Task<IActionResult> GetRiskHistoryAsync(
        [FromQuery] Guid patientId, [FromQuery] DateTime from, [FromQuery] DateTime to,
        CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
