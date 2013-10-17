namespace Maticsoft.BLL.SysManage
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.SysManage;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Collections;
    using System.Data;
    using System.Runtime.InteropServices;

    public class ConfigSystem
    {
        private static IConfigSystem dal = DASysManage.CreateConfigSystem();

        public static int Add(string Keyname, string Value, string Description)
        {
            return dal.Add(Keyname, Value, Description);
        }

        public static int Add(string Keyname, string Value, string Description, ApplicationKeyType KeyType)
        {
            return dal.Add(Keyname, Value, Description, KeyType);
        }

        public static bool ClearCacheByKey(string key)
        {
            try
            {
                Hashtable hashListByCache = GetHashListByCache();
                if (((hashListByCache != null) && hashListByCache.ContainsKey(key)) && (hashListByCache[key] != null))
                {
                    hashListByCache.Remove(key);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ClearCacheByKeyAndType(ApplicationKeyType keyType, string key)
        {
            try
            {
                Hashtable hashListByCache = GetHashListByCache(keyType);
                if (((hashListByCache != null) && hashListByCache.ContainsKey(key)) && (hashListByCache[key] != null))
                {
                    hashListByCache.Remove(key);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void Delete(int ID)
        {
            dal.Delete(ID);
        }

        public static bool Exists(string Keyname)
        {
            return dal.Exists(Keyname);
        }

        public static bool GetBoolValueByCache(string Keyname)
        {
            Hashtable hashListByCache = GetHashListByCache();
            if (((hashListByCache != null) && hashListByCache.ContainsKey(Keyname)) && (hashListByCache[Keyname] != null))
            {
                return Globals.SafeBool(hashListByCache[Keyname], false);
            }
            return Globals.SafeBool(GetValue(Keyname), false);
        }

        public static decimal GetDecimalValueByCache(string Keyname)
        {
            Hashtable hashListByCache = GetHashListByCache();
            if (((hashListByCache != null) && hashListByCache.ContainsKey(Keyname)) && (hashListByCache[Keyname] != null))
            {
                return Globals.SafeDecimal(hashListByCache[Keyname], (decimal) -1M);
            }
            return Globals.SafeDecimal(GetValue(Keyname), (decimal) -1M);
        }

        public static Hashtable GetHashList()
        {
            DataSet list = dal.GetList("");
            Hashtable hashtable = new Hashtable();
            if (((list != null) && (list.Tables.Count > 0)) && (list.Tables[0] != null))
            {
                foreach (DataRow row in list.Tables[0].Rows)
                {
                    string key = row["Keyname"].ToString();
                    string str2 = row["Value"].ToString();
                    hashtable.Add(key, str2);
                }
            }
            return hashtable;
        }

        public static Hashtable GetHashList(ApplicationKeyType KeyType)
        {
            DataSet list = dal.GetList(" KeyType=" + ((int) KeyType));
            Hashtable hashtable = new Hashtable();
            if (((list != null) && (list.Tables.Count > 0)) && (list.Tables[0] != null))
            {
                foreach (DataRow row in list.Tables[0].Rows)
                {
                    string key = row["Keyname"].ToString();
                    string str2 = row["Value"].ToString();
                    hashtable.Add(key, str2);
                }
            }
            return hashtable;
        }

        public static Hashtable GetHashListByCache()
        {
            string cacheKey = "ConfigSystemHashList";
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = GetHashList();
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(GetValue("CacheTime"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Hashtable) cache;
        }

        public static Hashtable GetHashListByCache(ApplicationKeyType keyType)
        {
            string cacheKey = "ConfigSystemHashList_" + keyType;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = GetHashList(keyType);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(GetValue("CacheTime"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Hashtable) cache;
        }

        public static int GetIntValueByCache(string Keyname)
        {
            Hashtable hashListByCache = GetHashListByCache();
            if (((hashListByCache != null) && hashListByCache.ContainsKey(Keyname)) && (hashListByCache[Keyname] != null))
            {
                return Globals.SafeInt(hashListByCache[Keyname], -1);
            }
            return Globals.SafeInt(GetValue(Keyname), -1);
        }

        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public static string GetValue(int ID)
        {
            return dal.GetValue(ID);
        }

        public static string GetValue(string Keyname)
        {
            return dal.GetValue(Keyname);
        }

        public static string GetValueByCache(string Keyname)
        {
            Hashtable hashListByCache = GetHashListByCache();
            if (((hashListByCache != null) && hashListByCache.ContainsKey(Keyname)) && (hashListByCache[Keyname] != null))
            {
                return hashListByCache[Keyname].ToString();
            }
            return GetValue(Keyname);
        }

        public static string GetValueByCache(string Keyname, ApplicationKeyType KeyType)
        {
            Hashtable hashListByCache = GetHashListByCache(KeyType);
            if (((hashListByCache != null) && hashListByCache.ContainsKey(Keyname)) && (hashListByCache[Keyname] != null))
            {
                return hashListByCache[Keyname].ToString();
            }
            return GetValue(Keyname);
        }

        public static bool Modify(string keyname, string value, string description = "", ApplicationKeyType keyType = -1)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                description = keyname;
            }
            if (keyType == ApplicationKeyType.None)
            {
                if (Exists(keyname))
                {
                    return Update(keyname, value, description);
                }
                return (Add(keyname, value, description) > 0);
            }
            if (Exists(keyname))
            {
                return Update(keyname, value);
            }
            return (Add(keyname, value, description, keyType) > 0);
        }

        public static bool Update(string Keyname, string Value)
        {
            return dal.Update(Keyname, Value);
        }

        public static bool Update(string Keyname, string Value, ApplicationKeyType KeyType)
        {
            return dal.Update(Keyname, Value, KeyType);
        }

        public static bool Update(string Keyname, string Value, string Description)
        {
            return dal.Update(Keyname, Value, Description);
        }

        public static void Update(int ID, string Keyname, string Value, string Description)
        {
            dal.Update(ID, Keyname, Value, Description);
        }

        public static void UpdateConnectionString(string connectionString)
        {
            dal.UpdateConnectionString(connectionString);
        }
    }
}

