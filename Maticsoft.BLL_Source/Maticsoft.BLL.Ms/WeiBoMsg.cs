namespace Maticsoft.BLL.Ms
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Ms;
    using Maticsoft.Model.Ms;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class WeiBoMsg
    {
        private readonly IWeiBoMsg dal = DAMs.CreateWeiBoMsg();

        public int Add(Maticsoft.Model.Ms.WeiBoMsg model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Ms.WeiBoMsg> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Ms.WeiBoMsg> list = new List<Maticsoft.Model.Ms.WeiBoMsg>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Ms.WeiBoMsg item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int WeiBoId)
        {
            return this.dal.Delete(WeiBoId);
        }

        public bool DeleteList(string WeiBoIdlist)
        {
            return this.dal.DeleteList(WeiBoIdlist);
        }

        public bool Exists(int WeiBoId)
        {
            return this.dal.Exists(WeiBoId);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
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

        public Maticsoft.Model.Ms.WeiBoMsg GetModel(int WeiBoId)
        {
            return this.dal.GetModel(WeiBoId);
        }

        public Maticsoft.Model.Ms.WeiBoMsg GetModelByCache(int WeiBoId)
        {
            string cacheKey = "WeiBoMsgModel-" + WeiBoId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(WeiBoId);
                    if (cache != null)
                    {
                        int configInt = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) configInt), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.Ms.WeiBoMsg) cache;
        }

        public List<Maticsoft.Model.Ms.WeiBoMsg> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Ms.WeiBoMsg model)
        {
            return this.dal.Update(model);
        }
    }
}

