namespace Maticsoft.Payment.Configuration
{
    using Maticsoft.Payment.Core;
    using System;
    using System.Configuration;
    using System.Web.Configuration;

    public sealed class WebConfigHelper
    {
        public static bool GetConfigBool(string key)
        {
            bool flag = false;
            string configString = GetConfigString(key);
            if (!string.IsNullOrWhiteSpace(configString))
            {
                try
                {
                    flag = bool.Parse(configString);
                }
                catch (FormatException)
                {
                }
            }
            return flag;
        }

        public static decimal GetConfigDecimal(string key)
        {
            decimal num = 0M;
            string configString = GetConfigString(key);
            if (!string.IsNullOrWhiteSpace(configString))
            {
                try
                {
                    num = decimal.Parse(configString);
                }
                catch (FormatException)
                {
                }
            }
            return num;
        }

        public static int GetConfigInt(string key)
        {
            int num = 0;
            string configString = GetConfigString(key);
            if (!string.IsNullOrWhiteSpace(configString))
            {
                try
                {
                    num = int.Parse(configString);
                }
                catch (FormatException)
                {
                }
            }
            return num;
        }

        public static string GetConfigString(string key)
        {
            string str = "AppSettings-" + key;
            object obj2 = DataCache.Get(str);
            if (obj2 == null)
            {
                try
                {
                    System.Configuration.Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");
                    if ((0 < configuration.AppSettings.Settings.Count) && (configuration.AppSettings.Settings[key] != null))
                    {
                        obj2 = configuration.AppSettings.Settings[key].Value;
                        DataCache.Insert(str, obj2, 180);
                    }
                }
                catch
                {
                }
            }
            if (obj2 == null)
            {
                return null;
            }
            return obj2.ToString();
        }
    }
}

