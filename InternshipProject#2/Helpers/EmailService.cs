using System.Net;
using System.Net.Mail;

using InternshipProject_2.Helpers;

public class EmailService : IEmailService
{
    private readonly IConfiguration _Configuration;

    public EmailService(IConfiguration configuration)
    {
        _Configuration = configuration;
    }

    public async Task SendEmailAsync(List<string> toEmails, string subject, string body)
    {
        string? smtpHost = _Configuration["SmtpSettings:SmtpHost"];
        int smtpPort;

        if (int.TryParse(_Configuration["SmtpSettings:SmtpPort"], out int smptSettingsPort))
        {
            smtpPort = smptSettingsPort;
            string? smtpUsername = _Configuration["SmtpSettings:SmtpUsername"];
            string? smtpPassword = _Configuration["SmtpSettings:SmtpPassword"];
            using (SmtpClient client = new SmtpClient(smtpHost, smtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.EnableSsl = true;

                MailMessage message = new MailMessage
                {
                    From = new MailAddress(smtpUsername),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                foreach (var toEmail in toEmails)
                {
                    message.To.Add(toEmail);
                }

                await client.SendMailAsync(message);
            }
        }
    }

}
