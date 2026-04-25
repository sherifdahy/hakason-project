using App.BLL.Contracts.Auth.Requests;
using App.Domain.Abstraction.Consts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BLL.Contracts.Auth.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.FullName).NotEmpty();

        RuleFor(x => x.Email).EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches(RegexPatterns.Password)
            .WithMessage("Password must contain uppercase, lowercase, number, and special character (min 8 chars).");
    }
}
