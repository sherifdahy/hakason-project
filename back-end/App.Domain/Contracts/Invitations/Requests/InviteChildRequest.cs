using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts.Invitations.Requests;

public record InviteChildRequest(
    string ChildEmail,
    string ChildName
);
