using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CASCO.DAO
{
    class Helper
    {

        public static void SendEmail(string from, string to, string title, string body, bool isHTML, string server, string usuario, string pwd, int puerto)
        {
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();

            correo.From = new System.Net.Mail.MailAddress(from);
            correo.To.Add(to);
            correo.Subject = title;
            correo.Body = body;
            correo.IsBodyHtml = isHTML;
            correo.Priority = System.Net.Mail.MailPriority.Normal;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(server);
            smtp.Port = puerto;
            smtp.Credentials = new System.Net.NetworkCredential(usuario, pwd);

            smtp.Send(correo);
        }

        internal static void SendSMS(string servidor, string usuario, string pass, int puerto, string telefono, string p)
        {
            throw new NotImplementedException();
        }

    }
}
