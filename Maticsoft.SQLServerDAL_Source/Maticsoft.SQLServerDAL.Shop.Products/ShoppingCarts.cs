namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.ShoppingCart.Model;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ShoppingCarts : IShoppingCarts
    {
        public bool Add(ShoppingCartItem model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_ShoppingCarts(");
            builder.Append("ItemId,UserId,Target,Quantity,ItemType,Name,ThumbnailsUrl,Description,CostPrice,SellPrice,AdjustedPrice,Attributes,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName)");
            builder.Append(" values (");
            builder.Append("@ItemId,@UserId,@Target,@Quantity,@ItemType,@Name,@ThumbnailsUrl,@Description,@CostPrice,@SellPrice,@AdjustedPrice,@Attributes,@Weight,@Deduct,@Points,@ProductLineId,@SupplierId,@SupplierName)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@ItemId", SqlDbType.Int, 4), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@SKU", SqlDbType.NVarChar, 50), new SqlParameter("@Quantity", SqlDbType.Int, 4), new SqlParameter("@ItemType", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 200), new SqlParameter("@ThumbnailsUrl", SqlDbType.NVarChar, 300), new SqlParameter("@Description", SqlDbType.NVarChar, 500), new SqlParameter("@CostPrice", SqlDbType.Money, 8), new SqlParameter("@SellPrice", SqlDbType.Money, 8), new SqlParameter("@AdjustedPrice", SqlDbType.Money, 8), new SqlParameter("@Attributes", SqlDbType.Text), new SqlParameter("@Weight", SqlDbType.Int, 4), new SqlParameter("@Deduct", SqlDbType.Money, 8), new SqlParameter("@Points", SqlDbType.Int, 4), new SqlParameter("@ProductLineId", SqlDbType.Int, 4), 
                new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@SupplierName", SqlDbType.NVarChar, 100)
             };
            cmdParms[0].Value = model.ItemId;
            cmdParms[1].Value = model.UserId;
            cmdParms[2].Value = model.SKU;
            cmdParms[3].Value = model.Quantity;
            cmdParms[4].Value = model.ItemType;
            cmdParms[5].Value = model.Name;
            cmdParms[6].Value = model.ThumbnailsUrl;
            cmdParms[7].Value = model.Description;
            cmdParms[8].Value = model.CostPrice;
            cmdParms[9].Value = model.SellPrice;
            cmdParms[10].Value = model.AdjustedPrice;
            cmdParms[11].Value = model.Attributes;
            cmdParms[12].Value = model.Weight;
            cmdParms[13].Value = model.Deduct;
            cmdParms[14].Value = model.Points;
            cmdParms[15].Value = model.ProductLineId;
            cmdParms[0x10].Value = model.SupplierId;
            cmdParms[0x11].Value = model.SupplierName;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public ShoppingCartItem DataRowToModel(DataRow row)
        {
            ShoppingCartItem item = new ShoppingCartItem();
            if (row != null)
            {
                if ((row["ItemId"] != null) && (row["ItemId"].ToString() != ""))
                {
                    item.ItemId = int.Parse(row["ItemId"].ToString());
                }
                if ((row["UserId"] != null) && (row["UserId"].ToString() != ""))
                {
                    item.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["SKU"] != null)
                {
                    item.SKU = row["SKU"].ToString();
                }
                if ((row["Quantity"] != null) && (row["Quantity"].ToString() != ""))
                {
                    item.Quantity = int.Parse(row["Quantity"].ToString());
                }
                if ((row["ItemType"] != null) && (row["ItemType"].ToString() != ""))
                {
                    item.ItemType = (CartItemType) int.Parse(row["ItemType"].ToString());
                }
                if (row["Name"] != null)
                {
                    item.Name = row["Name"].ToString();
                }
                if (row["ThumbnailsUrl"] != null)
                {
                    item.ThumbnailsUrl = row["ThumbnailsUrl"].ToString();
                }
                if (row["Description"] != null)
                {
                    item.Description = row["Description"].ToString();
                }
                if ((row["CostPrice"] != null) && (row["CostPrice"].ToString() != ""))
                {
                    item.CostPrice = decimal.Parse(row["CostPrice"].ToString());
                }
                if ((row["SellPrice"] != null) && (row["SellPrice"].ToString() != ""))
                {
                    item.SellPrice = decimal.Parse(row["SellPrice"].ToString());
                }
                if ((row["AdjustedPrice"] != null) && (row["AdjustedPrice"].ToString() != ""))
                {
                    item.AdjustedPrice = decimal.Parse(row["AdjustedPrice"].ToString());
                }
                if (row["Attributes"] != null)
                {
                    item.Attributes = row["Attributes"].ToString();
                }
                if ((row["Weight"] != null) && (row["Weight"].ToString() != ""))
                {
                    item.Weight = int.Parse(row["Weight"].ToString());
                }
                if ((row["Deduct"] != null) && (row["Deduct"].ToString() != ""))
                {
                    item.Deduct = new decimal?(decimal.Parse(row["Deduct"].ToString()));
                }
                if ((row["Points"] != null) && (row["Points"].ToString() != ""))
                {
                    item.Points = int.Parse(row["Points"].ToString());
                }
                if ((row["ProductLineId"] != null) && (row["ProductLineId"].ToString() != ""))
                {
                    item.ProductLineId = new int?(int.Parse(row["ProductLineId"].ToString()));
                }
                if ((row["SupplierId"] != null) && (row["SupplierId"].ToString() != ""))
                {
                    item.SupplierId = new int?(int.Parse(row["SupplierId"].ToString()));
                }
                if (row["SupplierName"] != null)
                {
                    item.SupplierName = row["SupplierName"].ToString();
                }
            }
            return item;
        }

        public bool Delete(int ItemId, int UserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_ShoppingCarts ");
            builder.Append(" where ItemId=@ItemId and UserId=@UserId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ItemId", SqlDbType.Int, 4), new SqlParameter("@UserId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ItemId;
            cmdParms[1].Value = UserId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(int ItemId, int UserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_ShoppingCarts");
            builder.Append(" where ItemId=@ItemId and UserId=@UserId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ItemId", SqlDbType.Int, 4), new SqlParameter("@UserId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ItemId;
            cmdParms[1].Value = UserId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ItemId,UserId,Target,Quantity,ItemType,Name,ThumbnailsUrl,Description,CostPrice,SellPrice,AdjustedPrice,Attributes,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName ");
            builder.Append(" FROM Shop_ShoppingCarts ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" ItemId,UserId,Target,Quantity,ItemType,Name,ThumbnailsUrl,Description,CostPrice,SellPrice,AdjustedPrice,Attributes,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName ");
            builder.Append(" FROM Shop_ShoppingCarts ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.UserId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_ShoppingCarts T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ItemId", "Shop_ShoppingCarts");
        }

        public ShoppingCartItem GetModel(int ItemId, int UserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ItemId,UserId,Target,Quantity,ItemType,Name,ThumbnailsUrl,Description,CostPrice,SellPrice,AdjustedPrice,Attributes,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName from Shop_ShoppingCarts ");
            builder.Append(" where ItemId=@ItemId and UserId=@UserId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ItemId", SqlDbType.Int, 4), new SqlParameter("@UserId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ItemId;
            cmdParms[1].Value = UserId;
            new ShoppingCartItem();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Shop_ShoppingCarts ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(ShoppingCartItem model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_ShoppingCarts set ");
            builder.Append("Target=@Target,");
            builder.Append("Quantity=@Quantity,");
            builder.Append("ItemType=@ItemType,");
            builder.Append("Name=@Name,");
            builder.Append("ThumbnailsUrl=@ThumbnailsUrl,");
            builder.Append("Description=@Description,");
            builder.Append("CostPrice=@CostPrice,");
            builder.Append("SellPrice=@SellPrice,");
            builder.Append("AdjustedPrice=@AdjustedPrice,");
            builder.Append("Attributes=@Attributes,");
            builder.Append("Weight=@Weight,");
            builder.Append("Deduct=@Deduct,");
            builder.Append("Points=@Points,");
            builder.Append("ProductLineId=@ProductLineId,");
            builder.Append("SupplierId=@SupplierId,");
            builder.Append("SupplierName=@SupplierName");
            builder.Append(" where ItemId=@ItemId and UserId=@UserId ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@SKU", SqlDbType.NVarChar, 50), new SqlParameter("@Quantity", SqlDbType.Int, 4), new SqlParameter("@ItemType", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 200), new SqlParameter("@ThumbnailsUrl", SqlDbType.NVarChar, 300), new SqlParameter("@Description", SqlDbType.NVarChar, 500), new SqlParameter("@CostPrice", SqlDbType.Money, 8), new SqlParameter("@SellPrice", SqlDbType.Money, 8), new SqlParameter("@AdjustedPrice", SqlDbType.Money, 8), new SqlParameter("@Attributes", SqlDbType.Text), new SqlParameter("@Weight", SqlDbType.Int, 4), new SqlParameter("@Deduct", SqlDbType.Money, 8), new SqlParameter("@Points", SqlDbType.Int, 4), new SqlParameter("@ProductLineId", SqlDbType.Int, 4), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@SupplierName", SqlDbType.NVarChar, 100), 
                new SqlParameter("@ItemId", SqlDbType.Int, 4), new SqlParameter("@UserId", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.SKU;
            cmdParms[1].Value = model.Quantity;
            cmdParms[2].Value = model.ItemType;
            cmdParms[3].Value = model.Name;
            cmdParms[4].Value = model.ThumbnailsUrl;
            cmdParms[5].Value = model.Description;
            cmdParms[6].Value = model.CostPrice;
            cmdParms[7].Value = model.SellPrice;
            cmdParms[8].Value = model.AdjustedPrice;
            cmdParms[9].Value = model.Attributes;
            cmdParms[10].Value = model.Weight;
            cmdParms[11].Value = model.Deduct;
            cmdParms[12].Value = model.Points;
            cmdParms[13].Value = model.ProductLineId;
            cmdParms[14].Value = model.SupplierId;
            cmdParms[15].Value = model.SupplierName;
            cmdParms[0x10].Value = model.ItemId;
            cmdParms[0x11].Value = model.UserId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

