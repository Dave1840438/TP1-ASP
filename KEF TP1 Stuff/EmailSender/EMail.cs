using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailSender
{
    public class EMail
    {
        public String SenderName { get; set; }
        public String From { get; set; }
        public String Password { get; set; }
        public String To { get; set; }
        public String Subject { get; set; }
        public String Body { get; set; }
        public String Host { get; set; }
        public int HostPort { get; set; }
        public bool SSLSecurity { get; set; }

        public bool Send()
        {
            bool success = true;

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(To);
            mail.From = new System.Net.Mail.MailAddress(From, SenderName, System.Text.Encoding.UTF8);
            mail.Subject = Subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = Body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = System.Net.Mail.MailPriority.High;

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

            client.Credentials = new System.Net.NetworkCredential(From, Password);
            client.Port = HostPort; 
            client.Host = Host;
            client.EnableSsl = SSLSecurity; 

            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                success = false;
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
                HttpContext.Current.Response.Write(errorMessage);
            }
            return success;
        }
    }
}