using System;
using System.IO;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
namespace Bridge.Service
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendWelcomeEmailAsync(string userEmail, string userName)
        {
            string? emailSender = Environment.GetEnvironmentVariable("SMTP_EMAIL")
                                 ?? _configuration["EmailSettings:SmtpEmail"];
            string? emailPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD")
                                   ?? _configuration["EmailSettings:SmtpPassword"];
            string clientUrl = Environment.GetEnvironmentVariable("CLIENT_URL")
                               ?? _configuration["ClientUrl"]
                               ?? "http://localhost:4200/";

            if (string.IsNullOrWhiteSpace(emailSender) || string.IsNullOrWhiteSpace(emailPassword))
            {
                throw new InvalidOperationException(
                    "Email sender/password are not configured. Set EmailSettings:SmtpEmail and EmailSettings:SmtpPassword " +
                    "in appsettings.Development.json (locally) or as SMTP_EMAIL/SMTP_PASSWORD environment variables (in production).");
            }

            string templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", "WelcomeTemplate.html");
            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Email template not found at path: {templatePath}");
            }
            string htmlBody = File.ReadAllText(templatePath);
            htmlBody = htmlBody.Replace("{{UserName}}", userName)
                               .Replace("{{ClientUrl}}", clientUrl);
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("מערכת Bridge", emailSender));
            message.To.Add(new MailboxAddress(userName, userEmail));
            message.Subject = "ברוך הבא למערכת!";
            message.Body = new TextPart("html")
            {
                Text = htmlBody
            };
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);

                await client.AuthenticateAsync(emailSender, emailPassword);

                await client.SendAsync(message);

                Console.WriteLine($"[EmailService] המייל נשלח בהצלחה לכתובת: {userEmail}");
                await client.DisconnectAsync(true);
            }
        }
    }
}