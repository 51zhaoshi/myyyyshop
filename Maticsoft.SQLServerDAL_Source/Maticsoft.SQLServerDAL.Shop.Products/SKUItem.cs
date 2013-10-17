namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SKUItem : ISKUItem
    {
        public bool Add(Maticsoft.Model.Shop.Products.SKUItem model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_SKUItems(");
            builder.Append("SkuId,AttributeId,ValueId)");
            builder.Append(" VALUES (");
            builder.Append("@SkuId,@AttributeId,@ValueId)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SkuId", SqlDbType.BigInt, 8), new SqlParameter("@AttributeId", SqlDbType.BigInt, 8), new SqlParameter("@ValueId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = model.SkuId;
            cmdParms[1].Value = model.AttributeId;
            cmdParms[2].Value = model.ValueId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet AttributeValuesInfo(long productId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT A.*,B.UserDefinedPic  ");
            builder.Append("FROM Shop_SKUItems A ");
            builder.Append("LEFT JOIN Shop_Attributes  B ON A.AttributeId = B.AttributeId ");
            builder.Append("WHERE ProductId=@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = productId;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public bool Delete(long SkuId, long AttributeId, long ValueId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_SKUItems ");
            builder.Append(" WHERE SkuId=@SkuId and AttributeId=@AttributeId and ValueId=@ValueId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SkuId", SqlDbType.BigInt, 8), new SqlParameter("@AttributeId", SqlDbType.BigInt, 8), new SqlParameter("@ValueId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = SkuId;
            cmdParms[1].Value = AttributeId;
            cmdParms[2].Value = ValueId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(long SkuId, long AttributeId, long ValueId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_SKUItems");
            builder.Append(" WHERE SkuId=@SkuId and AttributeId=@AttributeId and ValueId=@ValueId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SkuId", SqlDbType.BigInt, 8), new SqlParameter("@AttributeId", SqlDbType.BigInt, 8), new SqlParameter("@ValueId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = SkuId;
            cmdParms[1].Value = AttributeId;
            cmdParms[2].Value = ValueId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(long? SkuId, long? AttributeId, long? ValueId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_SKUItems");
            builder.Append(" WHERE 1=1 ");
            if (SkuId.HasValue)
            {
                builder.Append(" AND SkuId=" + SkuId.Value);
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
            builder.Append("SELECT SkuId,AttributeId,ValueId ");
            builder.Append(" FROM Shop_SKUItems ");
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
            builder.Append(" SkuId,AttributeId,ValueId ");
            builder.Append(" FROM Shop_SKUItems ");
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
            builder.Append(")AS Row, T.*  FROM Shop_SKUItems T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Shop.Products.SKUItem GetModel(long SkuId, long AttributeId, long ValueId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 SkuId,AttributeId,ValueId FROM Shop_SKUItems ");
            builder.Append(" WHERE SkuId=@SkuId and AttributeId=@AttributeId and ValueId=@ValueId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SkuId", SqlDbType.BigInt, 8), new SqlParameter("@AttributeId", SqlDbType.BigInt, 8), new SqlParameter("@ValueId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = SkuId;
            cmdParms[1].Value = AttributeId;
            cmdParms[2].Value = ValueId;
            Maticsoft.Model.Shop.Products.SKUItem item = new Maticsoft.Model.Shop.Products.SKUItem();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["SkuId"] != null) && (set.Tables[0].Rows[0]["SkuId"].ToString() != ""))
            {
                item.SkuId = long.Parse(set.Tables[0].Rows[0]["SkuId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["AttributeId"] != null) && (set.Tables[0].Rows[0]["AttributeId"].ToString() != ""))
            {
                item.AttributeId = long.Parse(set.Tables[0].Rows[0]["AttributeId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ValueId"] != null) && (set.Tables[0].Rows[0]["ValueId"].ToString() != ""))
            {
                item.ValueId = long.Parse(set.Tables[0].Rows[0]["ValueId"].ToString());
            }
            return item;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_SKUItems ");
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

        public DataSet GetSKUItem4AttrValByProductId(long productId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\nSELECT  SR.SkuId\r\n      , SI.SpecId\r\n      , SI.AttributeId\r\n      , SI.ValueId\r\n      , SI.ImageUrl\r\n      , SI.ValueStr\r\n      , SI.ProductId\r\n      \r\n      , AB.AttributeName\r\n      , AB.DisplaySequence AS AB_DisplaySequence\r\n      , AB.UsageMode\r\n      , AB.UseAttributeImage\r\n      , AB.UserDefinedPic\r\n      \r\n      , AV.DisplaySequence AS AV_DisplaySequence\r\n      , AV.ValueStr AS AV_ValueStr\r\n      , AV.ImageUrl AS AV_ImageUrl\r\nFROM    Shop_SKUItems SI\r\n        LEFT JOIN Shop_SKURelation SR ON SI.SpecId = SR.SpecId\r\n        LEFT JOIN Shop_Attributes AB ON AB.AttributeId = SI.AttributeId\r\n        LEFT JOIN Shop_AttributeValues AV ON SI.ValueId = AV.ValueId\r\nWHERE   SI.ProductId = @ProductId\r\nORDER BY AB_DisplaySequence,SI.AttributeId,AV_DisplaySequence\r\n");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = productId;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public DataSet GetSKUItem4AttrValBySkuId(long skuId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\nSELECT  SR.SkuId\r\n      , SI.SpecId\r\n      , SI.AttributeId\r\n      , SI.ValueId\r\n      , SI.ImageUrl\r\n      , SI.ValueStr\r\n      , SI.ProductId\r\n      \r\n      , AB.AttributeName\r\n      , AB.DisplaySequence AS AB_DisplaySequence\r\n      , AB.UsageMode\r\n      , AB.UseAttributeImage\r\n      , AB.UserDefinedPic\r\n      \r\n      , AV.DisplaySequence AS AV_DisplaySequence\r\n      , AV.ValueStr AS AV_ValueStr\r\n      , AV.ImageUrl AS AV_ImageUrl\r\nFROM    Shop_SKUItems SI\r\n        LEFT JOIN Shop_SKURelation SR ON SI.SpecId = SR.SpecId\r\n        LEFT JOIN Shop_Attributes AB ON AB.AttributeId = SI.AttributeId\r\n        LEFT JOIN Shop_AttributeValues AV ON SI.ValueId = AV.ValueId\r\nWHERE   SR.SkuId = @SkuId\r\nORDER BY AB_DisplaySequence,SI.AttributeId,AV_DisplaySequence\r\n");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SkuId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = skuId;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public bool Update(Maticsoft.Model.Shop.Products.SKUItem model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_SKUItems SET ");
            builder.Append("SkuId=@SkuId,");
            builder.Append("AttributeId=@AttributeId,");
            builder.Append("ValueId=@ValueId");
            builder.Append(" WHERE SkuId=@SkuId and AttributeId=@AttributeId and ValueId=@ValueId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SkuId", SqlDbType.BigInt, 8), new SqlParameter("@AttributeId", SqlDbType.BigInt, 8), new SqlParameter("@ValueId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = model.SkuId;
            cmdParms[1].Value = model.AttributeId;
            cmdParms[2].Value = model.ValueId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

