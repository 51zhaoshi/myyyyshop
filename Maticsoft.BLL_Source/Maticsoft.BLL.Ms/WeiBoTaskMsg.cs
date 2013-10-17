namespace Maticsoft.BLL.Ms
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Ms;
    using Maticsoft.Model.Ms;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class WeiBoTaskMsg
    {
        private readonly IWeiBoTaskMsg dal = DAMs.CreateWeiBoTaskMsg();

        public int Add(Maticsoft.Model.Ms.WeiBoTaskMsg model)
        {
            return this.dal.Add(model);
        }

        public int AddEx(Maticsoft.Model.Ms.WeiBoMsg model)
        {
            return this.dal.AddEx(model);
        }

        public List<Maticsoft.Model.Ms.WeiBoTaskMsg> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Ms.WeiBoTaskMsg> list = new List<Maticsoft.Model.Ms.WeiBoTaskMsg>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Ms.WeiBoTaskMsg item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int WeiBoTaskId)
        {
            return this.dal.Delete(WeiBoTaskId);
        }

        public bool DeleteList(string WeiBoTaskIdlist)
        {
            return this.dal.DeleteList(WeiBoTaskIdlist);
        }

        public bool Exists(int WeiBoTaskId)
        {
            return this.dal.Exists(WeiBoTaskId);
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

        public Maticsoft.Model.Ms.WeiBoTaskMsg GetModel(int WeiBoTaskId)
        {
            return this.dal.GetModel(WeiBoTaskId);
        }

        public Maticsoft.Model.Ms.WeiBoTaskMsg GetModelByCache(int WeiBoTaskId)
        {
            string cacheKey = "WeiBoTaskMsgModel-" + WeiBoTaskId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(WeiBoTaskId);
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
            return (Maticsoft.Model.Ms.WeiBoTaskMsg) cache;
        }

        public List<Maticsoft.Model.Ms.WeiBoTaskMsg> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool RunTask(int taskId)
        {
            Maticsoft.Model.Ms.WeiBoTaskMsg model = this.GetModel(taskId);
            if (model != null)
            {
                return this.dal.RunTask(model);
            }
            return true;
        }

        public bool Update(Maticsoft.Model.Ms.WeiBoTaskMsg model)
        {
            return this.dal.Update(model);
        }
    }
}

