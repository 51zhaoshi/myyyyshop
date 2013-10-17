namespace Maticsoft.BLL.Members
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class PointsDetail
    {
        private readonly IPointsDetail dal = DAMembers.CreatePointsDetail();

        public int Add(Maticsoft.Model.Members.PointsDetail model)
        {
            return this.dal.Add(model);
        }

        public int AddPoints(string ruleaction, int userid, string desc, string extdata = "")
        {
            Maticsoft.Model.Members.PointsRule model = new Maticsoft.BLL.Members.PointsRule().GetModel(ruleaction);
            Maticsoft.Model.Members.PointsDetail detail = new Maticsoft.Model.Members.PointsDetail();
            string valueByCache = ConfigSystem.GetValueByCache("PointEnable");
            if (!this.isLimit(model, userid) && (valueByCache == "true"))
            {
                detail.CreatedDate = DateTime.Now;
                detail.Description = desc;
                detail.ExtData = extdata;
                detail.Score = model.Score;
                detail.RuleAction = ruleaction;
                detail.UserID = userid;
                detail.Type = 0;
                if (this.dal.AddDetail(detail))
                {
                    return model.Score;
                }
            }
            return 0;
        }

        public bool AddPointsByBuy(int userid, int score, string desc, string extdata = "")
        {
            Maticsoft.Model.Members.PointsDetail model = new Maticsoft.Model.Members.PointsDetail {
                CreatedDate = DateTime.Now,
                Description = desc,
                ExtData = extdata,
                Score = score,
                RuleAction = "Buy",
                Type = 0,
                UserID = userid
            };
            return this.dal.AddDetail(model);
        }

        public List<Maticsoft.Model.Members.PointsDetail> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.PointsDetail> list = new List<Maticsoft.Model.Members.PointsDetail>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Members.PointsDetail item = new Maticsoft.Model.Members.PointsDetail();
                    if ((dt.Rows[i]["PointsDetailID"] != null) && (dt.Rows[i]["PointsDetailID"].ToString() != ""))
                    {
                        item.PointsDetailID = int.Parse(dt.Rows[i]["PointsDetailID"].ToString());
                    }
                    if ((dt.Rows[i]["RuleAction"] != null) && (dt.Rows[i]["RuleAction"].ToString() != ""))
                    {
                        item.RuleAction = dt.Rows[i]["RuleAction"].ToString();
                    }
                    if ((dt.Rows[i]["UserID"] != null) && (dt.Rows[i]["UserID"].ToString() != ""))
                    {
                        item.UserID = int.Parse(dt.Rows[i]["UserID"].ToString());
                    }
                    if ((dt.Rows[i]["Score"] != null) && (dt.Rows[i]["Score"].ToString() != ""))
                    {
                        item.Score = int.Parse(dt.Rows[i]["Score"].ToString());
                    }
                    if ((dt.Rows[i]["ExtData"] != null) && (dt.Rows[i]["ExtData"].ToString() != ""))
                    {
                        item.ExtData = dt.Rows[i]["ExtData"].ToString();
                    }
                    if ((dt.Rows[i]["CurrentPoints"] != null) && (dt.Rows[i]["CurrentPoints"].ToString() != ""))
                    {
                        item.CurrentPoints = int.Parse(dt.Rows[i]["CurrentPoints"].ToString());
                    }
                    if ((dt.Rows[i]["Description"] != null) && (dt.Rows[i]["Description"].ToString() != ""))
                    {
                        item.Description = dt.Rows[i]["Description"].ToString();
                    }
                    if ((dt.Rows[i]["CreatedDate"] != null) && (dt.Rows[i]["CreatedDate"].ToString() != ""))
                    {
                        item.CreatedDate = DateTime.Parse(dt.Rows[i]["CreatedDate"].ToString());
                    }
                    if ((dt.Rows[i]["Type"] != null) && (dt.Rows[i]["Type"].ToString() != ""))
                    {
                        item.Type = int.Parse(dt.Rows[i]["Type"].ToString());
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int PointsDetailID)
        {
            return this.dal.Delete(PointsDetailID);
        }

        public bool DeleteList(string PointsDetailIDlist)
        {
            return this.dal.DeleteList(PointsDetailIDlist);
        }

        public bool Exists(int PointsDetailID)
        {
            return this.dal.Exists(PointsDetailID);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public int GetCount(int userid, string unit, int cycle, string ruleaction)
        {
            return this.dal.GetRecordCount(string.Concat(new object[] { " userid=", userid, " and ruleaction='", ruleaction, "' and datediff( ", unit, ", CreatedDate, GETDATE())<", cycle }));
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

        public List<Maticsoft.Model.Members.PointsDetail> GetListByPageEX(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DataSet set = this.GetListByPage(strWhere, orderby, startIndex, endIndex);
            return this.DataTableToList(set.Tables[0]);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Members.PointsDetail GetModel(int PointsDetailID)
        {
            return this.dal.GetModel(PointsDetailID);
        }

        public Maticsoft.Model.Members.PointsDetail GetModelByCache(int PointsDetailID)
        {
            string cacheKey = "PointsDetailModel-" + PointsDetailID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(PointsDetailID);
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
            return (Maticsoft.Model.Members.PointsDetail) cache;
        }

        public List<Maticsoft.Model.Members.PointsDetail> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool isLimit(Maticsoft.Model.Members.PointsRule Rule, int userid)
        {
            Maticsoft.BLL.Members.PointsLimit limit = new Maticsoft.BLL.Members.PointsLimit();
            if (Rule == null)
            {
                return true;
            }
            if (Rule.PointsLimitID < 0)
            {
                return false;
            }
            Maticsoft.Model.Members.PointsLimit model = limit.GetModel(Rule.PointsLimitID);
            return (this.GetCount(userid, model.CycleUnit, model.Cycle, Rule.RuleAction) >= model.MaxTimes);
        }

        public bool Update(Maticsoft.Model.Members.PointsDetail model)
        {
            return this.dal.Update(model);
        }

        public bool UsePoints(int userid, int score, string desc, string extdata = "")
        {
            Maticsoft.Model.Members.PointsDetail model = new Maticsoft.Model.Members.PointsDetail {
                CreatedDate = DateTime.Now,
                Description = desc,
                ExtData = extdata,
                Score = score,
                RuleAction = "Use",
                Type = 1,
                UserID = userid
            };
            return this.dal.AddDetail(model);
        }
    }
}

