using App.Domain.Abstraction.Consts;
using App.Domain.Contracts.Auth.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts.Auth.Validators;

public class RegisterParentRequestValidator : AbstractValidator<RegisterParentRequest>
{
    public RegisterParentRequestValidator()
    {
        RuleFor(x => x.FullName).NotEmpty();

        RuleFor(x => x.Email).EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches(RegexPatterns.Password)
            .WithMessage("Password must contain uppercase, lowercase, number, and special character (min 8 chars).");
    }
}
