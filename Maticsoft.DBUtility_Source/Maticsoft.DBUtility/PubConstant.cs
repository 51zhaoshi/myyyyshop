namespace Maticsoft.DBUtility
{
    using System;
    using System.Configuration;
    using System.Web;

    public class PubConstant
    {
        public static bool IsSQLServer = (GetConfigString("DAL") == "Maticsoft.SQLServerDAL");
        protected const string KEY_CONNECTION = "ConnectionString";
        protected const string KEY_ENCRYPT = "ConStringEncrypt";
        private const string SQLSERVERDAL = "Maticsoft.SQLServerDAL";

        public static object GetCache(string CacheKey)
        {
            return HttpRuntime.Cache[CacheKey];
        }

        public static string GetConfigString(string key)
        {
            string cacheKey = "AppSettings-" + key;
            object cache = GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = ConfigurationManager.AppSettings[key];
                    if (cache != null)
                    {
                        int num = 30;
                        SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return cache.ToString();
        }

        public static string GetConnectionString(string configName)
        {
            ConfigurationManager.RefreshSection("appSettings");
            string text = ConfigurationManager.AppSettings[configName];
            string str2 = ConfigurationManager.AppSettings["ConStringEncrypt"];
            if (str2 == "true")
            {
                text = DESEncrypt.Decrypt(text);
            }
            return text;
        }

        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            HttpRuntime.Cache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }

        public static string ConnectionString
        {
            get
            {
                ConfigurationManager.RefreshSection("appSettings");
                string text = ConfigurationManager.AppSettings["ConnectionString"];
                string str2 = ConfigurationManager.AppSettings["ConStringEncrypt"];
                if (str2 == "true")
                {
                    text = DESEncrypt.Decrypt(text);
                }
                return text;
            }
        }
    }
}

