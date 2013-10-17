namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ProductTypeBrand : IProductTypeBrand
    {
        public bool Add(Maticsoft.Model.Shop.Products.ProductTypeBrand model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_ProductTypeBrands(");
            builder.Append("ProductTypeId,BrandId)");
            builder.Append(" VALUES (");
            builder.Append("@ProductTypeId,@BrandId)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductTypeId", SqlDbType.Int, 4), new SqlParameter("@BrandId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ProductTypeId;
            cmdParms[1].Value = model.BrandId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Add(int ProductTypeId, int BrandsId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_ProductTypeBrands(");
            builder.Append("ProductTypeId,BrandId)");
            builder.Append(" VALUES (");
            builder.Append("@ProductTypeId,@BrandId)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductTypeId", SqlDbType.Int, 4), new SqlParameter("@BrandId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ProductTypeId;
            cmdParms[1].Value = BrandsId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete(int? ProductTypeId, int? BrandId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_ProductTypeBrands ");
            if (ProductTypeId.HasValue)
            {
                builder.AppendFormat(" WHERE ProductTypeId={0}", ProductTypeId.Value);
            }
            else if (BrandId.HasValue)
            {
                builder.AppendFormat(" WHERE BrandId={0} ", BrandId.Value);
            }
            else
            {
                builder.AppendFormat(" WHERE ProductTypeId={0} AND BrandId={1} ", ProductTypeId.Value, BrandId.Value);
            }
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ProductTypeId, int BrandId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ProductTypeBrands");
            builder.Append(" WHERE ProductTypeId=@ProductTypeId and BrandId=@BrandId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductTypeId", SqlDbType.Int, 4), new SqlParameter("@BrandId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ProductTypeId;
            cmdParms[1].Value = BrandId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool ExistsBrands(int BrandId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ProductTypeBrands");
            builder.Append(" WHERE  BrandId=@BrandId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BrandId", SqlDbType.Int, 4) };
            cmdParms[0].Value = BrandId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ProductTypeId,BrandId ");
            builder.Append(" FROM Shop_ProductTypeBrands ");
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
            builder.Append(" ProductTypeId,BrandId ");
            builder.Append(" FROM Shop_ProductTypeBrands ");
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
                builder.Append("ORDER BY T.BrandId desc");
            }
            builder.Append(")AS Row, T.*  FROM Shop_ProductTypeBrands T ");
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
            return DbHelperSQL.GetMaxID("ProductTypeId", "Shop_ProductTypeBrands");
        }

        public Maticsoft.Model.Shop.Products.ProductTypeBrand GetModel(int ProductTypeId, int BrandId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 ProductTypeId,BrandId FROM Shop_ProductTypeBrands ");
            builder.Append(" WHERE ProductTypeId=@ProductTypeId and BrandId=@BrandId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductTypeId", SqlDbType.Int, 4), new SqlParameter("@BrandId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ProductTypeId;
            cmdParms[1].Value = BrandId;
            Maticsoft.Model.Shop.Products.ProductTypeBrand brand = new Maticsoft.Model.Shop.Products.ProductTypeBrand();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["ProductTypeId"] != null) && (set.Tables[0].Rows[0]["ProductTypeId"].ToString() != ""))
            {
                brand.ProductTypeId = int.Parse(set.Tables[0].Rows[0]["ProductTypeId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["BrandId"] != null) && (set.Tables[0].Rows[0]["BrandId"].ToString() != ""))
            {
                brand.BrandId = int.Parse(set.Tables[0].Rows[0]["BrandId"].ToString());
            }
            return brand;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ProductTypeBrands ");
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

        public bool Update(Maticsoft.Model.Shop.Products.ProductTypeBrand model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_ProductTypeBrands SET ");
            builder.Append("ProductTypeId=@ProductTypeId");
            builder.Append(" WHERE BrandId=@BrandId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductTypeId", SqlDbType.Int, 4), new SqlParameter("@BrandId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ProductTypeId;
            cmdParms[1].Value = model.BrandId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

