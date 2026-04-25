using App.BLL.Contracts.Auth.Requests;
using App.Domain.Abstraction.Consts;
using FluentValidation;

namespace App.BLL.Contracts.Auth.Validators;

public class GetTokenRequestValidator : AbstractValidator<GetTokenRequest>
{
    public GetTokenRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().Matches(RegexPatterns.Password).WithMessage("Password must contain uppercase, lowercase, number, and special character (min 8 chars)."); ;
    }
}
