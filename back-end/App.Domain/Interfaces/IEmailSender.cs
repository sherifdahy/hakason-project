using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}
