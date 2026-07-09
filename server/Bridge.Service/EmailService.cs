using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace Bridge.Service
{
    public class EmailService
    {
        public void SendWelcomeEmail(string userEmail, string userName)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("צוות הפרויקט", "your-email@gmail.com"));
            message.To.Add(new MailboxAddress(userName, userEmail));
            message.Subject = "ברוך הבא למערכת!";
            string htmlBody = $@"
        <div style='font-family: Arial, sans-serif; direction: rtl; text-align: right; border: 1px solid #ddd; padding: 20px; border-radius: 10px;'>
            <h1 style='color: #3b82f6;'>היי {userName}, ברוך הבא!</h1>
            <p>שמחים מאוד שהצטרפת לקהילה שלנו.</p>
            <p>אנחנו כאן לכל שאלה, ומחכים לראות אותך מתחיל!</p>
            <br>
            <a href='http://localhost:4200/' 
               style='background-color: #3b82f6; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>
               לכניסה לאתר
            </a>
        </div>";

            message.Body = new TextPart("html") 
            {
                Text = htmlBody
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);

                client.Authenticate("chanaa710@gmail.com", "bybwmwmretmlbmie");

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
