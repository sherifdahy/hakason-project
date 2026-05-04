using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts.Auth.Requests;

public record RegisterChildRequest(
string Email,
string Password,
string FullName,
string InvitationCode
);
