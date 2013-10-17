namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class AccessoriesValue : IAccessoriesValue
    {
        public DataSet AccessoriesByProductId(long productId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Shop_AccessoriesValues ");
            builder.Append("WHERE AccessoriesValueId IN( ");
            builder.Append("SELECT  AccessoriesValueId FROM  ");
            builder.Append("Shop_ProductAccessories ");
            builder.Append("WHERE ProductId=@ProductId) ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = productId;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public int Add(Maticsoft.Model.Shop.Products.AccessoriesValue model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_AccessoriesValues(");
            builder.Append("ProductAccessoriesId,ProductAccessoriesSKU)");
            builder.Append(" VALUES (");
            builder.Append("@ProductAccessoriesId,@ProductAccessoriesSKU)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductAccessoriesId", SqlDbType.Int, 4), new SqlParameter("@ProductAccessoriesSKU", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = model.ProductAccessoriesId;
            cmdParms[1].Value = model.ProductAccessoriesSKU;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int AccessoriesValueId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_AccessoriesValues ");
            builder.Append(" WHERE AccessoriesValueId=@AccessoriesValueId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AccessoriesValueId", SqlDbType.Int, 4) };
            cmdParms[0].Value = AccessoriesValueId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string AccessoriesValueIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_AccessoriesValues ");
            builder.Append(" WHERE AccessoriesValueId in (" + AccessoriesValueIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int AccessoriesValueId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_AccessoriesValues");
            builder.Append(" WHERE AccessoriesValueId=@AccessoriesValueId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AccessoriesValueId", SqlDbType.Int, 4) };
            cmdParms[0].Value = AccessoriesValueId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT AccessoriesValueId,ProductAccessoriesId,ProductAccessoriesSKU ");
            builder.Append(" FROM Shop_AccessoriesValues ");
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
            builder.Append(" AccessoriesValueId,ProductAccessoriesId,ProductAccessoriesSKU ");
            builder.Append(" FROM Shop_AccessoriesValues ");
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
                builder.Append("ORDER BY T.AccessoriesValueId desc");
            }
            builder.Append(")AS Row, T.*  FROM Shop_AccessoriesValues T ");
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
            return DbHelperSQL.GetMaxID("AccessoriesValueId", "Shop_AccessoriesValues");
        }

        public Maticsoft.Model.Shop.Products.AccessoriesValue GetModel(int AccessoriesValueId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 AccessoriesValueId,ProductAccessoriesId,ProductAccessoriesSKU FROM Shop_AccessoriesValues ");
            builder.Append(" WHERE AccessoriesValueId=@AccessoriesValueId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AccessoriesValueId", SqlDbType.Int, 4) };
            cmdParms[0].Value = AccessoriesValueId;
            Maticsoft.Model.Shop.Products.AccessoriesValue value2 = new Maticsoft.Model.Shop.Products.AccessoriesValue();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["AccessoriesValueId"] != null) && (set.Tables[0].Rows[0]["AccessoriesValueId"].ToString() != ""))
            {
                value2.AccessoriesValueId = int.Parse(set.Tables[0].Rows[0]["AccessoriesValueId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ProductAccessoriesId"] != null) && (set.Tables[0].Rows[0]["ProductAccessoriesId"].ToString() != ""))
            {
                value2.ProductAccessoriesId = int.Parse(set.Tables[0].Rows[0]["ProductAccessoriesId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ProductAccessoriesSKU"] != null) && (set.Tables[0].Rows[0]["ProductAccessoriesSKU"].ToString() != ""))
            {
                value2.ProductAccessoriesSKU = set.Tables[0].Rows[0]["ProductAccessoriesSKU"].ToString();
            }
            return value2;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_AccessoriesValues ");
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

        public bool Update(Maticsoft.Model.Shop.Products.AccessoriesValue model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_AccessoriesValues SET ");
            builder.Append("ProductAccessoriesId=@ProductAccessoriesId,");
            builder.Append("ProductAccessoriesSKU=@ProductAccessoriesSKU");
            builder.Append(" WHERE AccessoriesValueId=@AccessoriesValueId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductAccessoriesId", SqlDbType.Int, 4), new SqlParameter("@ProductAccessoriesSKU", SqlDbType.NVarChar, 50), new SqlParameter("@AccessoriesValueId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ProductAccessoriesId;
            cmdParms[1].Value = model.ProductAccessoriesSKU;
            cmdParms[2].Value = model.AccessoriesValueId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

