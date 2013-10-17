namespace Maticsoft.SQLServerDAL.Members
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PointsRule : IPointsRule
    {
        public bool Add(Maticsoft.Model.Members.PointsRule model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Accounts_PointsRule(");
            builder.Append("RuleAction,PointsLimitID,Name,Score,Description)");
            builder.Append(" values (");
            builder.Append("@RuleAction,@PointsLimitID,@Name,@Score,@Description)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleAction", SqlDbType.NVarChar, 200), new SqlParameter("@PointsLimitID", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 200), new SqlParameter("@Score", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar) };
            cmdParms[0].Value = model.RuleAction;
            cmdParms[1].Value = model.PointsLimitID;
            cmdParms[2].Value = model.Name;
            cmdParms[3].Value = model.Score;
            cmdParms[4].Value = model.Description;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete(string RuleAction)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_PointsRule ");
            builder.Append(" where RuleAction=@RuleAction ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleAction", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = RuleAction;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string RuleActionlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_PointsRule ");
            builder.Append(" where RuleAction in (" + RuleActionlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string RuleAction)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Accounts_PointsRule");
            builder.Append(" where RuleAction=@RuleAction ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleAction", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = RuleAction;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RuleAction,PointsLimitID,Name,Score,Description ");
            builder.Append(" FROM Accounts_PointsRule ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" RuleAction,PointsLimitID,Name,Score,Description ");
            builder.Append(" FROM Accounts_PointsRule ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.RuleAction desc");
            }
            builder.Append(")AS Row, T.*  from Accounts_PointsRule T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Members.PointsRule GetModel(string RuleAction)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 RuleAction,PointsLimitID,Name,Score,Description from Accounts_PointsRule ");
            builder.Append(" where RuleAction=@RuleAction ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleAction", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = RuleAction;
            Maticsoft.Model.Members.PointsRule rule = new Maticsoft.Model.Members.PointsRule();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["RuleAction"] != null) && (set.Tables[0].Rows[0]["RuleAction"].ToString() != ""))
            {
                rule.RuleAction = set.Tables[0].Rows[0]["RuleAction"].ToString();
            }
            if ((set.Tables[0].Rows[0]["PointsLimitID"] != null) && (set.Tables[0].Rows[0]["PointsLimitID"].ToString() != ""))
            {
                rule.PointsLimitID = int.Parse(set.Tables[0].Rows[0]["PointsLimitID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Name"] != null) && (set.Tables[0].Rows[0]["Name"].ToString() != ""))
            {
                rule.Name = set.Tables[0].Rows[0]["Name"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Score"] != null) && (set.Tables[0].Rows[0]["Score"].ToString() != ""))
            {
                rule.Score = int.Parse(set.Tables[0].Rows[0]["Score"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Description"] != null) && (set.Tables[0].Rows[0]["Description"].ToString() != ""))
            {
                rule.Description = set.Tables[0].Rows[0]["Description"].ToString();
            }
            return rule;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Accounts_PointsRule ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public string GetRuleName(string ruleaction)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT Name  ");
            builder.Append("FROM Accounts_PointsRule ");
            builder.AppendFormat("WHERE RuleAction='{0}' ", ruleaction);
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single != null)
            {
                return single.ToString();
            }
            return null;
        }

        public bool Update(Maticsoft.Model.Members.PointsRule model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_PointsRule set ");
            builder.Append("PointsLimitID=@PointsLimitID,");
            builder.Append("Name=@Name,");
            builder.Append("Score=@Score,");
            builder.Append("Description=@Description");
            builder.Append(" where RuleAction=@RuleAction ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PointsLimitID", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 200), new SqlParameter("@Score", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@RuleAction", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = model.PointsLimitID;
            cmdParms[1].Value = model.Name;
            cmdParms[2].Value = model.Score;
            cmdParms[3].Value = model.Description;
            cmdParms[4].Value = model.RuleAction;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

