namespace InternshipProject_2.Helpers;

public interface IEmailService
{
    Task SendEmailAsync(List<string> toEmails, string subject, string body);
}