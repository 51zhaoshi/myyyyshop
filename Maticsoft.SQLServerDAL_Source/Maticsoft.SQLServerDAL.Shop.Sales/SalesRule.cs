namespace Maticsoft.SQLServerDAL.Shop.Sales
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Sales;
    using Maticsoft.Model.Shop.Sales;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SalesRule : ISalesRule
    {
        public int Add(Maticsoft.Model.Shop.Sales.SalesRule model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_SalesRule(");
            builder.Append("RuleName,RuleMode,RuleUnit,Status,CreatedDate,CreatedUserID)");
            builder.Append(" values (");
            builder.Append("@RuleName,@RuleMode,@RuleUnit,@Status,@CreatedDate,@CreatedUserID)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleName", SqlDbType.NVarChar, 200), new SqlParameter("@RuleMode", SqlDbType.Int, 4), new SqlParameter("@RuleUnit", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.RuleName;
            cmdParms[1].Value = model.RuleMode;
            cmdParms[2].Value = model.RuleUnit;
            cmdParms[3].Value = model.Status;
            cmdParms[4].Value = model.CreatedDate;
            cmdParms[5].Value = model.CreatedUserID;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Sales.SalesRule DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Sales.SalesRule rule = new Maticsoft.Model.Shop.Sales.SalesRule();
            if (row != null)
            {
                if ((row["RuleId"] != null) && (row["RuleId"].ToString() != ""))
                {
                    rule.RuleId = int.Parse(row["RuleId"].ToString());
                }
                if (row["RuleName"] != null)
                {
                    rule.RuleName = row["RuleName"].ToString();
                }
                if ((row["RuleMode"] != null) && (row["RuleMode"].ToString() != ""))
                {
                    rule.RuleMode = int.Parse(row["RuleMode"].ToString());
                }
                if ((row["RuleUnit"] != null) && (row["RuleUnit"].ToString() != ""))
                {
                    rule.RuleUnit = int.Parse(row["RuleUnit"].ToString());
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    rule.Status = int.Parse(row["Status"].ToString());
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    rule.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["CreatedUserID"] != null) && (row["CreatedUserID"].ToString() != ""))
                {
                    rule.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
            }
            return rule;
        }

        public bool Delete(int RuleId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SalesRule ");
            builder.Append(" where RuleId=@RuleId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RuleId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteEx(int RuleId)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("delete Shop_SalesRule ");
            builder.Append(" where RuleId=@RuleId ");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4) };
            para[0].Value = RuleId;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("delete Shop_SalesItem ");
            builder2.Append(" where RuleId=@RuleId ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4) };
            parameterArray2[0].Value = RuleId;
            item = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(item);
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("delete Shop_SalesUserRank ");
            builder3.Append(" where  RuleId=@RuleId ");
            SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4) };
            parameterArray3[0].Value = RuleId;
            item = new CommandInfo(builder3.ToString(), parameterArray3);
            cmdList.Add(item);
            StringBuilder builder4 = new StringBuilder();
            builder4.Append("delete Shop_SalesRuleProduct ");
            builder4.Append(" where  RuleId=@RuleId ");
            SqlParameter[] parameterArray4 = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4) };
            parameterArray4[0].Value = RuleId;
            item = new CommandInfo(builder4.ToString(), parameterArray4);
            cmdList.Add(item);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }

        public bool DeleteList(string RuleIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SalesRule ");
            builder.Append(" where RuleId in (" + RuleIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool DeleteListEx(string RuleIdlist)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SalesRule ");
            builder.Append(" where RuleId in (" + RuleIdlist + ") ");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@RuleIdt", SqlDbType.Int, 4) };
            para[0].Value = 1;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("delete Shop_SalesItem ");
            builder2.Append(" where RuleId in (" + RuleIdlist + ") ");
            item = new CommandInfo(builder2.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("delete Shop_SalesUserRank ");
            builder3.Append(" where RuleId in (" + RuleIdlist + ") ");
            item = new CommandInfo(builder3.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder4 = new StringBuilder();
            builder4.Append("delete Shop_SalesRuleProduct ");
            builder4.Append(" where RuleId in (" + RuleIdlist + ") ");
            item = new CommandInfo(builder4.ToString(), para);
            cmdList.Add(item);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }

        public bool Exists(int RuleId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_SalesRule");
            builder.Append(" where RuleId=@RuleId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RuleId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RuleId,RuleName,RuleMode,RuleUnit,Status,CreatedDate,CreatedUserID ");
            builder.Append(" FROM Shop_SalesRule ");
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
            builder.Append(" RuleId,RuleName,RuleMode,RuleUnit,Status,CreatedDate,CreatedUserID ");
            builder.Append(" FROM Shop_SalesRule ");
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
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.RuleId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_SalesRule T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("RuleId", "Shop_SalesRule");
        }

        public Maticsoft.Model.Shop.Sales.SalesRule GetModel(int RuleId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 RuleId,RuleName,RuleMode,RuleUnit,Status,CreatedDate,CreatedUserID from Shop_SalesRule ");
            builder.Append(" where RuleId=@RuleId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RuleId;
            new Maticsoft.Model.Shop.Sales.SalesRule();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Shop_SalesRule ");
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

        public bool Update(Maticsoft.Model.Shop.Sales.SalesRule model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_SalesRule set ");
            builder.Append("RuleName=@RuleName,");
            builder.Append("RuleMode=@RuleMode,");
            builder.Append("RuleUnit=@RuleUnit,");
            builder.Append("Status=@Status,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("CreatedUserID=@CreatedUserID");
            builder.Append(" where RuleId=@RuleId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleName", SqlDbType.NVarChar, 200), new SqlParameter("@RuleMode", SqlDbType.Int, 4), new SqlParameter("@RuleUnit", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@RuleId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.RuleName;
            cmdParms[1].Value = model.RuleMode;
            cmdParms[2].Value = model.RuleUnit;
            cmdParms[3].Value = model.Status;
            cmdParms[4].Value = model.CreatedDate;
            cmdParms[5].Value = model.CreatedUserID;
            cmdParms[6].Value = model.RuleId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

