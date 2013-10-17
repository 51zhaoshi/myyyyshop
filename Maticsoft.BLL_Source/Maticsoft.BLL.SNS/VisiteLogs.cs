namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class VisiteLogs
    {
        private readonly IVisiteLogs dal = DASNS.CreateVisiteLogs();

        public int Add(Maticsoft.Model.SNS.VisiteLogs model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.VisiteLogs> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.VisiteLogs> list = new List<Maticsoft.Model.SNS.VisiteLogs>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.VisiteLogs item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int VisitID)
        {
            return this.dal.Delete(VisitID);
        }

        public bool DeleteList(string VisitIDlist)
        {
            return this.dal.DeleteList(VisitIDlist);
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

        public Maticsoft.Model.SNS.VisiteLogs GetModel(int VisitID)
        {
            return this.dal.GetModel(VisitID);
        }

        public Maticsoft.Model.SNS.VisiteLogs GetModelByCache(int VisitID)
        {
            string cacheKey = "VisiteLogsModel-" + VisitID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(VisitID);
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
            return (Maticsoft.Model.SNS.VisiteLogs) cache;
        }

        public List<Maticsoft.Model.SNS.VisiteLogs> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.SNS.VisiteLogs model)
        {
            return this.dal.Update(model);
        }
    }
}

