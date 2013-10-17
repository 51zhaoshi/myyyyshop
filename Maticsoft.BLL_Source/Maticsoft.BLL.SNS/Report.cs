namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Report
    {
        private readonly IReport dal = DASNS.CreateReport();

        public int Add(Maticsoft.Model.SNS.Report model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.Report> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.Report> list = new List<Maticsoft.Model.SNS.Report>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.Report item = this.dal.DataRowToModel(dt.Rows[i]);
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

        public bool Exists(int ID)
        {
            return this.dal.Exists(ID);
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

        public DataSet GetListEx(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetListEx(Top, strWhere, filedOrder);
        }

        public Maticsoft.Model.SNS.Report GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.SNS.Report GetModelByCache(int ID)
        {
            string cacheKey = "ReportModel-" + ID;
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
            return (Maticsoft.Model.SNS.Report) cache;
        }

        public List<Maticsoft.Model.SNS.Report> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetSearchList(string strWhere)
        {
            return this.GetListEx(0, strWhere, "CreatedDate Desc");
        }

        public bool Update(Maticsoft.Model.SNS.Report model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateReportStatus(int status, int reportId)
        {
            return this.dal.UpdateReportStatus(status, reportId);
        }

        public bool UpdateReportStatus(int status, string reportIds)
        {
            return this.dal.UpdateReportStatus(status, reportIds);
        }
    }
}

