using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Email
{
    public class EmailService
    {
        private readonly string emailUsername = "bookstroreapp@gmail.com";
        private readonly string emailFrom = "bookstroreapp@gmail.com";
        private readonly string emailPassword = "jeqoztmboeqgefgu";
        private readonly string smtpClient = "smtp.gmail.com";

        public bool SendShopingConfirmationMail(string email)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient(smtpClient);

            mail.From = new MailAddress(emailFrom, "BookStore.com");
            mail.Headers.Add("Message-Id", "<BookStore>");

            mail.To.Add(email);

            mail.Subject = "Successful order";
            mail.Body = "<div style='text-align: center'>";
            mail.Body += "<div style='text-align: left'> Hello there,";
            mail.Body += "<br/><br/>";
            mail.Body += "You have successfully purchased books in our bookstore. You can expect the order at Your address within 2 to 5 working days.";
            mail.Body += "<br/><br/>";
            mail.Body += "Thank you, ";
            mail.Body += "<br/><br/>";
            mail.Body += "Your BookStore.";
            mail.Body += "</div>";

            string html = mail.Body;
            mail.IsBodyHtml = true;

            smtpServer.Port = 587;
            smtpServer.UseDefaultCredentials = false;
            smtpServer.Credentials = new System.Net.NetworkCredential(emailUsername, emailPassword);
            smtpServer.EnableSsl = true;

            smtpServer.Send(mail);

            return true;
        }
    }
}
