namespace Maticsoft.Common
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Web;
    using System.Xml;

    public class SmtpConfig
    {
        private static SmtpConfig _smtpConfig;

        private SmtpConfig()
        {
        }

        public static SmtpConfig Create()
        {
            if (_smtpConfig == null)
            {
                _smtpConfig = new SmtpConfig();
            }
            return _smtpConfig;
        }

        private string ConfigFile
        {
            get
            {
                string str = ConfigurationManager.AppSettings["SmtpConfigPath"];
                if (string.IsNullOrEmpty(str) || (str.Trim().Length == 0))
                {
                    return HttpContext.Current.Request.MapPath("/Config/SmtpSetting.config");
                }
                if (!Path.IsPathRooted(str))
                {
                    return HttpContext.Current.Request.MapPath(Path.Combine(str, "SmtpSetting.config"));
                }
                return Path.Combine(str, "SmtpSetting.config");
            }
        }

        public Maticsoft.Common.SmtpSetting SmtpSetting
        {
            get
            {
                XmlDocument document = new XmlDocument();
                document.Load(this.ConfigFile);
                return new Maticsoft.Common.SmtpSetting { Server = document.DocumentElement.SelectSingleNode("Server").InnerText, Authentication = Convert.ToBoolean(document.DocumentElement.SelectSingleNode("Authentication").InnerText), UserName = document.DocumentElement.SelectSingleNode("User").InnerText, Password = document.DocumentElement.SelectSingleNode("Password").InnerText, Sender = document.DocumentElement.SelectSingleNode("Sender").InnerText };
            }
        }
    }
}

