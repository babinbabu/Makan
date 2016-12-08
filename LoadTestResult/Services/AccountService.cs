using NextProperty.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace LoadTestResult.Services
{
    public class AccountService
    {
#if DEBUG
        private const string ServerUrl = "http://localhost:1992/";
#else
        private const string ServerUrl = "http://localhost:1992/";
#endif
        public static string ForgotPassword(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return "Invalid User Name";
            }
            using (Entity.db_LoadTest2010Entities db = new Entity.db_LoadTest2010Entities())
            {
                var entityUser = db.UserProfiles.FirstOrDefault(e => e.UserName == userName);
                if (entityUser == null )
                {
                    return "You are not registred with us";
                }
        
                string token = WebSecurity.GeneratePasswordResetToken(entityUser.UserName);

                SendPasswordResetMail(entityUser.UserName, token);
            }
            return "Successfully sent the password reset mail";
        }

        public static bool SendPasswordResetMail(string email, string resetToken)
        {
            try
            {
                string resetLink = ServerUrl + @"/Account/Reset/" + resetToken;
                string subject = "Reset Password Load Test";
                string content = EmailHelper.ConvertEmailHtmlToString(@"Emails\ResetEmail.html");
                content = string.Format(content, resetLink);

                EmailHelper.SendViaSendGrid(new List<string>() { email }, null, subject, content, string.Empty);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
    }
}