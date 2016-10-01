using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ConnApsEmailService
{
    public class EmailService
    {
        private const string ServiceEmail = "conn.aps.sup@gmail.com";
        private const string Password = "L-j&K9hd=/8}9wqTXt";

        private static void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(ServiceEmail, Password);

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

        public static void SendTenantCreationEmail(String email, String password)
        {
            MailMessage mail = new MailMessage(ServiceEmail, email);

            mail.Subject = "You are now connected to Connected Apartments";
            mail.Body = "Welcome to Connected Apartments. Your tenant account has been created.Your Credentials are: Username: "+email+" Password:"+password;

            SendEmail(mail);
        }

        public static void SendBuildingCreationEmail(String email)
        {
            MailMessage mail = new MailMessage(ServiceEmail, email);

            mail.Subject = "Welcome to Connected Apartments";
            mail.Body = "Welcome to Connected Apartments. Your Building Manager account has been created. The next step to take is to create new Apartment.";

            SendEmail(mail);
        }
    }
}
