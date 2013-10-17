namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Distributor : IDistributor
    {
        public int Add(Maticsoft.Model.Shop.Products.Distributor model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_Distributors(");
            builder.Append("DistributorName)");
            builder.Append(" VALUES (");
            builder.Append("@DistributorName)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DistributorName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = model.DistributorName;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int DistributorId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_Distributors ");
            builder.Append(" WHERE DistributorId=@DistributorId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DistributorId", SqlDbType.Int, 4) };
            cmdParms[0].Value = DistributorId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string DistributorIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_Distributors ");
            builder.Append(" WHERE DistributorId in (" + DistributorIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int DistributorId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_Distributors");
            builder.Append(" WHERE DistributorId=@DistributorId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DistributorId", SqlDbType.Int, 4) };
            cmdParms[0].Value = DistributorId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT DistributorId,DistributorName ");
            builder.Append(" FROM Shop_Distributors ");
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
            builder.Append(" DistributorId,DistributorName ");
            builder.Append(" FROM Shop_Distributors ");
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
            builder.Append(")AS Row, T.*  FROM Shop_Distributors T ");
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
            return DbHelperSQL.GetMaxID("DistributorId", "Shop_Distributors");
        }

        public Maticsoft.Model.Shop.Products.Distributor GetModel(int DistributorId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 DistributorId,DistributorName FROM Shop_Distributors ");
            builder.Append(" WHERE DistributorId=@DistributorId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DistributorId", SqlDbType.Int, 4) };
            cmdParms[0].Value = DistributorId;
            Maticsoft.Model.Shop.Products.Distributor distributor = new Maticsoft.Model.Shop.Products.Distributor();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["DistributorId"] != null) && (set.Tables[0].Rows[0]["DistributorId"].ToString() != ""))
            {
                distributor.DistributorId = int.Parse(set.Tables[0].Rows[0]["DistributorId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["DistributorName"] != null) && (set.Tables[0].Rows[0]["DistributorName"].ToString() != ""))
            {
                distributor.DistributorName = set.Tables[0].Rows[0]["DistributorName"].ToString();
            }
            return distributor;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_Distributors ");
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

        public bool Update(Maticsoft.Model.Shop.Products.Distributor model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_Distributors SET ");
            builder.Append("DistributorName=@DistributorName");
            builder.Append(" WHERE DistributorId=@DistributorId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@DistributorName", SqlDbType.NVarChar, 50), new SqlParameter("@DistributorId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.DistributorName;
            cmdParms[1].Value = model.DistributorId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

