using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace JobsPortal.Helper
{
    public class Email
    {
        //This is a static method named Emailsend.
        //It accepts parameters such as the sender's email address, email subject, email message,
        //and a boolean flag indicating whether the email body contains HTML content.
        //It returns a boolean value indicating whether the email was sent successfully.
        public static bool Emailsend(string SenderEmail, string Subject, string Message, bool IsBodyHtml = false)
        {
            bool status = false;

            //In the try block code reads SMTP server configuration settings from the application's Web.config using ConfigurationManager.
            //AppSettings. These settings include the host address, sender's email address, password, and port number.
            try
            {
                // Configuration settings from the Web.config file
                string HostAddress = "smtp.gmail.com";
                string FormEmailId = "priye98priya@gmail.com";

                string Password = "pfwp jejb uczf ajvr";
                string Port = "587";

                // Create a MailMessage
                // MailMessage is created to represent an email message.
                // It specifies the sender's address, email subject, message body, and whether the message body is HTML.
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(FormEmailId);
                mailMessage.Subject = Subject;
                mailMessage.Body = Message;
                mailMessage.IsBodyHtml = IsBodyHtml;
                mailMessage.To.Add(new MailAddress(SenderEmail));

                // Create an SmtpClient
                // An SmtpClient is created to send the email.
                // It specifies the SMTP server's host address, disables default credentials,
                // and sets the delivery method to use network delivery.
                SmtpClient smtp = new SmtpClient();
                smtp.Host = HostAddress;
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                // Set network credentials
                // NetworkCredential is used to set the network credentials for the SMTP server,
                // including the sender's email address and password.
                NetworkCredential networkCredential = new NetworkCredential();
                networkCredential.UserName = mailMessage.From.Address;
                networkCredential.Password = Password;

                // The SMTP port is set and SSL (Secure Sockets Layer) is enabled for secure email transmission.
                smtp.Credentials = networkCredential;
                smtp.Port = Convert.ToInt32(Port);
                smtp.EnableSsl = true;

                // Send the email
                // The Send method is called on the SmtpClient object to send the email.
                smtp.Send(mailMessage);

                status = true;
            }
            // If any exceptions occur during the process,
            // the catch block returns false to indicate that the email was not sent.
            catch (Exception)
            {
                return status;
            }

            return status;
        }
    }
}
