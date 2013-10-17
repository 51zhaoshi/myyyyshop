namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class SearchWordTop
    {
        private readonly ISearchWordTop dal = DASNS.CreateSearchWordTop();

        public int Add(Maticsoft.Model.SNS.SearchWordTop model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.SearchWordTop> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.SearchWordTop> list = new List<Maticsoft.Model.SNS.SearchWordTop>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.SearchWordTop item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int ID)
        {
            return this.dal.Delete(ID);
        }

        public bool DeleteList(string IDlist)
        {
            return this.dal.DeleteList(IDlist);
        }

        public bool Exists(string HotWord)
        {
            return this.dal.Exists(HotWord);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<Maticsoft.Model.SNS.SearchWordTop> GetCacheRecommadKeyWordList()
        {
            string cacheKey = "CacheRecommadKeys";
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetRecommadKeyWordList();
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (List<Maticsoft.Model.SNS.SearchWordTop>) cache;
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

        public DataSet GetListEx(string strWhere)
        {
            return this.dal.GetListEx(strWhere);
        }

        public Maticsoft.Model.SNS.SearchWordTop GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.SNS.SearchWordTop GetModelByCache(int ID)
        {
            string cacheKey = "SearchWordTopModel-" + ID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ID);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.SNS.SearchWordTop) cache;
        }

        public List<Maticsoft.Model.SNS.SearchWordTop> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.SNS.SearchWordTop> GetRecommadKeyWordList()
        {
            string cacheKey = "GetRecommadKeyWordList-";
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetModelList("");
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (List<Maticsoft.Model.SNS.SearchWordTop>) cache;
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.SNS.SearchWordTop model)
        {
            return this.dal.Update(model);
        }
    }
}

