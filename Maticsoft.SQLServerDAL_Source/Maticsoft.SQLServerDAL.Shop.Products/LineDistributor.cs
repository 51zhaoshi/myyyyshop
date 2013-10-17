namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class LineDistributor : ILineDistributor
    {
        public bool Add(Maticsoft.Model.Shop.Products.LineDistributor model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_LineDistributors(");
            builder.Append("LineId,DistributorId)");
            builder.Append(" VALUES (");
            builder.Append("@LineId,@DistributorId)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LineId", SqlDbType.Int, 4), new SqlParameter("@DistributorId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.LineId;
            cmdParms[1].Value = model.DistributorId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete(int LineId, int DistributorId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_LineDistributors ");
            builder.Append(" WHERE LineId=@LineId and DistributorId=@DistributorId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LineId", SqlDbType.Int, 4), new SqlParameter("@DistributorId", SqlDbType.Int, 4) };
            cmdParms[0].Value = LineId;
            cmdParms[1].Value = DistributorId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(int LineId, int DistributorId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_LineDistributors");
            builder.Append(" WHERE LineId=@LineId and DistributorId=@DistributorId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LineId", SqlDbType.Int, 4), new SqlParameter("@DistributorId", SqlDbType.Int, 4) };
            cmdParms[0].Value = LineId;
            cmdParms[1].Value = DistributorId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT LineId,DistributorId ");
            builder.Append(" FROM Shop_LineDistributors ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ");
            if (Top > 0)
            {
                builder.Append(" TOP " + Top.ToString());
            }
            builder.Append(" LineId,DistributorId ");
            builder.Append(" FROM Shop_LineDistributors ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ORDER BY " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                builder.Append("ORDER BY T." + orderby);
            }
            else
            {
                builder.Append("ORDER BY T.DistributorId desc");
            }
            builder.Append(")AS Row, T.*  FROM Shop_LineDistributors T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("LineId", "Shop_LineDistributors");
        }

        public Maticsoft.Model.Shop.Products.LineDistributor GetModel(int LineId, int DistributorId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 LineId,DistributorId FROM Shop_LineDistributors ");
            builder.Append(" WHERE LineId=@LineId and DistributorId=@DistributorId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LineId", SqlDbType.Int, 4), new SqlParameter("@DistributorId", SqlDbType.Int, 4) };
            cmdParms[0].Value = LineId;
            cmdParms[1].Value = DistributorId;
            Maticsoft.Model.Shop.Products.LineDistributor distributor = new Maticsoft.Model.Shop.Products.LineDistributor();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["LineId"] != null) && (set.Tables[0].Rows[0]["LineId"].ToString() != ""))
            {
                distributor.LineId = int.Parse(set.Tables[0].Rows[0]["LineId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["DistributorId"] != null) && (set.Tables[0].Rows[0]["DistributorId"].ToString() != ""))
            {
                distributor.DistributorId = int.Parse(set.Tables[0].Rows[0]["DistributorId"].ToString());
            }
            return distributor;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_LineDistributors ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Maticsoft.Model.Shop.Products.LineDistributor model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_LineDistributors SET ");
            builder.Append("LineId=@LineId,");
            builder.Append("DistributorId=@DistributorId");
            builder.Append(" WHERE LineId=@LineId and DistributorId=@DistributorId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LineId", SqlDbType.Int, 4), new SqlParameter("@DistributorId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.LineId;
            cmdParms[1].Value = model.DistributorId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

