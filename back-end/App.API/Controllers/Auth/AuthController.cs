using App.API.Abstraction;
using App.BLL.Authentication;
using App.Domain.Contracts.Auth.Requests;
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

    [HttpPost("register-parent")]
    public async Task<IActionResult> RegisterParent(RegisterParentRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _authService.RegisterParentAsync(request, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPost("register-child")]
    public async Task<IActionResult> RegisterChild([FromBody] RegisterChildRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterChildAsync(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("resend-confirmation")]
    public async Task<IActionResult> ResentConfirmation([FromBody] ResendConfirmRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _authService.ResendConfirmationAsync(request.Email, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.ConfirmEmailAsync(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}
