namespace Maticsoft.BLL.Settings
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Settings;
    using Maticsoft.Model.Settings;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Web;

    public class FilterWords
    {
        private readonly IFilterWords dal = DASettings.CreateFilterWords();

        public int Add(Maticsoft.Model.Settings.FilterWords model)
        {
            return this.dal.Add(model);
        }

        public void ClearCache()
        {
            HttpRuntime.Cache.Remove("ForbidWordRegEx");
        }

        public static bool ContainsDisWords(string str)
        {
            string cacheKey = "ContainsDisWords-" + str;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = false;
                    List<Maticsoft.Model.Settings.FilterWords> modelList = new Maticsoft.BLL.Settings.FilterWords().GetModelList("ActionType=0");
                    if ((modelList != null) && (modelList.Count > 0))
                    {
                        foreach (Maticsoft.Model.Settings.FilterWords words2 in modelList)
                        {
                            if (str.Contains(words2.WordPattern.Trim()))
                            {
                                cache = true;
                                break;
                            }
                        }
                    }
                    int num = Globals.SafeInt(ConfigSystem.GetValueByCache("ModelCache"), 30);
                    DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                }
                catch
                {
                }
            }
            return (bool) cache;
        }

        public static bool ContainsModWords(string str)
        {
            string cacheKey = "ContainsModWords-" + str;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = false;
                    List<Maticsoft.Model.Settings.FilterWords> modelList = new Maticsoft.BLL.Settings.FilterWords().GetModelList("ActionType=1");
                    if ((modelList != null) && (modelList.Count > 0))
                    {
                        foreach (Maticsoft.Model.Settings.FilterWords words2 in modelList)
                        {
                            if (str.Contains(words2.WordPattern.Trim()))
                            {
                                cache = true;
                                break;
                            }
                        }
                    }
                    int num = Globals.SafeInt(ConfigSystem.GetValueByCache("ModelCache"), 30);
                    DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                }
                catch
                {
                }
            }
            return (bool) cache;
        }

        public List<Maticsoft.Model.Settings.FilterWords> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Settings.FilterWords> list = new List<Maticsoft.Model.Settings.FilterWords>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Settings.FilterWords item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int FilterId)
        {
            return this.dal.Delete(FilterId);
        }

        public bool DeleteList(string FilterIdlist)
        {
            return this.dal.DeleteList(FilterIdlist);
        }

        public bool Exists(int FilterId)
        {
            return this.dal.Exists(FilterId);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public Maticsoft.Model.Settings.FilterWords GetByWordPattern(string wordPattern)
        {
            return this.dal.GetByWordPattern(wordPattern);
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Settings.FilterWords GetModel(int FilterId)
        {
            return this.dal.GetModel(FilterId);
        }

        public Maticsoft.Model.Settings.FilterWords GetModelByCache(int FilterId)
        {
            string cacheKey = "FilterWordsModel-" + FilterId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(FilterId);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.Settings.FilterWords) cache;
        }

        public List<Maticsoft.Model.Settings.FilterWords> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public static string ReplaceWords(string str)
        {
            Maticsoft.BLL.Settings.FilterWords words = new Maticsoft.BLL.Settings.FilterWords();
            string str2 = str;
            List<Maticsoft.Model.Settings.FilterWords> modelList = words.GetModelList("ActionType=2");
            if ((modelList != null) && (modelList.Count > 0))
            {
                foreach (Maticsoft.Model.Settings.FilterWords words2 in modelList)
                {
                    str2 = str2.Replace(words2.WordPattern, words2.RepalceWord);
                }
            }
            return str2;
        }

        public static string ReplaceWords(string str, out bool contains)
        {
            contains = false;
            Maticsoft.BLL.Settings.FilterWords words = new Maticsoft.BLL.Settings.FilterWords();
            string str2 = str;
            List<Maticsoft.Model.Settings.FilterWords> modelList = words.GetModelList("ActionType=2");
            if ((modelList != null) && (modelList.Count > 0))
            {
                foreach (Maticsoft.Model.Settings.FilterWords words2 in modelList)
                {
                    if (!contains)
                    {
                        contains = str2.Contains(words2.WordPattern);
                    }
                    str2 = str2.Replace(words2.WordPattern, words2.RepalceWord);
                }
            }
            return str2;
        }

        public bool Update(Maticsoft.Model.Settings.FilterWords model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateActionType(string ids, int type, string replace)
        {
            return this.dal.UpdateActionType(ids, type, replace);
        }
    }
}

