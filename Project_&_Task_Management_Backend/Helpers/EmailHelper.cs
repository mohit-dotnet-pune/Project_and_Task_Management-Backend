using System.Net;

using System.Net.Mail;

namespace Project___Task_Management_Backend.Helpers
{

    public class EmailHelper

    {

        private readonly string FromEmail;

        private readonly string Password;

        private readonly string SmtpHost;

        private readonly int Port;

        private readonly bool EnableSsl;

        public EmailHelper(IConfiguration config)

        {

            var section = config.GetSection("EmailSettings");

            FromEmail = section.GetValue<string>("FromEmail");

            Password = section.GetValue<string>("Password");

            SmtpHost = section.GetValue<string>("SmtpHost");

            Port = section.GetValue<int>("Port");

            EnableSsl = section.GetValue<bool>("EnableSsl");

        }

        public bool Send(string toEmail, string subject, string message)

        {

            try

            {

                var smtp = new SmtpClient(SmtpHost)

                {

                    Port = Port,

                    Credentials = new NetworkCredential(FromEmail, Password),

                    EnableSsl = EnableSsl

                };

                var mail = new MailMessage(FromEmail, toEmail)

                {

                    Subject = subject,

                    Body = message,

                    IsBodyHtml = true

                };

                smtp.Send(mail);

                return true;

            }

            catch

            {

                return false;

            }

        }

    }

}


 