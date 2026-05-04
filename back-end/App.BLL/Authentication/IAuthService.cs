using App.Domain.Abstraction;
using App.Domain.Contracts.Auth.Requests;
using App.Domain.Contracts.Auth.Responses;

namespace App.BLL.Authentication;

public interface IAuthService
{
    Task<Result<AuthResponse>> GetTokenAsync(GetTokenRequest request, CancellationToken cancellationToken);
    Task<Result> RegisterParentAsync(RegisterParentRequest request, CancellationToken cancellationToken);
    Task<Result<AuthResponse>> RegisterChildAsync(RegisterChildRequest request,CancellationToken cancellationToken);
    Task<Result> ResendConfirmationAsync(string email, CancellationToken cancellationToken = default);
    Task<Result<AuthResponse>> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken cancellationToken);
}
