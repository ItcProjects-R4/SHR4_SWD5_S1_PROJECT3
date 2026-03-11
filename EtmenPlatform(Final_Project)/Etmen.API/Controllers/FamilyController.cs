using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Controllers;

/// <summary>Family account linking management (NEW v3.0).</summary>
[ApiController]
[Route("api/family")]
[Authorize(Roles = "Patient")]
public sealed class FamilyController : ControllerBase
{
    private readonly IMediator _mediator;
    public FamilyController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public IActionResult GetFamilyMembersAsync() => throw new NotImplementedException();

    [HttpPost("invite")]
    public IActionResult InviteMemberAsync() => throw new NotImplementedException();

    [HttpPost("accept")]
    public IActionResult AcceptInviteAsync() => throw new NotImplementedException();

    [HttpGet("switch/{linkedUserId}")]
    public IActionResult SwitchProfileAsync(Guid linkedUserId) => throw new NotImplementedException();

    [HttpDelete("{linkId}")]
    public IActionResult RemoveLinkAsync(Guid linkId) => throw new NotImplementedException();
}
