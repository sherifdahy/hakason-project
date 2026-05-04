using App.Domain.Abstraction;
using App.Domain.Contracts.Invitations.Requests;
using App.Domain.Contracts.Invitations.Responses;

namespace App.Domain.Interfaces;

public interface IInvitationService
{
    Task<Result<InviteChildResponse>> InviteChildAsync(int parentId,InviteChildRequest request,CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<InvitationDetailsResponse>>> GetMyInvitationsAsync(int parentId,CancellationToken cancellationToken = default);
    Task<Result> CancelInvitationAsync(int invitationId,CancellationToken cancellationToken = default);
}