using Application.Interfaces.Services;
using Infrastructure.AppSettings.Models;
using Infrastructure.Constants;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly SmtpSettings _smtpSettings;

    public EmailService(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress(AppSettingConstants.ProjectName, _smtpSettings.SenderMail));
        emailMessage.To.Add(new MailboxAddress(string.Empty, email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message
        };

        using var client = new MailKit.Net.Smtp.SmtpClient();
        await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, true);
        await client.AuthenticateAsync(_smtpSettings.SenderMail, _smtpSettings.SenderMailPassword);
        await client.SendAsync(emailMessage);

        await client.DisconnectAsync(true);
    }
}
