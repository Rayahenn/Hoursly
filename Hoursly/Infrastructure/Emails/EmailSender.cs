using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Hoursly.Infrastructure.Emails
{
    public class EmailSender : IEmailSender
    {
        public void Send(
            string supervisorEmail,
            DateTime startDate,
            DateTime endDate)
        {
            var mailConfiguration = (MailConfiguration) ConfigurationManager.GetSection(MailConfiguration.SectionName);
            var mail = new MailMessage();
            var smtpServer = new SmtpClient(mailConfiguration.SmtpServer);

            mail.From = new MailAddress(mailConfiguration.FromLogin);
            mail.To.Add(supervisorEmail);
            mail.Subject = mailConfiguration.Subject;
            mail.Body = $"{mailConfiguration.Body}: {startDate} - {endDate}";

            smtpServer.Port = mailConfiguration.Port;
            smtpServer.Credentials = new NetworkCredential(mailConfiguration.FromLogin, mailConfiguration.FromPassword);
            smtpServer.EnableSsl = true;

            smtpServer.Send(mail);
        }
    }
}