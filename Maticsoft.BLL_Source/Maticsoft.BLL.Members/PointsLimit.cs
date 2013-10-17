namespace Maticsoft.BLL.Members
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class PointsLimit
    {
        private readonly IPointsLimit dal = DAMembers.CreatePointsLimit();

        public int Add(Maticsoft.Model.Members.PointsLimit model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Members.PointsLimit> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.PointsLimit> list = new List<Maticsoft.Model.Members.PointsLimit>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Members.PointsLimit item = new Maticsoft.Model.Members.PointsLimit();
                    if ((dt.Rows[i]["PointsLimitID"] != null) && (dt.Rows[i]["PointsLimitID"].ToString() != ""))
                    {
                        item.PointsLimitID = int.Parse(dt.Rows[i]["PointsLimitID"].ToString());
                    }
                    if ((dt.Rows[i]["Cycle"] != null) && (dt.Rows[i]["Cycle"].ToString() != ""))
                    {
                        item.Cycle = int.Parse(dt.Rows[i]["Cycle"].ToString());
                    }
                    if ((dt.Rows[i]["CycleUnit"] != null) && (dt.Rows[i]["CycleUnit"].ToString() != ""))
                    {
                        item.CycleUnit = dt.Rows[i]["CycleUnit"].ToString();
                    }
                    if ((dt.Rows[i]["MaxTimes"] != null) && (dt.Rows[i]["MaxTimes"].ToString() != ""))
                    {
                        item.MaxTimes = int.Parse(dt.Rows[i]["MaxTimes"].ToString());
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int PointsLimitID)
        {
            return this.dal.Delete(PointsLimitID);
        }

        public bool DeleteEX(int PointsLimitID)
        {
            return this.dal.DeleteEX(PointsLimitID);
        }

        public bool DeleteList(string PointsLimitIDlist)
        {
            return this.dal.DeleteList(PointsLimitIDlist);
        }

        public bool Exists(int PointsLimitID)
        {
            return this.dal.Exists(PointsLimitID);
        }

        public bool Exists(string name)
        {
            return (this.GetRecordCount("Name='" + name + "'") > 0);
        }

        public bool Exists(string name, int limitid)
        {
            return (this.GetRecordCount(string.Concat(new object[] { "Name='", name, "' and PointsLimitID!=", limitid })) > 0);
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

        public Maticsoft.Model.Members.PointsLimit GetModel(int PointsLimitID)
        {
            return this.dal.GetModel(PointsLimitID);
        }

        public Maticsoft.Model.Members.PointsLimit GetModelByCache(int PointsLimitID)
        {
            string cacheKey = "PointsLimitModel-" + PointsLimitID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(PointsLimitID);
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
            return (Maticsoft.Model.Members.PointsLimit) cache;
        }

        public List<Maticsoft.Model.Members.PointsLimit> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Members.PointsLimit model)
        {
            return this.dal.Update(model);
        }
    }
}

