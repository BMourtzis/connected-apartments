﻿using System;
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
            mail.Body = "<html><body><h1> Welcome to Connected Apartments </h1><p> Your tenant account has been created.Your Credentials are: </p><p> Username:"+email+" </p>< p > Password:"+password+" </p></body></html> ";

            SendEmail(mail);
        }
    }
}
