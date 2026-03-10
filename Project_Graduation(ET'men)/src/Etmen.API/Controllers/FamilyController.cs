using Etmen.Application.UseCases.Family;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>
/// Family account linking management (v3.0). Route: /api/family
/// Supports invite flow, profile switching, and link removal.
/// </summary>
[ApiController]
[Route("api/family")]
[Authorize]
public class FamilyController : ControllerBase
{
    private readonly IMediator _mediator;
    public FamilyController(IMediator mediator) => _mediator = mediator;

    /// <summary>GET /api/family — Returns all linked profiles for the authenticated user.</summary>
    [HttpGet]
    public async Task<IActionResult> GetFamilyMembersAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    /// <summary>POST /api/family/invite — Generates invite link and emails it to the family member.</summary>
    [HttpPost("invite")]
    public async Task<IActionResult> InviteMemberAsync([FromBody] InviteMemberCommand command, CancellationToken ct)
        => Ok(await _mediator.Send(command, ct));

    /// <summary>POST /api/family/accept — Family member accepts invite → FamilyLink created.</summary>
    [HttpPost("accept")]
    public async Task<IActionResult> AcceptInviteAsync([FromQuery] string token, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    /// <summary>GET /api/family/switch/{linkedUserId} — Returns a scoped JWT for the linked profile.</summary>
    [HttpGet("switch/{linkedUserId:guid}")]
    public async Task<IActionResult> SwitchProfileAsync(Guid linkedUserId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    /// <summary>DELETE /api/family/{linkId} — Removes the family link.</summary>
    [HttpDelete("{linkId:guid}")]
    public async Task<IActionResult> RemoveLinkAsync(Guid linkId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
