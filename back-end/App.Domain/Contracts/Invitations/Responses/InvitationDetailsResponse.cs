using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts.Invitations.Responses;


public record InvitationDetailsResponse(
    int Id,
    string ChildEmail,
    string ChildName,
    DateTime ExpiresAt,
    DateTime CreatedAt,
    bool IsAccepted,
    bool IsExpired
);
