using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts.Invitations.Responses;

public record InviteChildResponse(
int InvitationId,
string ChildEmail,
string ChildName,
DateTime ExpiresAt,
string Message
);
