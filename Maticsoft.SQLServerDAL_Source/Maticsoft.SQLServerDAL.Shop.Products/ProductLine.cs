namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ProductLine : IProductLine
    {
        public int Add(Maticsoft.Model.Shop.Products.ProductLine model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_ProductLines(");
            builder.Append("LineName)");
            builder.Append(" VALUES (");
            builder.Append("@LineName)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LineName", SqlDbType.NVarChar, 60) };
            cmdParms[0].Value = model.LineName;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int LineId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_ProductLines ");
            builder.Append(" WHERE LineId=@LineId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LineId", SqlDbType.Int, 4) };
            cmdParms[0].Value = LineId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string LineIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_ProductLines ");
            builder.Append(" WHERE LineId in (" + LineIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int LineId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ProductLines");
            builder.Append(" WHERE LineId=@LineId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LineId", SqlDbType.Int, 4) };
            cmdParms[0].Value = LineId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT LineId,LineName ");
            builder.Append(" FROM Shop_ProductLines ");
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
            builder.Append(" LineId,LineName ");
            builder.Append(" FROM Shop_ProductLines ");
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
                builder.Append("ORDER BY T.LineId desc");
            }
            builder.Append(")AS Row, T.*  FROM Shop_ProductLines T ");
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
            return DbHelperSQL.GetMaxID("LineId", "Shop_ProductLines");
        }

        public Maticsoft.Model.Shop.Products.ProductLine GetModel(int LineId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 LineId,LineName FROM Shop_ProductLines ");
            builder.Append(" WHERE LineId=@LineId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LineId", SqlDbType.Int, 4) };
            cmdParms[0].Value = LineId;
            Maticsoft.Model.Shop.Products.ProductLine line = new Maticsoft.Model.Shop.Products.ProductLine();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["LineId"] != null) && (set.Tables[0].Rows[0]["LineId"].ToString() != ""))
            {
                line.LineId = int.Parse(set.Tables[0].Rows[0]["LineId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["LineName"] != null) && (set.Tables[0].Rows[0]["LineName"].ToString() != ""))
            {
                line.LineName = set.Tables[0].Rows[0]["LineName"].ToString();
            }
            return line;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ProductLines ");
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

        public bool Update(Maticsoft.Model.Shop.Products.ProductLine model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_ProductLines SET ");
            builder.Append("LineName=@LineName");
            builder.Append(" WHERE LineId=@LineId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LineName", SqlDbType.NVarChar, 60), new SqlParameter("@LineId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.LineName;
            cmdParms[1].Value = model.LineId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

