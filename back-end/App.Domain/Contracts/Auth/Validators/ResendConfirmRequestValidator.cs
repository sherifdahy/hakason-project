using App.Domain.Contracts.Auth.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts.Auth.Validators;

public class ResendConfirmRequestValidator : AbstractValidator<ResendConfirmRequest>
{
    public ResendConfirmRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress();
    }
}
