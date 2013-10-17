namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ProductImage : IProductImage
    {
        public int Add(Maticsoft.Model.Shop.Products.ProductImage model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_ProductImages(");
            builder.Append("ProductId,ImageUrl,ThumbnailUrl1,ThumbnailUrl2,ThumbnailUrl3,ThumbnailUrl4,ThumbnailUrl5,ThumbnailUrl6,ThumbnailUrl7,ThumbnailUrl8)");
            builder.Append(" VALUES (");
            builder.Append("@ProductId,@ImageUrl,@ThumbnailUrl1,@ThumbnailUrl2,@ThumbnailUrl3,@ThumbnailUrl4,@ThumbnailUrl5,@ThumbnailUrl6,@ThumbnailUrl7,@ThumbnailUrl8)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl1", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl2", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl3", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl4", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl5", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl6", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl7", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl8", SqlDbType.NVarChar, 0xff) };
            cmdParms[0].Value = model.ProductId;
            cmdParms[1].Value = model.ImageUrl;
            cmdParms[2].Value = model.ThumbnailUrl1;
            cmdParms[3].Value = model.ThumbnailUrl2;
            cmdParms[4].Value = model.ThumbnailUrl3;
            cmdParms[5].Value = model.ThumbnailUrl4;
            cmdParms[6].Value = model.ThumbnailUrl5;
            cmdParms[7].Value = model.ThumbnailUrl6;
            cmdParms[8].Value = model.ThumbnailUrl7;
            cmdParms[9].Value = model.ThumbnailUrl8;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int ProductImageId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_ProductImages ");
            builder.Append(" WHERE ProductImageId=@ProductImageId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductImageId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ProductImageId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete(long ProductId, int ProductImageId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_ProductImages ");
            builder.Append(" WHERE ProductId=@ProductId and ProductImageId=@ProductImageId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@ProductImageId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ProductId;
            cmdParms[1].Value = ProductImageId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ProductImageIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_ProductImages ");
            builder.Append(" WHERE ProductImageId in (" + ProductImageIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(long ProductId, int ProductImageId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ProductImages");
            builder.Append(" WHERE ProductId=@ProductId and ProductImageId=@ProductImageId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@ProductImageId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ProductId;
            cmdParms[1].Value = ProductImageId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ProductImageId,ProductId,ImageUrl,ThumbnailUrl1,ThumbnailUrl2,ThumbnailUrl3,ThumbnailUrl4,ThumbnailUrl5,ThumbnailUrl6,ThumbnailUrl7,ThumbnailUrl8 ");
            builder.Append(" FROM Shop_ProductImages ");
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
            builder.Append(" ProductImageId,ProductId,ImageUrl,ThumbnailUrl1,ThumbnailUrl2,ThumbnailUrl3,ThumbnailUrl4,ThumbnailUrl5,ThumbnailUrl6,ThumbnailUrl7,ThumbnailUrl8 ");
            builder.Append(" FROM Shop_ProductImages ");
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
                builder.Append("ORDER BY T.ProductImageId desc");
            }
            builder.Append(")AS Row, T.*  FROM Shop_ProductImages T ");
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
            return DbHelperSQL.GetMaxID("ProductImageId", "Shop_ProductImages");
        }

        public Maticsoft.Model.Shop.Products.ProductImage GetModel(int ProductImageId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 ProductImageId,ProductId,ImageUrl,ThumbnailUrl1,ThumbnailUrl2,ThumbnailUrl3,ThumbnailUrl4,ThumbnailUrl5,ThumbnailUrl6,ThumbnailUrl7,ThumbnailUrl8 FROM Shop_ProductImages ");
            builder.Append(" WHERE ProductImageId=@ProductImageId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductImageId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ProductImageId;
            Maticsoft.Model.Shop.Products.ProductImage image = new Maticsoft.Model.Shop.Products.ProductImage();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["ProductImageId"] != null) && (set.Tables[0].Rows[0]["ProductImageId"].ToString() != ""))
            {
                image.ProductImageId = int.Parse(set.Tables[0].Rows[0]["ProductImageId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ProductId"] != null) && (set.Tables[0].Rows[0]["ProductId"].ToString() != ""))
            {
                image.ProductId = long.Parse(set.Tables[0].Rows[0]["ProductId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ImageUrl"] != null) && (set.Tables[0].Rows[0]["ImageUrl"].ToString() != ""))
            {
                image.ImageUrl = set.Tables[0].Rows[0]["ImageUrl"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ThumbnailUrl1"] != null) && (set.Tables[0].Rows[0]["ThumbnailUrl1"].ToString() != ""))
            {
                image.ThumbnailUrl1 = set.Tables[0].Rows[0]["ThumbnailUrl1"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ThumbnailUrl2"] != null) && (set.Tables[0].Rows[0]["ThumbnailUrl2"].ToString() != ""))
            {
                image.ThumbnailUrl2 = set.Tables[0].Rows[0]["ThumbnailUrl2"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ThumbnailUrl3"] != null) && (set.Tables[0].Rows[0]["ThumbnailUrl3"].ToString() != ""))
            {
                image.ThumbnailUrl3 = set.Tables[0].Rows[0]["ThumbnailUrl3"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ThumbnailUrl4"] != null) && (set.Tables[0].Rows[0]["ThumbnailUrl4"].ToString() != ""))
            {
                image.ThumbnailUrl4 = set.Tables[0].Rows[0]["ThumbnailUrl4"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ThumbnailUrl5"] != null) && (set.Tables[0].Rows[0]["ThumbnailUrl5"].ToString() != ""))
            {
                image.ThumbnailUrl5 = set.Tables[0].Rows[0]["ThumbnailUrl5"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ThumbnailUrl6"] != null) && (set.Tables[0].Rows[0]["ThumbnailUrl6"].ToString() != ""))
            {
                image.ThumbnailUrl6 = set.Tables[0].Rows[0]["ThumbnailUrl6"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ThumbnailUrl7"] != null) && (set.Tables[0].Rows[0]["ThumbnailUrl7"].ToString() != ""))
            {
                image.ThumbnailUrl7 = set.Tables[0].Rows[0]["ThumbnailUrl7"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ThumbnailUrl8"] != null) && (set.Tables[0].Rows[0]["ThumbnailUrl8"].ToString() != ""))
            {
                image.ThumbnailUrl8 = set.Tables[0].Rows[0]["ThumbnailUrl8"].ToString();
            }
            return image;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ProductImages ");
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

        public DataSet ProductImagesList(long productId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append("SELECT ProductId,ImageUrl,ThumbnailUrl1,ThumbnailUrl2,ThumbnailUrl3 FROM Shop_Products P ");
            builder.Append("UNION ALL  ");
            builder.Append("SELECT ProductId,ImageUrl,ThumbnailUrl1,ThumbnailUrl2,ThumbnailUrl3 FROM Shop_ProductImages PIM)A ");
            builder.Append("WHERE A.ProductId=@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = productId;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public bool Update(Maticsoft.Model.Shop.Products.ProductImage model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_ProductImages SET ");
            builder.Append("ImageUrl=@ImageUrl,");
            builder.Append("ThumbnailUrl1=@ThumbnailUrl1,");
            builder.Append("ThumbnailUrl2=@ThumbnailUrl2,");
            builder.Append("ThumbnailUrl3=@ThumbnailUrl3,");
            builder.Append("ThumbnailUrl4=@ThumbnailUrl4,");
            builder.Append("ThumbnailUrl5=@ThumbnailUrl5,");
            builder.Append("ThumbnailUrl6=@ThumbnailUrl6,");
            builder.Append("ThumbnailUrl7=@ThumbnailUrl7,");
            builder.Append("ThumbnailUrl8=@ThumbnailUrl8");
            builder.Append(" WHERE ProductImageId=@ProductImageId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl1", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl2", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl3", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl4", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl5", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl6", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl7", SqlDbType.NVarChar, 0xff), new SqlParameter("@ThumbnailUrl8", SqlDbType.NVarChar, 0xff), new SqlParameter("@ProductImageId", SqlDbType.Int, 4), new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = model.ImageUrl;
            cmdParms[1].Value = model.ThumbnailUrl1;
            cmdParms[2].Value = model.ThumbnailUrl2;
            cmdParms[3].Value = model.ThumbnailUrl3;
            cmdParms[4].Value = model.ThumbnailUrl4;
            cmdParms[5].Value = model.ThumbnailUrl5;
            cmdParms[6].Value = model.ThumbnailUrl6;
            cmdParms[7].Value = model.ThumbnailUrl7;
            cmdParms[8].Value = model.ThumbnailUrl8;
            cmdParms[9].Value = model.ProductImageId;
            cmdParms[10].Value = model.ProductId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

