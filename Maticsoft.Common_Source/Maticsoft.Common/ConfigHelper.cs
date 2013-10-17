namespace Maticsoft.Common
{
    using System;
    using System.Configuration;
    using System.Web.Configuration;

    public sealed class ConfigHelper
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
            string cacheKey = "AppSettings-" + key;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    System.Configuration.Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");
                    if ((0 < configuration.AppSettings.Settings.Count) && (configuration.AppSettings.Settings[key] != null))
                    {
                        cache = configuration.AppSettings.Settings[key].Value;
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes(180.0), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            if (cache == null)
            {
                return null;
            }
            return cache.ToString();
        }
    }
}

