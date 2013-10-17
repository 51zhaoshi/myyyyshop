namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ProductAttribute : IProductAttribute
    {
        public bool Add(Maticsoft.Model.Shop.Products.ProductAttribute model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_ProductAttributes(");
            builder.Append("ProductId,AttributeId,ValueId)");
            builder.Append(" VALUES (");
            builder.Append("@ProductId,@AttributeId,@ValueId)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@AttributeId", SqlDbType.BigInt, 8), new SqlParameter("@ValueId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ProductId;
            cmdParms[1].Value = model.AttributeId;
            cmdParms[2].Value = model.ValueId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete(long ProductId, long AttributeId, int ValueId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_ProductAttributes ");
            builder.Append(" WHERE ProductId=@ProductId and AttributeId=@AttributeId and ValueId=@ValueId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@AttributeId", SqlDbType.BigInt, 8), new SqlParameter("@ValueId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ProductId;
            cmdParms[1].Value = AttributeId;
            cmdParms[2].Value = ValueId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(long ProductId, long AttributeId, int ValueId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ProductAttributes");
            builder.Append(" WHERE ProductId=@ProductId and AttributeId=@AttributeId and ValueId=@ValueId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@AttributeId", SqlDbType.BigInt, 8), new SqlParameter("@ValueId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ProductId;
            cmdParms[1].Value = AttributeId;
            cmdParms[2].Value = ValueId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(long? ProductId, long? AttributeId, long? ValueId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ProductAttributes");
            builder.Append(" WHERE 1=1 ");
            if (ProductId.HasValue)
            {
                builder.Append(" AND ProductId=" + ProductId.Value);
            }
            if (AttributeId.HasValue)
            {
                builder.Append(" AND AttributeId=" + AttributeId.Value);
            }
            if (ValueId.HasValue)
            {
                builder.Append(" AND ValueId=" + ValueId.Value);
            }
            return DbHelperSQL.Exists(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ProductId,AttributeId,ValueId ");
            builder.Append(" FROM Shop_ProductAttributes ");
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
            builder.Append(" ProductId,AttributeId,ValueId ");
            builder.Append(" FROM Shop_ProductAttributes ");
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
                builder.Append("ORDER BY T.ValueId desc");
            }
            builder.Append(")AS Row, T.*  FROM Shop_ProductAttributes T ");
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
            return DbHelperSQL.GetMaxID("ValueId", "Shop_ProductAttributes");
        }

        public Maticsoft.Model.Shop.Products.ProductAttribute GetModel(long ProductId, long AttributeId, int ValueId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 ProductId,AttributeId,ValueId FROM Shop_ProductAttributes ");
            builder.Append(" WHERE ProductId=@ProductId and AttributeId=@AttributeId and ValueId=@ValueId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@AttributeId", SqlDbType.BigInt, 8), new SqlParameter("@ValueId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ProductId;
            cmdParms[1].Value = AttributeId;
            cmdParms[2].Value = ValueId;
            Maticsoft.Model.Shop.Products.ProductAttribute attribute = new Maticsoft.Model.Shop.Products.ProductAttribute();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["ProductId"] != null) && (set.Tables[0].Rows[0]["ProductId"].ToString() != ""))
            {
                attribute.ProductId = long.Parse(set.Tables[0].Rows[0]["ProductId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["AttributeId"] != null) && (set.Tables[0].Rows[0]["AttributeId"].ToString() != ""))
            {
                attribute.AttributeId = long.Parse(set.Tables[0].Rows[0]["AttributeId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ValueId"] != null) && (set.Tables[0].Rows[0]["ValueId"].ToString() != ""))
            {
                attribute.ValueId = int.Parse(set.Tables[0].Rows[0]["ValueId"].ToString());
            }
            return attribute;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ProductAttributes ");
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

        public bool Update(Maticsoft.Model.Shop.Products.ProductAttribute model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_ProductAttributes SET ");
            builder.Append("ProductId=@ProductId,");
            builder.Append("AttributeId=@AttributeId,");
            builder.Append("ValueId=@ValueId");
            builder.Append(" WHERE ProductId=@ProductId and AttributeId=@AttributeId and ValueId=@ValueId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@AttributeId", SqlDbType.BigInt, 8), new SqlParameter("@ValueId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ProductId;
            cmdParms[1].Value = model.AttributeId;
            cmdParms[2].Value = model.ValueId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

