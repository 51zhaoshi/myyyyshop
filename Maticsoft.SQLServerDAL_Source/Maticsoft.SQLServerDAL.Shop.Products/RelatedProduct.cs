namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class RelatedProduct : IRelatedProduct
    {
        public bool Add(Maticsoft.Model.Shop.Products.RelatedProduct model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_RelatedProducts(");
            builder.Append("RelatedId,ProductId)");
            builder.Append(" VALUES (");
            builder.Append("@RelatedId,@ProductId)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RelatedId", SqlDbType.BigInt, 8), new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = model.RelatedId;
            cmdParms[1].Value = model.ProductId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete(int RelatedId, long ProductId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_RelatedProducts ");
            builder.Append(" WHERE RelatedId=@RelatedId and ProductId=@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RelatedId", SqlDbType.BigInt, 8), new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = RelatedId;
            cmdParms[1].Value = ProductId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(int RelatedId, long ProductId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_RelatedProducts");
            builder.Append(" WHERE RelatedId=@RelatedId and ProductId=@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RelatedId", SqlDbType.BigInt, 8), new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = RelatedId;
            cmdParms[1].Value = ProductId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT RelatedId,ProductId ");
            builder.Append(" FROM Shop_RelatedProducts ");
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
            builder.Append(" RelatedId,ProductId ");
            builder.Append(" FROM Shop_RelatedProducts ");
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
                builder.Append("ORDER BY T.ProductId desc");
            }
            builder.Append(")AS Row, T.*  FROM Shop_RelatedProducts T ");
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
            return DbHelperSQL.GetMaxID("RelatedId", "Shop_RelatedProducts");
        }

        public Maticsoft.Model.Shop.Products.RelatedProduct GetModel(int RelatedId, long ProductId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 RelatedId,ProductId FROM Shop_RelatedProducts ");
            builder.Append(" WHERE RelatedId=@RelatedId and ProductId=@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RelatedId", SqlDbType.BigInt, 8), new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = RelatedId;
            cmdParms[1].Value = ProductId;
            Maticsoft.Model.Shop.Products.RelatedProduct product = new Maticsoft.Model.Shop.Products.RelatedProduct();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["RelatedId"] != null) && (set.Tables[0].Rows[0]["RelatedId"].ToString() != ""))
            {
                product.RelatedId = int.Parse(set.Tables[0].Rows[0]["RelatedId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ProductId"] != null) && (set.Tables[0].Rows[0]["ProductId"].ToString() != ""))
            {
                product.ProductId = long.Parse(set.Tables[0].Rows[0]["ProductId"].ToString());
            }
            return product;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_RelatedProducts ");
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

        public DataSet IsDoubleRelated(long productId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT A.*,CASE  WHEN B.RelatedId IS NULL THEN 0 ELSE 1 END AS IsRelated   FROM( ");
            builder.Append("SELECT * FROM Shop_RelatedProducts P ");
            builder.AppendFormat("WHERE P.ProductId={0})A ", productId);
            builder.Append("LEFT JOIN (SELECT * FROM Shop_RelatedProducts RP ");
            builder.AppendFormat("WHERE RP.RelatedId={0})B ON  A.RelatedId = B.ProductId ", productId);
            return DbHelperSQL.Query(builder.ToString());
        }

        public bool Update(Maticsoft.Model.Shop.Products.RelatedProduct model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_RelatedProducts SET ");
            builder.Append("RelatedId=@RelatedId,");
            builder.Append("ProductId=@ProductId");
            builder.Append(" WHERE RelatedId=@RelatedId and ProductId=@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RelatedId", SqlDbType.BigInt, 8), new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = model.RelatedId;
            cmdParms[1].Value = model.ProductId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

