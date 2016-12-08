using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace NextProperty.Web.Helpers
{
    public class EmailHelper
    {
        public static void SendViaSendGrid(List<string> tos, List<string> bccs, string subject, string html, string text)
        {
            MailMessage myMessage = new MailMessage();
            foreach (var to in tos)
            {
                if (IsValidEmail(to))
                {
                    myMessage.To.Add(to);
                }
            }
            if (bccs != null)
            {
                foreach (var bcc in bccs)
                {
                    if (IsValidEmail(bcc))
                    {
                        myMessage.Bcc.Add(bcc);
                    }
                }
            }
            myMessage.From = new MailAddress("agmoloadtest@gmail.com", "Load Test");
            myMessage.Subject = subject;

            myMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            myMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            // Init SmtpClient and send
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
            smtpClient.EnableSsl = true;
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("agmoloadtest@gmail.com", "p@ss1234");
            smtpClient.Credentials = credentials;

            smtpClient.Send(myMessage);
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string ConvertEmailHtmlToString(string emailHtmlRelativePath)
        {
            string actualPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, emailHtmlRelativePath);
            return File.ReadAllText(actualPath);
        }
    }
}