using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ConnApsEmailService
{
    public static class EmailService
    {
        private const string ServiceEmail = "conn.aps.sup@gmail.com";

        private static void SendEmail(MailMessage mail)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                //EnableSsl = true,
                Timeout = 10000,
                //DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true
                //Credentials = new NetworkCredential(ServiceEmail, Password)
            };


            mail.BodyEncoding = UTF8Encoding.UTF8;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            try
            {
                client.Send(mail);
            }
            catch(Exception e)
            {

            }
        }

        public static void SendTenantCreationEmail(string email, string password)
        {
            var mail = new MailMessage(ServiceEmail, email)
            {
                IsBodyHtml = true,
                Subject = "You are now connected to Connected Apartments",
                Body =
                    "Welcome to Connected Apartments. Your tenant account has been created.Your Credentials are: Username: " +
                    email + " Password:" + password
            };


            SendEmail(mail);
        }

        public static void SendBuildingCreationEmail(string email)
        {
            var mail = new MailMessage(ServiceEmail, email)
            {
                IsBodyHtml = true,
                Subject = "Welcome to Connected Apartments",
                Body = "Welcome to Connected Apartments. Your Building Manager account has been created. The next step to take is to create new Apartment."
            };


            SendEmail(mail);
        }

        public static void SendPasswordResetEmail(String email, string password)
        {
            var mail = new MailMessage(ServiceEmail, email)
            {
                Subject = "Your password has been reset",
                Body = "Your new password is " + password
            };


            SendEmail(mail);
        }

        public static void SendNewBookingEmail(string Email)
        {

        }
    }
}
