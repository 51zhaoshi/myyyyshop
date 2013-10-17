namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public class SKUInfo : ISKUInfo
    {
        public long Add(Maticsoft.Model.Shop.Products.SKUInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_SKUs(");
            builder.Append("ProductId,SKU,Weight,Stock,AlertStock,CostPrice,SalePrice,Upselling)");
            builder.Append(" VALUES (");
            builder.Append("@ProductId,@SKU,@Weight,@Stock,@AlertStock,@CostPrice,@SalePrice,@Upselling)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@SKU", SqlDbType.NVarChar, 50), new SqlParameter("@Weight", SqlDbType.Int, 4), new SqlParameter("@Stock", SqlDbType.Int, 4), new SqlParameter("@AlertStock", SqlDbType.Int, 4), new SqlParameter("@CostPrice", SqlDbType.Money, 8), new SqlParameter("@SalePrice", SqlDbType.Money, 8), new SqlParameter("@Upselling", SqlDbType.Bit, 1) };
            cmdParms[0].Value = model.ProductId;
            cmdParms[1].Value = model.SKU;
            cmdParms[2].Value = model.Weight;
            cmdParms[3].Value = model.Stock;
            cmdParms[4].Value = model.AlertStock;
            cmdParms[5].Value = model.CostPrice;
            cmdParms[6].Value = model.SalePrice;
            cmdParms[7].Value = model.Upselling;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0L;
            }
            return Convert.ToInt64(single);
        }

        public bool Delete(long SkuId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_SKUs ");
            builder.Append(" WHERE SkuId=@SkuId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SkuId", SqlDbType.BigInt) };
            cmdParms[0].Value = SkuId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string SkuIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_SKUs ");
            builder.Append(" WHERE SkuId in (" + SkuIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(long SkuId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_SKUs");
            builder.Append(" WHERE SkuId=@SkuId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SkuId", SqlDbType.BigInt) };
            cmdParms[0].Value = SkuId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(string SkuCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_SKUs");
            builder.Append(" WHERE SKU=@SkuCode");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SkuCode", SqlDbType.NVarChar) };
            cmdParms[0].Value = SkuCode;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT SkuId,ProductId,SKU,Weight,Stock,AlertStock,CostPrice,SalePrice,Upselling ");
            builder.Append(" FROM Shop_SKUs ");
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
            builder.Append(" SkuId,ProductId,SKU,Weight,Stock,AlertStock,CostPrice,SalePrice,Upselling ");
            builder.Append(" FROM Shop_SKUs ");
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
                builder.Append("ORDER BY T.SkuId desc");
            }
            builder.Append(")AS Row, T.*  FROM Shop_SKUs T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Shop.Products.SKUInfo GetModel(long SkuId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 SkuId,ProductId,SKU,Weight,Stock,AlertStock,CostPrice,SalePrice,Upselling FROM Shop_SKUs ");
            builder.Append(" WHERE SkuId=@SkuId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SkuId", SqlDbType.BigInt) };
            cmdParms[0].Value = SkuId;
            Maticsoft.Model.Shop.Products.SKUInfo info = new Maticsoft.Model.Shop.Products.SKUInfo();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["SkuId"] != null) && (set.Tables[0].Rows[0]["SkuId"].ToString() != ""))
            {
                info.SkuId = long.Parse(set.Tables[0].Rows[0]["SkuId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ProductId"] != null) && (set.Tables[0].Rows[0]["ProductId"].ToString() != ""))
            {
                info.ProductId = long.Parse(set.Tables[0].Rows[0]["ProductId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["SKU"] != null) && (set.Tables[0].Rows[0]["SKU"].ToString() != ""))
            {
                info.SKU = set.Tables[0].Rows[0]["SKU"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Weight"] != null) && (set.Tables[0].Rows[0]["Weight"].ToString() != ""))
            {
                info.Weight = new int?(int.Parse(set.Tables[0].Rows[0]["Weight"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Stock"] != null) && (set.Tables[0].Rows[0]["Stock"].ToString() != ""))
            {
                info.Stock = int.Parse(set.Tables[0].Rows[0]["Stock"].ToString());
            }
            if ((set.Tables[0].Rows[0]["AlertStock"] != null) && (set.Tables[0].Rows[0]["AlertStock"].ToString() != ""))
            {
                info.AlertStock = int.Parse(set.Tables[0].Rows[0]["AlertStock"].ToString());
            }
            if ((set.Tables[0].Rows[0]["CostPrice"] != null) && (set.Tables[0].Rows[0]["CostPrice"].ToString() != ""))
            {
                info.CostPrice = new decimal?(decimal.Parse(set.Tables[0].Rows[0]["CostPrice"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["SalePrice"] != null) && (set.Tables[0].Rows[0]["SalePrice"].ToString() != ""))
            {
                info.SalePrice = decimal.Parse(set.Tables[0].Rows[0]["SalePrice"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Upselling"] != null) && (set.Tables[0].Rows[0]["Upselling"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["Upselling"].ToString() == "1") || (set.Tables[0].Rows[0]["Upselling"].ToString().ToLower() == "true"))
                {
                    info.Upselling = true;
                    return info;
                }
                info.Upselling = false;
            }
            return info;
        }

        public Maticsoft.Model.Shop.Products.SKUInfo GetModelBySKU(string sku)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 SkuId,ProductId,SKU,Weight,Stock,AlertStock,CostPrice,SalePrice,Upselling FROM Shop_SKUs ");
            builder.Append(" WHERE SKU=@SKU");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SKU", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = sku;
            Maticsoft.Model.Shop.Products.SKUInfo info = new Maticsoft.Model.Shop.Products.SKUInfo();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["SkuId"] != null) && (set.Tables[0].Rows[0]["SkuId"].ToString() != ""))
            {
                info.SkuId = long.Parse(set.Tables[0].Rows[0]["SkuId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ProductId"] != null) && (set.Tables[0].Rows[0]["ProductId"].ToString() != ""))
            {
                info.ProductId = long.Parse(set.Tables[0].Rows[0]["ProductId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["SKU"] != null) && (set.Tables[0].Rows[0]["SKU"].ToString() != ""))
            {
                info.SKU = set.Tables[0].Rows[0]["SKU"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Weight"] != null) && (set.Tables[0].Rows[0]["Weight"].ToString() != ""))
            {
                info.Weight = new int?(int.Parse(set.Tables[0].Rows[0]["Weight"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Stock"] != null) && (set.Tables[0].Rows[0]["Stock"].ToString() != ""))
            {
                info.Stock = int.Parse(set.Tables[0].Rows[0]["Stock"].ToString());
            }
            if ((set.Tables[0].Rows[0]["AlertStock"] != null) && (set.Tables[0].Rows[0]["AlertStock"].ToString() != ""))
            {
                info.AlertStock = int.Parse(set.Tables[0].Rows[0]["AlertStock"].ToString());
            }
            if ((set.Tables[0].Rows[0]["CostPrice"] != null) && (set.Tables[0].Rows[0]["CostPrice"].ToString() != ""))
            {
                info.CostPrice = new decimal?(decimal.Parse(set.Tables[0].Rows[0]["CostPrice"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["SalePrice"] != null) && (set.Tables[0].Rows[0]["SalePrice"].ToString() != ""))
            {
                info.SalePrice = decimal.Parse(set.Tables[0].Rows[0]["SalePrice"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Upselling"] != null) && (set.Tables[0].Rows[0]["Upselling"].ToString() != ""))
            {
                if ((set.Tables[0].Rows[0]["Upselling"].ToString() == "1") || (set.Tables[0].Rows[0]["Upselling"].ToString().ToLower() == "true"))
                {
                    info.Upselling = true;
                    return info;
                }
                info.Upselling = false;
            }
            return info;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_SKUs ");
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

        public DataSet GetSKUListByPage(string strWhere, string orderby, int startIndex, int endIndex, out int dataCount, long productId)
        {
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                strWhere = strWhere.Insert(0, " AND ");
            }
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@SqlWhere", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@OrderBy", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@StartIndex", SqlDbType.Int, 4), new SqlParameter("@EndIndex", SqlDbType.Int, 4), new SqlParameter("@ProductId", SqlDbType.BigInt, 8), DbHelperSQL.CreateReturnParam("ReturnValue", SqlDbType.Int, 4) };
            parameters[0].Value = strWhere;
            parameters[1].Value = orderby;
            parameters[2].Value = startIndex;
            parameters[3].Value = endIndex;
            parameters[4].Value = productId;
            return DbHelperSQL.RunProcedure("sp_Shop_ProductSkuInfo_Get", parameters, "ProductSkuInfo", out dataCount);
        }

        public int GetStockById(long productId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT SUM(Stock)Stock FROM Shop_SKUs ");
            builder.Append("WHERE ProductId=@ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt) };
            cmdParms[0].Value = productId;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public int GetStockBySKU(string SKU)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT SUM(Stock)Stock FROM Shop_SKUs ");
            builder.Append("WHERE SKU=@SKU ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SKU", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = SKU;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public DataSet PrductsSkuInfo(long prductId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM  Shop_SKUs ");
            builder.Append("WHERE ProductId=@ProducId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProducId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = prductId;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public bool Update(Maticsoft.Model.Shop.Products.SKUInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_SKUs SET ");
            builder.Append("ProductId=@ProductId,");
            builder.Append("SKU=@SKU,");
            builder.Append("Weight=@Weight,");
            builder.Append("Stock=@Stock,");
            builder.Append("AlertStock=@AlertStock,");
            builder.Append("CostPrice=@CostPrice,");
            builder.Append("SalePrice=@SalePrice,");
            builder.Append("Upselling=@Upselling");
            builder.Append(" WHERE SkuId=@SkuId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@SKU", SqlDbType.NVarChar, 50), new SqlParameter("@Weight", SqlDbType.Int, 4), new SqlParameter("@Stock", SqlDbType.Int, 4), new SqlParameter("@AlertStock", SqlDbType.Int, 4), new SqlParameter("@CostPrice", SqlDbType.Money, 8), new SqlParameter("@SalePrice", SqlDbType.Money, 8), new SqlParameter("@Upselling", SqlDbType.Bit, 1), new SqlParameter("@SkuId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = model.ProductId;
            cmdParms[1].Value = model.SKU;
            cmdParms[2].Value = model.Weight;
            cmdParms[3].Value = model.Stock;
            cmdParms[4].Value = model.AlertStock;
            cmdParms[5].Value = model.CostPrice;
            cmdParms[6].Value = model.SalePrice;
            cmdParms[7].Value = model.Upselling;
            cmdParms[8].Value = model.SkuId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

