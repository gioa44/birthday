using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Birthday.Tools
{
    public static class MailSender
    {
        public static void SendMail(string mailTo, string subject, string body)
        {
            var mailConfig = GetMailConfigSection();

            MailAddress to = new MailAddress(mailTo);

            MailAddress from = new MailAddress(mailConfig.From);

            MailMessage mail = new MailMessage(from, to);

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.DeliveryFormat = SmtpDeliveryFormat.International;

            new Thread(x =>
            {
                smtp.SendAsync(mail, null);
            }).Start();
        }

        private static SmtpSection GetMailConfigSection()
        {
            return (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
        }
    }
}
