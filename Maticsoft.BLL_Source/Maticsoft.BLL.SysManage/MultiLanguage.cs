namespace Maticsoft.BLL.SysManage
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.SysManage;
    using System;
    using System.Collections;
    using System.Data;

    public class MultiLanguage
    {
        private readonly IMultiLanguage dal = DASysManage.CreateMultiLanguage();

        public int Add(string MultiLang_iField, int MultiLang_iPKValue, string MultiLang_cLang, string MultiLang_cValue)
        {
            return this.dal.Add(MultiLang_iField, MultiLang_iPKValue, MultiLang_cLang, MultiLang_cValue);
        }

        public void Delete(int MultiLang_iID)
        {
            this.dal.Delete(MultiLang_iID);
        }

        public bool Exists(string MultiLang_iField, int MultiLang_iPKValue, string MultiLang_cLang)
        {
            return this.dal.Exists(MultiLang_iField, MultiLang_iPKValue, MultiLang_cLang);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetAllListByCache()
        {
            string cacheKey = "GetAllListMultiLang";
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetList("");
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("CacheTime"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (DataSet) cache;
        }

        public string GetDefaultLangCodeByCache()
        {
            string cacheKey = "DefaultLanguageCode";
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetDefaultLangCode();
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("CacheTime"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return cache.ToString();
        }

        public Hashtable GetHashValueListByLang(string MultiLang_iField, string MultiLang_cLang)
        {
            DataSet valueListByLang = this.dal.GetValueListByLang(MultiLang_iField, MultiLang_cLang);
            Hashtable hashtable = new Hashtable();
            if ((valueListByLang.Tables.Count > 0) && (valueListByLang.Tables[0] != null))
            {
                foreach (DataRow row in valueListByLang.Tables[0].Rows)
                {
                    string key = row["MultiLang_iPKValue"].ToString();
                    string str2 = row["MultiLang_cValue"].ToString();
                    hashtable.Add(key, str2);
                }
            }
            return hashtable;
        }

        public Hashtable GetHashValueListByLangCache(string MultiLang_iField, string MultiLang_cLang)
        {
            string cacheKey = "HashValueListByLang" + MultiLang_iField + MultiLang_cLang;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetHashValueListByLang(MultiLang_iField, MultiLang_cLang);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("CacheTime"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Hashtable) cache;
        }

        public DataSet GetLangListByValue(string MultiLang_iField, int MultiLang_iPKValue)
        {
            return this.dal.GetLangListByValue(MultiLang_iField, MultiLang_iPKValue);
        }

        public DataSet GetLanguageList()
        {
            return this.dal.GetLanguageList();
        }

        public DataSet GetLanguageListByCache()
        {
            string cacheKey = "GetLanguageList";
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetLanguageList();
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("CacheTime"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (DataSet) cache;
        }

        public string GetLanguageNameByCache(string Language_cCode)
        {
            string cacheKey = "Language-" + Language_cCode;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetLanguageName(Language_cCode);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("CacheTime"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return cache.ToString();
        }

        public string GetLangValue(int MultiLang_iID)
        {
            return this.dal.GetModel(MultiLang_iID);
        }

        public string GetLangValue(string MultiLang_iField, int MultiLang_iPKValue, string MultiLang_cLang)
        {
            return this.dal.GetModel(MultiLang_iField, MultiLang_iPKValue, MultiLang_cLang);
        }

        public string GetLangValueByCache(int MultiLang_iID)
        {
            string cacheKey = "MultiLangModel-" + MultiLang_iID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(MultiLang_iID);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("CacheTime"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return cache.ToString();
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public DataSet GetValueListByLang(string MultiLang_iField, string MultiLang_cLang)
        {
            return this.dal.GetValueListByLang(MultiLang_iField, MultiLang_cLang);
        }

        public void Update(int MultiLang_iID, string MultiLang_cValue)
        {
            this.dal.Update(MultiLang_iID, MultiLang_cValue);
        }
    }
}

