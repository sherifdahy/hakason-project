using App.Domain.Abstraction;
using Microsoft.AspNetCore.Http;

namespace App.BLL.Errors;

public static class InvitationErrors
{
    public static readonly Error NotFound = new("Invitation.NotFound", "Invitation not found.", StatusCodes.Status404NotFound);
    public static readonly Error InvalidCode = new("Invitation.InvalidCode", "Invalid invitation code.", StatusCodes.Status400BadRequest);
    public static readonly Error ExpiredCode = new("Invitation.Expired", "Invitation code has expired.", StatusCodes.Status400BadRequest);
    public static readonly Error AlreadyUsed = new("Invitation.AlreadyUsed", "Invitation already used.", StatusCodes.Status400BadRequest);
    public static readonly Error TooManyAttempts = new("Invitation.TooManyAttempts", "Too many failed attempts.", StatusCodes.Status429TooManyRequests);
    public static readonly Error CannotInviteSelf = new("Invitation.CannotInviteSelf", "You cannot invite yourself.", StatusCodes.Status400BadRequest);
    public static readonly Error UnauthorizedRole = new("Invitation.UnauthorizedRole", "Only parents can invite children.", StatusCodes.Status403Forbidden);
}
