using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>Risk assessment retrieval, manual trigger, and recommendation endpoints.</summary>
[ApiController]
[Route("api/risk-assessments")]
[Authorize]
public sealed class RiskAssessmentController : ControllerBase
{
    private readonly IMediator _mediator;
    public RiskAssessmentController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{id}")]
    public IActionResult GetAssessmentAsync(Guid id) => throw new NotImplementedException();

    [HttpGet("patient/{id}")]
    public IActionResult GetPatientAssessmentsAsync(Guid id) => throw new NotImplementedException();

    [HttpPost("trigger")]
    [Authorize(Roles = "Doctor,Admin")]
    public IActionResult TriggerManualAsync() => throw new NotImplementedException();

    [HttpGet("{id}/required-analyses")]
    public IActionResult GetRequiredAnalysesAsync(Guid id) => throw new NotImplementedException();

    [HttpGet("{id}/doctors")]
    public IActionResult GetRecommendedDoctorsAsync(Guid id) => throw new NotImplementedException();

    [HttpGet("history")]
    public IActionResult GetRiskHistoryAsync([FromQuery] Guid patientId) => throw new NotImplementedException();
}
