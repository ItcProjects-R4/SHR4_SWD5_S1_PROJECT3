using Etmen.Application.UseCases.Nearby;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>
/// GPS-based doctor and hospital search + appointment booking (v3.0). Route: /api/nearby
/// </summary>
[ApiController]
[Route("api/nearby")]
[Authorize]
public class NearbyController : ControllerBase
{
    private readonly IMediator _mediator;
    public NearbyController(IMediator mediator) => _mediator = mediator;

    /// <summary>GET /api/nearby/providers — Merged ranked list of doctors + hospitals.</summary>
    [HttpGet("providers")]
    public async Task<IActionResult> GetNearbyProvidersAsync(
        [FromQuery] double lat, [FromQuery] double lng,
        [FromQuery] double radius = 5000, [FromQuery] string? specialty = null,
        [FromQuery] string? riskLevel = null, [FromQuery] bool openNow = false,
        CancellationToken ct = default)
        => Ok(await _mediator.Send(new GetNearbyProvidersQuery(lat, lng, radius, specialty, riskLevel, openNow), ct));

    /// <summary>GET /api/nearby/doctors — Doctors only, filtered by specialty.</summary>
    [HttpGet("doctors")]
    public async Task<IActionResult> GetNearbyDoctorsAsync(
        [FromQuery] double lat, [FromQuery] double lng,
        [FromQuery] string? specialty = null, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>GET /api/nearby/hospitals — Hospitals by risk category.</summary>
    [HttpGet("hospitals")]
    public async Task<IActionResult> GetNearbyHospitalsAsync(
        [FromQuery] double lat, [FromQuery] double lng, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>GET /api/nearby/providers/{id} — Full detail: slots, reviews.</summary>
    [HttpGet("providers/{id}")]
    public async Task<IActionResult> GetProviderDetailsAsync(string id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    /// <summary>POST /api/nearby/book — Create Appointment + send confirmation.</summary>
    [HttpPost("book")]
    public async Task<IActionResult> BookAppointmentAsync([FromBody] BookAppointmentCommand command, CancellationToken ct)
        => Ok(await _mediator.Send(command, ct));
}
