using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>GPS-based healthcare provider search and appointment booking (NEW v3.0).</summary>
[ApiController]
[Route("api/nearby")]
[Authorize(Roles = "Patient")]
public sealed class NearbyController : ControllerBase
{
    private readonly IMediator _mediator;
    public NearbyController(IMediator mediator) => _mediator = mediator;

    [HttpGet("providers")]
    public IActionResult GetNearbyProvidersAsync([FromQuery] double lat, [FromQuery] double lng,
        [FromQuery] string? riskLevel, [FromQuery] bool openNow = false)
        => throw new NotImplementedException();

    [HttpGet("doctors")]
    public IActionResult GetNearbyDoctorsAsync([FromQuery] double lat, [FromQuery] double lng,
        [FromQuery] string? specialty)
        => throw new NotImplementedException();

    [HttpGet("hospitals")]
    public IActionResult GetNearbyHospitalsAsync([FromQuery] double lat, [FromQuery] double lng)
        => throw new NotImplementedException();

    [HttpGet("providers/{id}")]
    public IActionResult GetProviderDetailsAsync(Guid id) => throw new NotImplementedException();

    [HttpPost("book")]
    public IActionResult BookAppointmentAsync() => throw new NotImplementedException();
}
