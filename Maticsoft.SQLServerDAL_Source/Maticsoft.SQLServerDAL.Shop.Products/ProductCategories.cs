namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ProductCategories : IProductCategories
    {
        public bool Add(Maticsoft.Model.Shop.Products.ProductCategories model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_ProductCategories(");
            builder.Append("CategoryId,ProductId,CategoryPath)");
            builder.Append(" VALUES (");
            builder.Append("@CategoryId,@ProductId,@CategoryPath)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@CategoryPath", SqlDbType.NVarChar) };
            cmdParms[0].Value = model.CategoryId;
            cmdParms[1].Value = model.ProductId;
            cmdParms[2].Value = model.CategoryPath;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete(long produtId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_ProductCategories ");
            builder.Append(" WHERE ProductId=@ProductId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt) };
            cmdParms[0].Value = produtId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT CategoryId,ProductId,CategoryPath ");
            builder.Append(" FROM Shop_ProductCategories ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
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
            builder.Append(" CategoryId,ProductId,CategoryPath ");
            builder.Append(" FROM Shop_ProductCategories ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
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
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("ORDER BY T." + orderby);
            }
            else
            {
                builder.Append("ORDER BY T.TagID desc");
            }
            builder.Append(")AS Row, T.*  FROM Shop_ProductCategories T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Shop.Products.ProductCategories GetModel(long produtId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 CategoryId,ProductId,CategoryPath FROM Shop_ProductCategories ");
            builder.Append(" WHERE ProductId=@ProductId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt) };
            cmdParms[0].Value = produtId;
            Maticsoft.Model.Shop.Products.ProductCategories categories = new Maticsoft.Model.Shop.Products.ProductCategories();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["CategoryId"] != null) && (set.Tables[0].Rows[0]["CategoryId"].ToString() != ""))
            {
                categories.CategoryId = int.Parse(set.Tables[0].Rows[0]["CategoryId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ProductId"] != null) && (set.Tables[0].Rows[0]["ProductId"].ToString() != ""))
            {
                categories.ProductId = long.Parse(set.Tables[0].Rows[0]["ProductId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["CategoryPath"] != null) && (set.Tables[0].Rows[0]["CategoryPath"].ToString() != ""))
            {
                categories.CategoryPath = set.Tables[0].Rows[0]["CategoryPath"].ToString();
            }
            return categories;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ProductCategories ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
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
    }
}

