using App.Domain.Entities.Identity;

namespace App.BLL.Authentication;

public interface IJwtProvider
{
    (string token, int expiresIn) GenerateToken(ApplicationUser user, IEnumerable<string> roles);
    public string? ValidateToken(string token);
}
