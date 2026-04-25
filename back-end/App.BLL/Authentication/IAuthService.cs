using App.BLL.Contracts.Auth.Requests;
using App.BLL.Contracts.Auth.Responses;
using App.Domain.Abstraction;

namespace App.BLL.Authentication;

public interface IAuthService
{
    Task<Result<AuthResponse>> GetTokenAsync(GetTokenRequest request, CancellationToken cancellationToken);
    Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
}
