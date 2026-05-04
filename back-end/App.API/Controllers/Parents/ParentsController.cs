using App.API.Abstraction;
using App.Domain.Abstraction.Consts;
using App.Domain.Contracts.Invitations.Requests;
using App.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Parents;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = $"{DefaultRoles.Parent.Name},{DefaultRoles.Admin.Name}")]
public class ParentsController : ControllerBase
{
    private readonly IInvitationService _invitationService;

    public ParentsController(IInvitationService invitationService)
    {
        _invitationService = invitationService;
    }

    [HttpPost("{parentId}/invitations")]
    public async Task<IActionResult> Invite(int parentId,[FromBody] InviteChildRequest request,CancellationToken cancellationToken)
    {
        var result = await _invitationService.InviteChildAsync(parentId, request, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{parentId}/invitations")]
    public async Task<IActionResult> GetMyInvitations(int parentId,CancellationToken cancellationToken)
    {
        var result = await _invitationService.GetMyInvitationsAsync(parentId, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpDelete("invitations/{id:int}/cancel")]
    public async Task<IActionResult> Cancel(int id,CancellationToken cancellationToken)
    {
        var result = await _invitationService.CancelInvitationAsync(id, cancellationToken);

        return result.IsSuccess? NoContent() : result.ToProblem();
    }
}
