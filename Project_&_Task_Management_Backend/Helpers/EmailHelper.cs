




using System.Net;
using System.Net.Mail;

namespace Project___Task_Management_Backend.Helpers
{
    public class EmailHelper
    {
        private readonly string _fromEmail;
        private readonly string _password;
        private readonly string _smtpHost;
        private readonly int _port;
        private readonly bool _enableSsl;

        public EmailHelper()
        {
            // Read values directly from .env
            _fromEmail = Environment.GetEnvironmentVariable("EMAIL_FROM");
            _password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");
            _smtpHost = Environment.GetEnvironmentVariable("SMTP_HOST");
            _port = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "587");
            _enableSsl = bool.Parse(Environment.GetEnvironmentVariable("ENABLE_SSL") ?? "true");
        }

        public bool Send(string toEmail, string subject, string message)
        {
            try
            {
                var mail = new MailMessage
                {
                    From = new MailAddress(_fromEmail, "Skedulo"),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mail.To.Add(toEmail);

                var smtp = new SmtpClient(_smtpHost, _port)
                {
                    Credentials = new NetworkCredential(_fromEmail, _password),
                    EnableSsl = _enableSsl
                };

                smtp.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email Error: " + ex.Message);
                return false;
            }
        }
    }
}

