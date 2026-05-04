using App.Domain.Abstraction;
using App.Domain.Contracts.Users.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Interfaces;

public interface IUserService
{
    Task<Result<UserResponse>> GetCurrentUserAsync(CancellationToken cancellationToken = default);
}
