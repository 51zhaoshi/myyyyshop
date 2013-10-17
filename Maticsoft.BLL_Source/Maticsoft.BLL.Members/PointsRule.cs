namespace Maticsoft.BLL.Members
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class PointsRule
    {
        private readonly IPointsRule dal = DAMembers.CreatePointsRule();

        public bool Add(Maticsoft.Model.Members.PointsRule model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Members.PointsRule> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.PointsRule> list = new List<Maticsoft.Model.Members.PointsRule>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Members.PointsRule item = new Maticsoft.Model.Members.PointsRule();
                    if ((dt.Rows[i]["RuleAction"] != null) && (dt.Rows[i]["RuleAction"].ToString() != ""))
                    {
                        item.RuleAction = dt.Rows[i]["RuleAction"].ToString();
                    }
                    if ((dt.Rows[i]["PointsLimitID"] != null) && (dt.Rows[i]["PointsLimitID"].ToString() != ""))
                    {
                        item.PointsLimitID = int.Parse(dt.Rows[i]["PointsLimitID"].ToString());
                    }
                    if ((dt.Rows[i]["Name"] != null) && (dt.Rows[i]["Name"].ToString() != ""))
                    {
                        item.Name = dt.Rows[i]["Name"].ToString();
                    }
                    if ((dt.Rows[i]["Score"] != null) && (dt.Rows[i]["Score"].ToString() != ""))
                    {
                        item.Score = int.Parse(dt.Rows[i]["Score"].ToString());
                    }
                    if ((dt.Rows[i]["Description"] != null) && (dt.Rows[i]["Description"].ToString() != ""))
                    {
                        item.Description = dt.Rows[i]["Description"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(string RuleAction)
        {
            return this.dal.Delete(RuleAction);
        }

        public bool DeleteList(string RuleActionlist)
        {
            return this.dal.DeleteList(RuleActionlist);
        }

        public bool Exists(string RuleAction)
        {
            return this.dal.Exists(RuleAction);
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

        public DataSet GetListByKeyWord(string keyword)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                builder.AppendFormat("RuleAction like '%{0}%' or name like '%{0}%'", keyword);
                return this.dal.GetList(builder.ToString());
            }
            return this.GetAllList();
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public Maticsoft.Model.Members.PointsRule GetModel(string RuleAction)
        {
            return this.dal.GetModel(RuleAction);
        }

        public Maticsoft.Model.Members.PointsRule GetModelByCache(string RuleAction)
        {
            string cacheKey = "PointsRuleModel-" + RuleAction;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(RuleAction);
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
            return (Maticsoft.Model.Members.PointsRule) cache;
        }

        public List<Maticsoft.Model.Members.PointsRule> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public string GetRuleName(string ruleaction)
        {
            return this.dal.GetRuleName(ruleaction);
        }

        public bool Update(Maticsoft.Model.Members.PointsRule model)
        {
            return this.dal.Update(model);
        }
    }
}

