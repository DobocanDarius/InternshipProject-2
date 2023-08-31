using InternshipProject_2.Helpers;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(List<string> toEmails, string subject, string body)
    {
        var smtpHost = _configuration["SmtpSettings:SmtpHost"];
        var smtpPort = int.Parse(_configuration["SmtpSettings:SmtpPort"]);
        var smtpUsername = _configuration["SmtpSettings:SmtpUsername"];
        var smtpPassword = _configuration["SmtpSettings:SmtpPassword"];
        using (var client = new SmtpClient(smtpHost, smtpPort))
        {
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            client.EnableSsl = true;

            var message = new MailMessage
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
