namespace Maticsoft.Common
{
    using System;
    using System.Web;

    public class DataCache
    {
        public static void DeleteCache(string CacheKey)
        {
            HttpRuntime.Cache.Remove(CacheKey);
        }

        public static object GetCache(string CacheKey)
        {
            return HttpRuntime.Cache[CacheKey];
        }

        public static void SetCache(string CacheKey, object objObject)
        {
            HttpRuntime.Cache.Insert(CacheKey, objObject);
        }

        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            HttpRuntime.Cache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }
    }
}

