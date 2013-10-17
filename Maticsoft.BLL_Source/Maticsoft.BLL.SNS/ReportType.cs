namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ReportType
    {
        private readonly IReportType dal = DASNS.CreateReportType();

        public int Add(Maticsoft.Model.SNS.ReportType model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.ReportType> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.ReportType> list = new List<Maticsoft.Model.SNS.ReportType>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.ReportType item = this.dal.DataRowToModel(dt.Rows[i]);
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

        public bool Exists(string TypeName)
        {
            return this.dal.Exists(TypeName);
        }

        public bool Exists(int ID, string TypeName)
        {
            return this.dal.Exists(ID, TypeName);
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

        public Maticsoft.Model.SNS.ReportType GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.SNS.ReportType GetModelByCache(int ID)
        {
            string cacheKey = "ReportTypeModel-" + ID;
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
            return (Maticsoft.Model.SNS.ReportType) cache;
        }

        public List<Maticsoft.Model.SNS.ReportType> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetSearchList(int Status, string Keywords)
        {
            string strWhere = "";
            if (Status != -1)
            {
                strWhere = string.Format(" Status={0} ", Status);
            }
            if (Keywords.Length > 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere = strWhere + " AND ";
                }
                strWhere = strWhere + string.Format(" TypeName like '%{0}%'", Keywords);
            }
            return this.dal.GetList(0, strWhere, " ID DESC ");
        }

        public bool Update(Maticsoft.Model.SNS.ReportType model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateList(int Status, string IdList)
        {
            return this.dal.UpdateList(Status, IdList);
        }
    }
}

