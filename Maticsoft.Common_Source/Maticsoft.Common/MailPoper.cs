namespace Maticsoft.Common
{
    using Maticsoft.Common.Mail;
    using System;
    using System.Collections.Generic;

    public class MailPoper
    {
        public static List<MailMessageEx> Receive()
        {
            PopSetting popSetting = PopConfig.Create().PopSetting;
            return Receive(popSetting.Server, popSetting.Port, popSetting.UseSSL, popSetting.UserName, popSetting.Password);
        }

        public static List<MailMessageEx> Receive(string hostname, int port, bool useSsl, string username, string password)
        {
            using (Pop3Client client = new Pop3Client(hostname, port, useSsl, username, password))
            {
                client.Trace += new Action<string>(Console.WriteLine);
                client.Authenticate();
                client.Stat();
                List<MailMessageEx> list = new List<MailMessageEx>();
                MailMessageEx ex = null;
                foreach (Pop3ListItem item in client.List())
                {
                    ex = client.RetrMailMessageEx(item);
                    if (ex != null)
                    {
                        client.Dele(item);
                        list.Add(ex);
                    }
                }
                client.Noop();
                client.Rset();
                client.Quit();
                return list;
            }
        }
    }
}

