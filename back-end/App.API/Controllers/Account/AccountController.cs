using App.API.Abstraction;
using App.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.API.Controllers.Account;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AccountController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        var result = await _userService.GetCurrentUserAsync(cancellationToken);
        return result.IsSuccess ? Ok(result.Value): result.ToProblem();
    }
}
