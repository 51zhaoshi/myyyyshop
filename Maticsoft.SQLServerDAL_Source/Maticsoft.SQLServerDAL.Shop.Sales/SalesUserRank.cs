namespace Maticsoft.SQLServerDAL.Shop.Sales
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Sales;
    using Maticsoft.Model.Shop.Sales;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SalesUserRank : ISalesUserRank
    {
        public bool Add(Maticsoft.Model.Shop.Sales.SalesUserRank model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_SalesUserRank(");
            builder.Append("RuleId,RankId,Remark)");
            builder.Append(" values (");
            builder.Append("@RuleId,@RankId,@Remark)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4), new SqlParameter("@RankId", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 500) };
            cmdParms[0].Value = model.RuleId;
            cmdParms[1].Value = model.RankId;
            cmdParms[2].Value = model.Remark;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Maticsoft.Model.Shop.Sales.SalesUserRank DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Sales.SalesUserRank rank = new Maticsoft.Model.Shop.Sales.SalesUserRank();
            if (row != null)
            {
                if ((row["RuleId"] != null) && (row["RuleId"].ToString() != ""))
                {
                    rank.RuleId = int.Parse(row["RuleId"].ToString());
                }
                if ((row["RankId"] != null) && (row["RankId"].ToString() != ""))
                {
                    rank.RankId = int.Parse(row["RankId"].ToString());
                }
                if (row["Remark"] != null)
                {
                    rank.Remark = row["Remark"].ToString();
                }
            }
            return rank;
        }

        public bool Delete(int RuleId, int RankId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SalesUserRank ");
            builder.Append(" where RuleId=@RuleId and RankId=@RankId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4), new SqlParameter("@RankId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RuleId;
            cmdParms[1].Value = RankId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteByRuleId(int ruleId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SalesUserRank ");
            builder.Append(" where RuleId=@RuleId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ruleId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(int RuleId, int RankId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_SalesUserRank");
            builder.Append(" where RuleId=@RuleId and RankId=@RankId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4), new SqlParameter("@RankId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RuleId;
            cmdParms[1].Value = RankId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RuleId,RankId,Remark ");
            builder.Append(" FROM Shop_SalesUserRank ");
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
            builder.Append(" RuleId,RankId,Remark ");
            builder.Append(" FROM Shop_SalesUserRank ");
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
                builder.Append("order by T.RankId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_SalesUserRank T ");
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
            return DbHelperSQL.GetMaxID("RuleId", "Shop_SalesUserRank");
        }

        public Maticsoft.Model.Shop.Sales.SalesUserRank GetModel(int RuleId, int RankId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 RuleId,RankId,Remark from Shop_SalesUserRank ");
            builder.Append(" where RuleId=@RuleId and RankId=@RankId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4), new SqlParameter("@RankId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RuleId;
            cmdParms[1].Value = RankId;
            new Maticsoft.Model.Shop.Sales.SalesUserRank();
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
            builder.Append("select count(1) FROM Shop_SalesUserRank ");
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

        public bool Update(Maticsoft.Model.Shop.Sales.SalesUserRank model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_SalesUserRank set ");
            builder.Append("Remark=@Remark");
            builder.Append(" where RuleId=@RuleId and RankId=@RankId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Remark", SqlDbType.NVarChar, 500), new SqlParameter("@RuleId", SqlDbType.Int, 4), new SqlParameter("@RankId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Remark;
            cmdParms[1].Value = model.RuleId;
            cmdParms[2].Value = model.RankId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

