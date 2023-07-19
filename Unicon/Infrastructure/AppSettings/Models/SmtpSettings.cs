namespace Infrastructure.AppSettings.Models;

public class SmtpSettings
{
    public string Host { get; init; }
    public int Port { get; init; }
    public string SenderMail { get; init; }
    public string SenderMailPassword { get; init; }
    public int SslEnabledPort { get; init; }
    public bool IsSslEnabled { get; init; }
    public string RecipientMail { get; init; }
}
