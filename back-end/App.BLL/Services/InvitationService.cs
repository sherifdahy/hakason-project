using App.BLL.Builders;
using App.BLL.Errors;
using App.BLL.Helpers;
using App.DAL.Data;
using App.Domain.Abstraction;
using App.Domain.Abstraction.Consts;
using App.Domain.Contracts.Invitations.Requests;
using App.Domain.Contracts.Invitations.Responses;
using App.Domain.Entities.Business;
using App.Domain.Entities.Identity;
using App.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.BLL.Services;

public class InvitationService : IInvitationService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;

    private const int CodeLength = 6;
    private const int InvitationLifetimeHours = 24;

    public InvitationService(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        IEmailSender emailSender)
    {
        _context = context;
        _userManager = userManager;
        _emailSender = emailSender;
    }

    public async Task<Result<InviteChildResponse>> InviteChildAsync(
        int parentId,
        InviteChildRequest request,
        CancellationToken cancellationToken = default)
    {
        var parent = await _userManager.FindByIdAsync(parentId.ToString());
        if (parent is null)
            return Result.Failure<InviteChildResponse>(AuthErrors.NotFound);

        var roles = await _userManager.GetRolesAsync(parent);
        if (!roles.Contains(DefaultRoles.Parent.Name))
            return Result.Failure<InviteChildResponse>(InvitationErrors.UnauthorizedRole);

        if (string.Equals(parent.Email, request.ChildEmail, StringComparison.OrdinalIgnoreCase))
            return Result.Failure<InviteChildResponse>(InvitationErrors.CannotInviteSelf);

        if (await _userManager.FindByEmailAsync(request.ChildEmail) is not null)
            return Result.Failure<InviteChildResponse>(AuthErrors.DuplicatedEmail);

        var existingInvitations = await _context.Invitations
            .Where(i => i.ChildEmail == request.ChildEmail
                     && !i.IsAccepted
                     && i.ExpiresAt > DateTime.UtcNow)
            .ToListAsync(cancellationToken);

        foreach (var old in existingInvitations)
        {
            old.ExpiresAt = DateTime.UtcNow;
        }

        var code = OtpHelper.Generate(CodeLength);

        var invitation = new Invitation
        {
            ParentId = parentId,
            ChildEmail = request.ChildEmail,
            ChildName = request.ChildName,
            HashedCode = OtpHelper.Hash(code),
            ExpiresAt = DateTime.UtcNow.AddHours(InvitationLifetimeHours),
            IsAccepted = false,
            Attempts = 0
        };

        await _context.Invitations.AddAsync(invitation, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        await SendInvitationEmailAsync(
            request.ChildEmail,
            request.ChildName,
            parent.FullName,
            code);

        var response = new InviteChildResponse(
            invitation.Id,
            invitation.ChildEmail,
            invitation.ChildName,
            invitation.ExpiresAt,
            "Invitation sent successfully to your child's email."
        );

        return Result.Success(response);
    }

    public async Task<Result<IEnumerable<InvitationDetailsResponse>>> GetMyInvitationsAsync(
        int parentId,
        CancellationToken cancellationToken = default)
    {
        var invitations = await _context.Invitations
            .Where(i => i.ParentId == parentId)
            .OrderByDescending(i => i.CreatedAt)
            .Select(i => new InvitationDetailsResponse(
                i.Id,
                i.ChildEmail,
                i.ChildName,
                i.ExpiresAt,
                i.CreatedAt,
                i.IsAccepted,
                DateTime.UtcNow > i.ExpiresAt
            ))
            .ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<InvitationDetailsResponse>>(invitations);
    }

    public async Task<Result> CancelInvitationAsync(
        int invitationId,
        CancellationToken cancellationToken = default)
    {
        var invitation = await _context.Invitations
            .FirstOrDefaultAsync(i => i.Id == invitationId, cancellationToken);

        if (invitation is null)
            return Result.Failure(InvitationErrors.NotFound);

        if (invitation.IsAccepted)
            return Result.Failure(InvitationErrors.AlreadyUsed);

        invitation.ExpiresAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private async Task SendInvitationEmailAsync(
        string childEmail,
        string childName,
        string parentName,
        string code)
    {
        var body = await EmailBodyBuilder.GenerateEmailBody(
            "ChildInvitation",
            new Dictionary<string, string>
            {
                { "{{child_name}}", childName },
                { "{{parent_name}}", parentName },
                { "{{invitation_code}}", code },
                { "{{expiration_hours}}", InvitationLifetimeHours.ToString() }
            });

        await _emailSender.SendEmailAsync(
            childEmail,
            $"🎉 {parentName} invited you to KidBridge AI!",
            body);
    }
}