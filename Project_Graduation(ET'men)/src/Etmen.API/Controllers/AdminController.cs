using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>
/// Admin-only dashboard statistics and oversight endpoints. Route: /api/admin
/// </summary>
[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;
    public AdminController(IMediator mediator) => _mediator = mediator;

    /// <summary>GET /api/admin/dashboard — Total patients, risk distribution, alert counts.</summary>
    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboardStatsAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    /// <summary>GET /api/admin/high-risk — Patients with risk ≥ 0.6 sorted by urgency. Also Doctor role.</summary>
    [HttpGet("high-risk")]
    [Authorize(Roles = "Doctor,Admin")]
    public async Task<IActionResult> GetHighRiskPatientsAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    /// <summary>GET /api/admin/risk-distribution — Low / Medium / High counts + percentages.</summary>
    [HttpGet("risk-distribution")]
    public async Task<IActionResult> GetRiskDistributionAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
