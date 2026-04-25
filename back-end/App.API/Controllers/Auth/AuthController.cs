using App.API.Abstraction;
using App.BLL.Authentication;
using App.BLL.Contracts.Auth.Requests;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Auth;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("get-token")]
    public async Task<IActionResult> GetToken(GetTokenRequest request,CancellationToken cancellationToken = default)
    {
        var result = await _authService.GetTokenAsync(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _authService.RegisterAsync(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}
