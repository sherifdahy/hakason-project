using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts.Users.Responses;

public record UserResponse(
    int Id,
    string Email,
    string FullName,
    string Role,
    bool EmailConfirmed
);
