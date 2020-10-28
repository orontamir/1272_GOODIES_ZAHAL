using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1272_GOODIES_ZAHAL.Email
{
    public class SendMail
    {
        /// <summary>
        /// The singleton instance
        /// </summary>
        private static SendMail m_ins;
        private static readonly ILog log = LogManager.GetLogger(typeof(SendMail));
        public static SendMail Instance()
        {
            if (m_ins == null)
            {
                m_ins = new SendMail();
            }
            return m_ins;
        }

        static bool mailSent = false;
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            string token = (string)e.UserState;

            if (e.Cancelled)
            {
                log.Debug("[{0}] Send canceled.");
            }
            if (e.Error != null)
            {
                log.Debug($"[{token}] {e.Error.ToString()}");
            }
            else
            {
                log.Debug("Message sent.");
            }
            mailSent = true;
        }
        public void Email(string body, string title)
        {
            string addresses = ConfigurationManager.AppSettings["addresses"];
            ManualResetEvent _ThreadsEvent = new ManualResetEvent(false);
            try
            {
                MailMessage mail = new MailMessage();
                mail.Body = body;
                mail.IsBodyHtml = true;
                foreach (var address in addresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mail.To.Add(address);
                }
                // mail.To.Add(new MailAddress(email));
                mail.From = new MailAddress("NoReply@delek.co.il", "NoReply@delek.co.il", Encoding.UTF8);
                mail.Subject = title;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.Normal;
                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new System.Net.NetworkCredential("Webjob", "!1qazxsw2");
                smtp.Host = "timtam.delek.loc";
                smtp.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                string userState = "send message";
                mailSent = false;
                smtp.SendAsync(mail, userState);
                _ThreadsEvent.WaitOne(10000);
                if (mailSent == false)
                {
                    smtp.SendAsyncCancel();
                }
                else
                {
                    log.Debug($"Email was send to user {addresses} succesfully");
                }
                // Clean up.
                mail.Dispose();


            }
            catch (SmtpException ex)
            {
                log.Error($"Error message: {ex.StatusCode}");
            }
            catch (Exception ex)
            {
                log.Error($"Error when try send email to user {addresses}, error message: {ex.Message}");


            }

        }
    }
}

