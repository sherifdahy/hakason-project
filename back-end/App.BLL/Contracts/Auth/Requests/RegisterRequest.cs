using System;
using System.Collections.Generic;
using System.Text;

namespace App.BLL.Contracts.Auth.Requests;

public record RegisterRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
