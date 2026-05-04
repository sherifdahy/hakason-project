using App.Domain.ClassOptions;
using App.Domain.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace App.BLL.Services;

public class EmailService(IOptions<MailSettings> options) : IEmailSender
{
    private readonly MailSettings _mailSettings = options.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage
        {
            Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail),
            Subject = subject
        };

        message.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
        message.To.Add(MailboxAddress.Parse(email));

        message.Body = new BodyBuilder { HtmlBody = htmlMessage }.ToMessageBody();

        using var smtp = new SmtpClient
        {
            CheckCertificateRevocation = false
        };

        try
        {
            await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(message);
        }
        finally
        {
            await smtp.DisconnectAsync(true);
        }
    }
}
