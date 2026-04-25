using App.BLL.Authentication;
using App.BLL.Contracts.Auth.Requests;
using App.BLL.Contracts.Auth.Responses;
using App.BLL.Errors;
using App.Domain.Abstraction;
using App.Domain.Abstraction.Consts;
using App.Domain.Entities.Identity;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace App.BLL.Services;
public class AuthService : IAuthService
{
    private readonly IJwtProvider _jwtProvider;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthService(
        IJwtProvider jwtProvider,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _jwtProvider = jwtProvider;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Result<AuthResponse>> GetTokenAsync(GetTokenRequest request, CancellationToken cancellationToken)
    {
        var applicationUser = await _userManager.FindByEmailAsync(request.Email);

        if (applicationUser == null)
            return Result.Failure<AuthResponse>(AuthErrors.InvalidCredentials);

        var passwordCheckResult = await _signInManager.PasswordSignInAsync(applicationUser, request.Password, false, false);

        if (passwordCheckResult.Succeeded)
        {
            var roles = await _userManager.GetRolesAsync(applicationUser);
            var token = _jwtProvider.GenerateToken(applicationUser, roles);
            return Result.Success(new AuthResponse()
            {
                Token = token.token,
                ExpireIn = token.expiresIn
            });
        }

        return passwordCheckResult.IsLockedOut
            ? Result.Failure<AuthResponse>(AuthErrors.LockedUser)
            : Result.Failure<AuthResponse>(AuthErrors.InvalidCredentials);
    }

    public async Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not null)
            return Result.Failure<AuthResponse>(AuthErrors.DuplicatedEmail);

        var user = request.Adapt<ApplicationUser>();
        user.UserName = request.Email;
        var result = await _userManager.CreateAsync(user, request.Password);

        if(result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, DefaultRoles.Client.name);
            
            var token = _jwtProvider.GenerateToken(user, new List<string> { DefaultRoles.Client.name });
            return Result.Success(new AuthResponse()
            {
                Token = token.token,
                ExpireIn = token.expiresIn
            });
        }
        var error = result.Errors.First();

        return Result.Failure<AuthResponse>(new Error(error.Code,error.Description,StatusCodes.Status400BadRequest));

    }
}
