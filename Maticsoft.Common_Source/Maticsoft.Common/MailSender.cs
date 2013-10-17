namespace Maticsoft.Common
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    public class MailSender
    {
        public static void Send(string recipient, string subject, string body)
        {
            Send(SmtpConfig.Create().SmtpSetting.Sender, recipient, "", subject, body, true, Encoding.Default, true, null);
        }

        public static void Send(string Recipient, string Sender, string Subject, string Body)
        {
            Send(Sender, Recipient, "", Subject, Body, true, Encoding.UTF8, true, null);
        }

        public static void Send(string tomail, string bccmail, string subject, string body, params string[] files)
        {
            Send(SmtpConfig.Create().SmtpSetting.Sender, tomail, bccmail, subject, body, true, Encoding.Default, true, files);
        }

        public static void Send(string frommail, string tomail, string bccmail, string subject, string body, bool isBodyHtml, Encoding encoding, bool isAuthentication, params string[] files)
        {
            Send(SmtpConfig.Create().SmtpSetting.Server, SmtpConfig.Create().SmtpSetting.UserName, SmtpConfig.Create().SmtpSetting.Password, frommail, tomail, "", bccmail, subject, body, isBodyHtml, encoding, isAuthentication, false, files);
        }

        public static void Send(string server, string username, string password, string frommail, string tomail, string ccmail, string bccmail, string subject, string body, bool isBodyHtml, Encoding encoding, bool isAuthentication, bool isSsl, params string[] files)
        {
            SmtpClient client = new SmtpClient(server);
            MailMessage message = new MailMessage(frommail, tomail);
            if (bccmail.Length > 1)
            {
                foreach (string str in StringPlus.GetStrArray(bccmail))
                {
                    if (str.Trim() != "")
                    {
                        MailAddress item = new MailAddress(str.Trim());
                        message.Bcc.Add(item);
                    }
                }
            }
            if (ccmail.Length > 1)
            {
                foreach (string str2 in StringPlus.GetStrArray(ccmail))
                {
                    if (str2.Trim() != "")
                    {
                        MailAddress address2 = new MailAddress(str2.Trim());
                        message.CC.Add(address2);
                    }
                }
            }
            message.IsBodyHtml = isBodyHtml;
            message.SubjectEncoding = encoding;
            message.BodyEncoding = encoding;
            message.Subject = subject;
            message.Body = body;
            message.Attachments.Clear();
            if ((files != null) && (files.Length != 0))
            {
                for (int i = 0; i < files.Length; i++)
                {
                    Attachment attachment = new Attachment(files[i]);
                    message.Attachments.Add(attachment);
                }
            }
            if (isAuthentication)
            {
                client.Credentials = new NetworkCredential(username, password);
                client.EnableSsl = true;
            }
            client.Send(message);
            message.Attachments.Dispose();
        }

        public static void Send(string server, string username, string password, string frommail, string tomail, string ccmail, string bccmail, string replymail, string subject, string body, bool isBodyHtml, Encoding encoding, bool isAuthentication, bool isSsl, params string[] files)
        {
            SmtpClient client = new SmtpClient(server);
            MailMessage message = new MailMessage(frommail, tomail);
            if (bccmail.Length > 1)
            {
                foreach (string str in StringPlus.GetStrArray(bccmail))
                {
                    if (str.Trim() != "")
                    {
                        MailAddress item = new MailAddress(str.Trim());
                        message.Bcc.Add(item);
                    }
                }
            }
            if (ccmail.Length > 1)
            {
                foreach (string str2 in StringPlus.GetStrArray(ccmail))
                {
                    if (str2.Trim() != "")
                    {
                        MailAddress address2 = new MailAddress(str2.Trim());
                        message.CC.Add(address2);
                    }
                }
            }
            if (replymail.Length > 1)
            {
                foreach (string str3 in StringPlus.GetStrArray(replymail))
                {
                    if (str3.Trim() != "")
                    {
                        MailAddress address3 = new MailAddress(str3.Trim());
                        message.ReplyToList.Add(address3);
                    }
                }
            }
            message.IsBodyHtml = isBodyHtml;
            message.SubjectEncoding = encoding;
            message.BodyEncoding = encoding;
            message.Subject = subject;
            message.Body = body;
            message.Attachments.Clear();
            if ((files != null) && (files.Length != 0))
            {
                for (int i = 0; i < files.Length; i++)
                {
                    Attachment attachment = new Attachment(files[i]);
                    message.Attachments.Add(attachment);
                }
            }
            if (isAuthentication)
            {
                client.Credentials = new NetworkCredential(username, password);
                client.EnableSsl = true;
            }
            client.Send(message);
            message.Attachments.Dispose();
        }
    }
}

