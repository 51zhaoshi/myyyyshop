namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class HotWords
    {
        private readonly IHotWords dal = DASNS.CreateHotWords();

        public int Add(Maticsoft.Model.SNS.HotWords model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.HotWords> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.HotWords> list = new List<Maticsoft.Model.SNS.HotWords>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.HotWords item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete()
        {
            return this.dal.Delete();
        }

        public bool Delete(int ID)
        {
            return this.dal.Delete(ID);
        }

        public bool DeleteList(string IDlist)
        {
            return this.dal.DeleteList(IDlist);
        }

        public bool Exists(string KeyWord)
        {
            return this.dal.Exists(KeyWord);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<Maticsoft.Model.SNS.HotWords> GetCacheRecommadKeyWordList()
        {
            string cacheKey = "CacheRecommadKey";
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
            return (List<Maticsoft.Model.SNS.HotWords>) cache;
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

        public int GetMaxSequence()
        {
            return this.dal.GetMaxSequence();
        }

        public Maticsoft.Model.SNS.HotWords GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.SNS.HotWords GetModelByCache(int ID)
        {
            string cacheKey = "HotWordsModel-" + ID;
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
            return (Maticsoft.Model.SNS.HotWords) cache;
        }

        public List<Maticsoft.Model.SNS.HotWords> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.SNS.HotWords> GetRecommadKeyWordList()
        {
            List<Maticsoft.Model.SNS.HotWords> list = new List<Maticsoft.Model.SNS.HotWords>();
            return this.GetModelList(" IsRecommend=1");
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetSearchList(string Keywords)
        {
            return this.dal.GetList(0, string.Format(" KeyWord like '%{0}%' ", Keywords), "");
        }

        public bool Update(Maticsoft.Model.SNS.HotWords model)
        {
            return this.dal.Update(model);
        }
    }
}

