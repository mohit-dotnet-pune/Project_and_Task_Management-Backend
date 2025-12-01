

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

        public EmailHelper(IConfiguration config)
        {
            var section = config.GetSection("EmailSettings");

            _fromEmail = section.GetValue<string>("FromEmail");
            _password = section.GetValue<string>("Password");   
            _smtpHost = section.GetValue<string>("SmtpHost");
            _port = section.GetValue<int>("Port");
            _enableSsl = section.GetValue<bool>("EnableSsl");
        }

        public bool Send(string toEmail, string subject, string message)
        {
            try
            {
                var mail = new MailMessage
                {
                    From = new MailAddress(_fromEmail, "Project & Task Management"),  
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
