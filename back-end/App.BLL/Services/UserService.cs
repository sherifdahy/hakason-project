using App.BLL.Errors;
using App.BLL.Helpers;
using App.DAL.Data;
using App.Domain.Abstraction;
using App.Domain.Contracts.Users.Responses;
using App.Domain.Entities.Identity;
using App.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace App.BLL.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    public UserService(
        IHttpContextAccessor httpContextAccessor,
        UserManager<ApplicationUser> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public async Task<Result<UserResponse>> GetCurrentUserAsync(
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext!.User.GetUserId().ToString());
        if (user is null)
            return Result.Failure<UserResponse>(AuthErrors.NotFound);

        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? string.Empty;


        var response = new UserResponse(
            user.Id,
            user.Email!,
            user.FullName,
            role,
            user.EmailConfirmed
        );

        return Result.Success(response);
    }
}