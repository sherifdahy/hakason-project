using App.BLL.Authentication;
using App.BLL.Builders;
using App.BLL.Errors;
using App.BLL.Helpers;
using App.DAL.Data;
using App.Domain.Abstraction;
using App.Domain.Abstraction.Consts;
using App.Domain.Contracts.Auth.Requests;
using App.Domain.Contracts.Auth.Responses;
using App.Domain.Entities.Identity;
using App.Domain.Entities.Persons;
using App.Domain.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.BLL.Services;

public class AuthService : IAuthService
{
    private readonly IJwtProvider _jwtProvider;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IEmailSender _emailSender;
    private readonly ApplicationDbContext _context;

    private const int MaxAttempts = 5;

    public AuthService(
        ApplicationDbContext context,
        IEmailSender emailSender,
        IJwtProvider jwtProvider,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _context = context;
        _emailSender = emailSender;
        _jwtProvider = jwtProvider;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Result<AuthResponse>> GetTokenAsync(GetTokenRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return Result.Failure<AuthResponse>(AuthErrors.InvalidCredentials);

        if (!user.EmailConfirmed)
            return Result.Failure<AuthResponse>(AuthErrors.EmailNotConfirmed);

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, true);

        if (result.Succeeded)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtProvider.GenerateToken(user, roles);
            return Result.Success(new AuthResponse(user.Id, user.FullName, roles.First(), token.token, token.expiresIn));
        }

        return result.IsLockedOut
            ? Result.Failure<AuthResponse>(AuthErrors.LockedUser)
            : Result.Failure<AuthResponse>(AuthErrors.InvalidCredentials);
    }

    public async Task<Result> RegisterParentAsync(RegisterParentRequest request, CancellationToken cancellationToken)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not null)
            return Result.Failure(AuthErrors.DuplicatedEmail);

        var user = request.Adapt<ApplicationUser>();
        user.UserName = request.Email;

        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var error = result.Errors.First();
                return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
            }

            await _userManager.AddToRoleAsync(user, DefaultRoles.Parent.Name);

            var parent = new Parent()
            {
                UserId = user.Id,
            };

            await _context.Parents.AddAsync(parent);

            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            await SendConfirmationOtpAsync(user);

            return Result.Success();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<Result<AuthResponse>> RegisterChildAsync(
        RegisterChildRequest request,
        CancellationToken cancellationToken)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not null)
            return Result.Failure<AuthResponse>(AuthErrors.DuplicatedEmail);

        var invitation = await _context.Invitations
            .Include(i => i.Parent).ThenInclude(x=>x.User)
            .Where(i => i.ChildEmail == request.Email && !i.IsAccepted)
            .OrderByDescending(i => i.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);

        if (invitation is null)
            return Result.Failure<AuthResponse>(InvitationErrors.InvalidCode);

        if (invitation.IsExpired)
            return Result.Failure<AuthResponse>(InvitationErrors.ExpiredCode);

        if (invitation.Attempts >= MaxAttempts)
            return Result.Failure<AuthResponse>(InvitationErrors.TooManyAttempts);

        if (!OtpHelper.Verify(request.InvitationCode, invitation.HashedCode))
        {
            invitation.Attempts++;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Failure<AuthResponse>(InvitationErrors.InvalidCode);
        }

        // ✅ استخدمي Transaction عشان تتأكدي إن كل حاجة بتتعمل سوا
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var appUser = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FullName = request.FullName,
                EmailConfirmed = true
            };

            var createResult = await _userManager.CreateAsync(appUser, request.Password);

            if (!createResult.Succeeded)
            {
                var error = createResult.Errors.First();
                return Result.Failure<AuthResponse>(
                    new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
            }

            await _userManager.AddToRoleAsync(appUser, DefaultRoles.Child.Name);

            // 👨‍👦 اعملي ربط الـ Child بالـ Parent
            var child = new Child
            {
                ParentId = invitation.ParentId,
                UserId = appUser.Id
            };
            await _context.Children.AddAsync(child, cancellationToken);

            // ✅ علّمي الـ invitation كـ accepted
            invitation.IsAccepted = true;
            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            // 📧 ابعتي إشعار للأب
            await SendChildJoinedEmailAsync(
                invitation.Parent.User.Email!,
                invitation.Parent.User.FullName,
                appUser.FullName);

            // 🎟️ JWT
            var roles = new List<string> { DefaultRoles.Child.Name };
            var token = _jwtProvider.GenerateToken(appUser, roles);

            return Result.Success(new AuthResponse(
                appUser.Id,
                appUser.FullName,
                roles.First(),
                token.token,
                token.expiresIn
            ));
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<Result<AuthResponse>> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return Result.Failure<AuthResponse>(AuthErrors.InvalidCode);

        if (user.EmailConfirmed)
            return Result.Failure<AuthResponse>(AuthErrors.EmailAlreadyConfirmed);

        var isValid = await _userManager.VerifyUserTokenAsync(
            user,
            TokenOptions.DefaultEmailProvider,
            UserManager<ApplicationUser>.ConfirmEmailTokenPurpose,
            request.Code
        );

        if (!isValid)
            return Result.Failure<AuthResponse>(AuthErrors.InvalidCode);

        user.EmailConfirmed = true;
        var updateResult = await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
        {
            var error = updateResult.Errors.First();
            return Result.Failure<AuthResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        var roles = await _userManager.GetRolesAsync(user);
        var jwt = _jwtProvider.GenerateToken(user, roles);

        return Result.Success(new AuthResponse(
            user.Id,
            user.FullName,
            roles.First(),
            jwt.token,
            jwt.expiresIn
        ));
    }

    public async Task<Result> ResendConfirmationAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            return Result.Success();

        if (user.EmailConfirmed)
            return Result.Failure(AuthErrors.EmailAlreadyConfirmed);

        await SendConfirmationOtpAsync(user);

        return Result.Success();
    }

    private async Task SendConfirmationOtpAsync(ApplicationUser user)
    {
        var otpCode = await _userManager.GenerateUserTokenAsync(
            user,
            TokenOptions.DefaultEmailProvider,
            UserManager<ApplicationUser>.ConfirmEmailTokenPurpose
        );

        var emailBody = await EmailBodyBuilder.GenerateEmailBody("EmailConfirmation", new Dictionary<string, string>
        {
            { "{{name}}", user.FullName },
            { "{{otp_code}}", otpCode },
            { "{{expiration_minutes}}", "3" } 
        });

        await _emailSender.SendEmailAsync(user.Email!, "🔐 Confirm Your KidBridge AI Account", emailBody);
    }

    private async Task SendChildJoinedEmailAsync(string parentEmail, string parentName, string childName)
    {
        var body = await EmailBodyBuilder.GenerateEmailBody("ChildJoined", new Dictionary<string, string>
        {
            { "{{parent_name}}", parentName },
            { "{{child_name}}", childName }
        });

        await _emailSender.SendEmailAsync(parentEmail, $"{childName} joined your KidBridge AI family!", body);
    }
}