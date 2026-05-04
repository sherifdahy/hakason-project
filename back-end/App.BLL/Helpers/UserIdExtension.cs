using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace App.BLL.Helpers;

public static class UserIdExtension
{
    public static int GetUserId(this ClaimsPrincipal claims)
    {
        var userIdClaim = claims.Claims.First(x=> x.Type == ClaimTypes.NameIdentifier);

        return int.Parse(userIdClaim.Value);
    }
}
