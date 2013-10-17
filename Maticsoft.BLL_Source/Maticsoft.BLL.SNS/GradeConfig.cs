namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class GradeConfig
    {
        private readonly IGradeConfig dal = DASNS.CreateGradeConfig();

        public int Add(Maticsoft.Model.SNS.GradeConfig model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.GradeConfig> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.GradeConfig> list = new List<Maticsoft.Model.SNS.GradeConfig>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.GradeConfig item = new Maticsoft.Model.SNS.GradeConfig();
                    if ((dt.Rows[i]["GradeID"] != null) && (dt.Rows[i]["GradeID"].ToString() != ""))
                    {
                        item.GradeID = int.Parse(dt.Rows[i]["GradeID"].ToString());
                    }
                    if ((dt.Rows[i]["GradeName"] != null) && (dt.Rows[i]["GradeName"].ToString() != ""))
                    {
                        item.GradeName = dt.Rows[i]["GradeName"].ToString();
                    }
                    if ((dt.Rows[i]["MinRange"] != null) && (dt.Rows[i]["MinRange"].ToString() != ""))
                    {
                        item.MinRange = new int?(int.Parse(dt.Rows[i]["MinRange"].ToString()));
                    }
                    if ((dt.Rows[i]["MaxRange"] != null) && (dt.Rows[i]["MaxRange"].ToString() != ""))
                    {
                        item.MaxRange = new int?(int.Parse(dt.Rows[i]["MaxRange"].ToString()));
                    }
                    if ((dt.Rows[i]["Remark"] != null) && (dt.Rows[i]["Remark"].ToString() != ""))
                    {
                        item.Remark = dt.Rows[i]["Remark"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int GradeID)
        {
            return this.dal.Delete(GradeID);
        }

        public bool DeleteList(string GradeIDlist)
        {
            return this.dal.DeleteList(GradeIDlist);
        }

        public bool Exists(int GradeID)
        {
            return this.dal.Exists(GradeID);
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

        public Maticsoft.Model.SNS.GradeConfig GetModel(int GradeID)
        {
            return this.dal.GetModel(GradeID);
        }

        public Maticsoft.Model.SNS.GradeConfig GetModelByCache(int GradeID)
        {
            string cacheKey = "GradeConfigModel-" + GradeID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(GradeID);
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
            return (Maticsoft.Model.SNS.GradeConfig) cache;
        }

        public List<Maticsoft.Model.SNS.GradeConfig> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public string GetUserLevel(int? grades)
        {
            if (grades.HasValue)
            {
                Maticsoft.Model.SNS.GradeConfig userLevel = this.dal.GetUserLevel(grades);
                if (userLevel != null)
                {
                    return userLevel.GradeName;
                }
            }
            return "0";
        }

        public bool Update(Maticsoft.Model.SNS.GradeConfig model)
        {
            return this.dal.Update(model);
        }
    }
}

