using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Configuration;

namespace ConnApsEmailService
{
    /// <summary>
    /// A set of methods that allows ConnAps to send emails to its clients
    /// </summary>
    public class EmailService
    {
        /// <summary>
        /// The from email address
        /// </summary>
        private string ServiceEmail;
        /// <summary>
        /// The password of the from email
        /// </summary>
        private string Password;
        /// <summary>
        /// The smtp client's host address
        /// </summary>
        private string Host;
        /// <summary>
        /// The smtp client's port numbers
        /// </summary>
        private int Port;

        /// <summary>
        /// Initiates the needed fields. Takes them from the appsettings.config
        /// </summary>
        public EmailService()
        {
            ServiceEmail = ConfigurationManager.AppSettings["Email"];
            Password = ConfigurationManager.AppSettings["Password"];
            Host = ConfigurationManager.AppSettings["Host"];
            Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
        }

        /// <summary>
        /// A method that creates the smtp client, attaches the email information and sends the email
        /// </summary>
        /// <param name="mail"></param>
        private bool sendEmail(MailMessage mail)
        {
            var client = new SmtpClient(Host, Port)
            {
                EnableSsl = true,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(ServiceEmail, Password)
            };


            mail.BodyEncoding = UTF8Encoding.UTF8;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            try
            {
                client.Send(mail);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Creates the newly created tenant email, with the given email and password
        /// </summary>
        /// <param name="email">The email of the new user</param>
        /// <param name="password">The password generated</param>
        public bool SendTenantCreationEmail(string email, string password)
        {
            var mail = new MailMessage(ServiceEmail, email)
            {
                IsBodyHtml = true,
                Subject = "You are now connected to Connected Apartments",
                Body =
                    "Welcome to Connected Apartments. Your tenant account has been created.Your Credentials are: Username: " +
                    email + " Password:" + password
            };


            return sendEmail(mail);
        }

        /// <summary>
        /// Creates an email to greet the new Building Manager to the service
        /// </summary>
        /// <param name="email">The Email of the Building Manager</param>
        public bool SendBuildingCreationEmail(string email)
        {
            var mail = new MailMessage(ServiceEmail, email)
            {
                IsBodyHtml = true,
                Subject = "Welcome to Connected Apartments",
                Body = "Welcome to Connected Apartments. Your Building Manager account has been created. The next step to take is to create new Apartment."
            };


            return sendEmail(mail);
        }

        /// <summary>
        /// Sends an forgot password email with a newly generated password.
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <param name="password">The new password</param>
        public bool SendPasswordResetEmail(string email, string password)
        {
            var mail = new MailMessage(ServiceEmail, email)
            {
                Subject = "Your password has been reset",
                Body = "Your new password is " + password
            };


            return sendEmail(mail);
        }

        /// <summary>
        /// Sends a confirmation email for a new booking
        /// </summary>
        /// <param name="email">The email of the user</param>
        public bool SendNewBookingEmail(string email)
        {
            return false;
        }

        /// <summary>
        /// A function to test the service
        /// </summary>
        /// <param name="email">The email address of the recipient</param>
        public bool SendTestEmail(string email)
        {
            var mail = new MailMessage(ServiceEmail, email)
            {
                Subject = "This is a test",
                Body = "This is an email to test the email functionality of the system."
            };

            return sendEmail(mail);
        }
    }
}
