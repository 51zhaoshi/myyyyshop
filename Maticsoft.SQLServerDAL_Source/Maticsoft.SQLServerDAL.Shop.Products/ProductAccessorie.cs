namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ProductAccessorie : IProductAccessorie
    {
        public bool Add(Maticsoft.Model.Shop.Products.ProductAccessorie model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_ProductAccessories(");
            builder.Append("ProductId,AccessoriesValueId,AccessoriesName,MaxQuantity,MinQuantity,DiscountType,DiscountAmount)");
            builder.Append(" VALUES (");
            builder.Append("@ProductId,@AccessoriesValueId,@AccessoriesName,@MaxQuantity,@MinQuantity,@DiscountType,@DiscountAmount)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@AccessoriesValueId", SqlDbType.Int, 4), new SqlParameter("@AccessoriesName", SqlDbType.NVarChar, 100), new SqlParameter("@MaxQuantity", SqlDbType.Int, 4), new SqlParameter("@MinQuantity", SqlDbType.Int, 4), new SqlParameter("@DiscountType", SqlDbType.SmallInt, 2), new SqlParameter("@DiscountAmount", SqlDbType.Money, 8) };
            cmdParms[0].Value = model.ProductId;
            cmdParms[1].Value = model.AccessoriesValueId;
            cmdParms[2].Value = model.AccessoriesName;
            cmdParms[3].Value = model.MaxQuantity;
            cmdParms[4].Value = model.MinQuantity;
            cmdParms[5].Value = model.DiscountType;
            cmdParms[6].Value = model.DiscountAmount;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete(long ProductId, int AccessoriesValueId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_ProductAccessories ");
            builder.Append(" WHERE ProductId=@ProductId and AccessoriesValueId=@AccessoriesValueId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@AccessoriesValueId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ProductId;
            cmdParms[1].Value = AccessoriesValueId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(long ProductId, int AccessoriesValueId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ProductAccessories");
            builder.Append(" WHERE ProductId=@ProductId and AccessoriesValueId=@AccessoriesValueId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@AccessoriesValueId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ProductId;
            cmdParms[1].Value = AccessoriesValueId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ProductId,AccessoriesValueId,AccessoriesName,MaxQuantity,MinQuantity,DiscountType,DiscountAmount ");
            builder.Append(" FROM Shop_ProductAccessories ");
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
            builder.Append(" ProductId,AccessoriesValueId,AccessoriesName,MaxQuantity,MinQuantity,DiscountType,DiscountAmount ");
            builder.Append(" FROM Shop_ProductAccessories ");
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
            builder.Append(")AS Row, T.*  FROM Shop_ProductAccessories T ");
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
            return DbHelperSQL.GetMaxID("AccessoriesValueId", "Shop_ProductAccessories");
        }

        public Maticsoft.Model.Shop.Products.ProductAccessorie GetModel(long ProductId, int AccessoriesValueId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 ProductId,AccessoriesValueId,AccessoriesName,MaxQuantity,MinQuantity,DiscountType,DiscountAmount FROM Shop_ProductAccessories ");
            builder.Append(" WHERE ProductId=@ProductId and AccessoriesValueId=@AccessoriesValueId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@AccessoriesValueId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ProductId;
            cmdParms[1].Value = AccessoriesValueId;
            Maticsoft.Model.Shop.Products.ProductAccessorie accessorie = new Maticsoft.Model.Shop.Products.ProductAccessorie();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["ProductId"] != null) && (set.Tables[0].Rows[0]["ProductId"].ToString() != ""))
            {
                accessorie.ProductId = long.Parse(set.Tables[0].Rows[0]["ProductId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["AccessoriesValueId"] != null) && (set.Tables[0].Rows[0]["AccessoriesValueId"].ToString() != ""))
            {
                accessorie.AccessoriesValueId = int.Parse(set.Tables[0].Rows[0]["AccessoriesValueId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["AccessoriesName"] != null) && (set.Tables[0].Rows[0]["AccessoriesName"].ToString() != ""))
            {
                accessorie.AccessoriesName = set.Tables[0].Rows[0]["AccessoriesName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["MaxQuantity"] != null) && (set.Tables[0].Rows[0]["MaxQuantity"].ToString() != ""))
            {
                accessorie.MaxQuantity = new int?(int.Parse(set.Tables[0].Rows[0]["MaxQuantity"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["MinQuantity"] != null) && (set.Tables[0].Rows[0]["MinQuantity"].ToString() != ""))
            {
                accessorie.MinQuantity = new int?(int.Parse(set.Tables[0].Rows[0]["MinQuantity"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["DiscountType"] != null) && (set.Tables[0].Rows[0]["DiscountType"].ToString() != ""))
            {
                accessorie.DiscountType = new int?(int.Parse(set.Tables[0].Rows[0]["DiscountType"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["DiscountAmount"] != null) && (set.Tables[0].Rows[0]["DiscountAmount"].ToString() != ""))
            {
                accessorie.DiscountAmount = new decimal?(decimal.Parse(set.Tables[0].Rows[0]["DiscountAmount"].ToString()));
            }
            return accessorie;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ProductAccessories ");
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

        public bool Update(Maticsoft.Model.Shop.Products.ProductAccessorie model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_ProductAccessories SET ");
            builder.Append("AccessoriesName=@AccessoriesName,");
            builder.Append("MaxQuantity=@MaxQuantity,");
            builder.Append("MinQuantity=@MinQuantity,");
            builder.Append("DiscountType=@DiscountType,");
            builder.Append("DiscountAmount=@DiscountAmount");
            builder.Append(" WHERE ProductId=@ProductId and AccessoriesValueId=@AccessoriesValueId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AccessoriesName", SqlDbType.NVarChar, 100), new SqlParameter("@MaxQuantity", SqlDbType.Int, 4), new SqlParameter("@MinQuantity", SqlDbType.Int, 4), new SqlParameter("@DiscountType", SqlDbType.SmallInt, 2), new SqlParameter("@DiscountAmount", SqlDbType.Money, 8), new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@AccessoriesValueId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.AccessoriesName;
            cmdParms[1].Value = model.MaxQuantity;
            cmdParms[2].Value = model.MinQuantity;
            cmdParms[3].Value = model.DiscountType;
            cmdParms[4].Value = model.DiscountAmount;
            cmdParms[5].Value = model.ProductId;
            cmdParms[6].Value = model.AccessoriesValueId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

