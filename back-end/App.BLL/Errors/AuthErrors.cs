using App.Domain.Abstraction;
using Microsoft.AspNetCore.Http;

namespace App.BLL.Errors;

public static class AuthErrors
{
    public static Error DuplicatedEmail => new("Auth.DuplicatedEmail", "Email is already exist.", StatusCodes.Status401Unauthorized);
    public static Error InvalidCredentials => new ("Auth.InvalidCredentials", "Email or Password is incorrect.", StatusCodes.Status401Unauthorized);
    public static Error InvalidJwtToken => new("User.InvalidJwtToken", "Invalid Jwt Token.", StatusCodes.Status401Unauthorized);
    public static Error InvalidCode => new("User.InvalidCode", "Invalid Code", StatusCodes.Status401Unauthorized);
    public static Error LockedUser => new("Auth.LockedUser", "Locked User please contact administrator.", StatusCodes.Status401Unauthorized);

    public static readonly Error EmailAlreadyConfirmed = new("Auth.EmailAlreadyConfirmed", "Email is already confirmed.", StatusCodes.Status400BadRequest);

    public static readonly Error EmailNotConfirmed = new("Auth.EmailNotConfirmed", "Email is not confirmed.", StatusCodes.Status401Unauthorized);
    
    public static readonly Error NotFound = new("Auth.NotFound", "User is not Found.", StatusCodes.Status401Unauthorized);
}
