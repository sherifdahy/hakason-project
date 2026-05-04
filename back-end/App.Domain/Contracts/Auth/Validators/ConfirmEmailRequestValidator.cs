using App.Domain.Contracts.Auth.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts.Auth.Validators;

public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
{
    public ConfirmEmailRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress();

        RuleFor(x => x.Code)
            .Length(6);
    }
}
