using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>Patient profile, risk history, and alert management.</summary>
[ApiController]
[Route("api/patients")]
[Authorize]
public sealed class PatientController : ControllerBase
{
    private readonly IMediator _mediator;
    public PatientController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{id}")]
    public IActionResult GetProfileAsync(Guid id) => throw new NotImplementedException();

    [HttpPut("{id}")]
    public IActionResult UpdateProfileAsync(Guid id) => throw new NotImplementedException();

    [HttpGet("{id}/risk-history")]
    public IActionResult GetRiskHistoryAsync(Guid id) => throw new NotImplementedException();

    [HttpGet("{id}/alerts")]
    public IActionResult GetMyAlertsAsync(Guid id) => throw new NotImplementedException();

    [HttpGet]
    [Authorize(Roles = "Doctor,Admin")]
    public IActionResult GetAllPatientsAsync() => throw new NotImplementedException();

    [HttpGet("high-risk")]
    [Authorize(Roles = "Doctor,Admin")]
    public IActionResult GetHighRiskPatientsAsync() => throw new NotImplementedException();
}
