using Base.Application.Contracts;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Base.Infrastructure.Implementation
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string verificationCode, string subject, string toEmail)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("mayhie50@gmail.com", "retv hmxx tdty nlgu");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("mayhie50@gmail.com");
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("<h1>Verification Code</h1>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat($"<p>Your Verification Code: {verificationCode}</p>");
            mailMessage.Body = mailBody.ToString();

            client.Send(mailMessage);
        }
    }
}
