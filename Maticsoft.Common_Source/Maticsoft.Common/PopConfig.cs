namespace Maticsoft.Common
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Web;
    using System.Xml;

    public class PopConfig
    {
        private static PopConfig _popConfig;

        public static PopConfig Create()
        {
            if (_popConfig == null)
            {
                _popConfig = new PopConfig();
            }
            return _popConfig;
        }

        private string ConfigFile
        {
            get
            {
                string str = ConfigurationManager.AppSettings["PopConfigPath"];
                if (string.IsNullOrEmpty(str) || (str.Trim().Length == 0))
                {
                    return HttpContext.Current.Request.MapPath("/Config/PopSetting.config");
                }
                if (!Path.IsPathRooted(str))
                {
                    return HttpContext.Current.Request.MapPath(Path.Combine(str, "PopSetting.config"));
                }
                return Path.Combine(str, "PopSetting.config");
            }
        }

        public Maticsoft.Common.PopSetting PopSetting
        {
            get
            {
                XmlDocument document = new XmlDocument();
                document.Load(this.ConfigFile);
                return new Maticsoft.Common.PopSetting { Server = document.DocumentElement.SelectSingleNode("Server").InnerText, Port = Convert.ToInt32(document.DocumentElement.SelectSingleNode("Port").InnerText), UseSSL = Convert.ToBoolean(document.DocumentElement.SelectSingleNode("UseSSL").InnerText), UserName = document.DocumentElement.SelectSingleNode("User").InnerText, Password = document.DocumentElement.SelectSingleNode("Password").InnerText };
            }
        }
    }
}

