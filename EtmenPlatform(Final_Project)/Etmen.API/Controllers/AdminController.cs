using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>Admin-only dashboard statistics and high-risk patient overview.</summary>
[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public sealed class AdminController : ControllerBase
{
    private readonly IMediator _mediator;
    public AdminController(IMediator mediator) => _mediator = mediator;

    [HttpGet("dashboard")]
    public IActionResult GetDashboardStatsAsync() => throw new NotImplementedException();

    [HttpGet("high-risk")]
    [Authorize(Roles = "Admin,Doctor")]
    public IActionResult GetHighRiskPatientsAsync() => throw new NotImplementedException();

    [HttpGet("risk-distribution")]
    public IActionResult GetRiskDistributionAsync() => throw new NotImplementedException();
}
