namespace Maticsoft.SQLServerDAL.Shop.Order
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Order;
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class OrderItems : IOrderItems
    {
        public long Add(Maticsoft.Model.Shop.Order.OrderItems model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_OrderItems(");
            builder.Append("OrderId,OrderCode,ProductId,ProductCode,SKU,Name,ThumbnailsUrl,Description,Quantity,ShipmentQuantity,CostPrice,SellPrice,AdjustedPrice,Attribute,Remark,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName)");
            builder.Append(" values (");
            builder.Append("@OrderId,@OrderCode,@ProductId,@ProductCode,@SKU,@Name,@ThumbnailsUrl,@Description,@Quantity,@ShipmentQuantity,@CostPrice,@SellPrice,@AdjustedPrice,@Attribute,@Remark,@Weight,@Deduct,@Points,@ProductLineId,@SupplierId,@SupplierName)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@OrderId", SqlDbType.BigInt, 8), new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50), new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@ProductCode", SqlDbType.NVarChar, 50), new SqlParameter("@SKU", SqlDbType.NVarChar, 200), new SqlParameter("@Name", SqlDbType.NVarChar, 200), new SqlParameter("@ThumbnailsUrl", SqlDbType.NVarChar, 300), new SqlParameter("@Description", SqlDbType.NVarChar, 500), new SqlParameter("@Quantity", SqlDbType.Int, 4), new SqlParameter("@ShipmentQuantity", SqlDbType.Int, 4), new SqlParameter("@CostPrice", SqlDbType.Money, 8), new SqlParameter("@SellPrice", SqlDbType.Money, 8), new SqlParameter("@AdjustedPrice", SqlDbType.Money, 8), new SqlParameter("@Attribute", SqlDbType.Text), new SqlParameter("@Remark", SqlDbType.Text), new SqlParameter("@Weight", SqlDbType.Int, 4), 
                new SqlParameter("@Deduct", SqlDbType.Money, 8), new SqlParameter("@Points", SqlDbType.Int, 4), new SqlParameter("@ProductLineId", SqlDbType.Int, 4), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@SupplierName", SqlDbType.NVarChar, 100)
             };
            cmdParms[0].Value = model.OrderId;
            cmdParms[1].Value = model.OrderCode;
            cmdParms[2].Value = model.ProductId;
            cmdParms[3].Value = model.ProductCode;
            cmdParms[4].Value = model.SKU;
            cmdParms[5].Value = model.Name;
            cmdParms[6].Value = model.ThumbnailsUrl;
            cmdParms[7].Value = model.Description;
            cmdParms[8].Value = model.Quantity;
            cmdParms[9].Value = model.ShipmentQuantity;
            cmdParms[10].Value = model.CostPrice;
            cmdParms[11].Value = model.SellPrice;
            cmdParms[12].Value = model.AdjustedPrice;
            cmdParms[13].Value = model.Attribute;
            cmdParms[14].Value = model.Remark;
            cmdParms[15].Value = model.Weight;
            cmdParms[0x10].Value = model.Deduct;
            cmdParms[0x11].Value = model.Points;
            cmdParms[0x12].Value = model.ProductLineId;
            cmdParms[0x13].Value = model.SupplierId;
            cmdParms[20].Value = model.SupplierName;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0L;
            }
            return Convert.ToInt64(single);
        }

        public Maticsoft.Model.Shop.Order.OrderItems DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Order.OrderItems items = new Maticsoft.Model.Shop.Order.OrderItems();
            if (row != null)
            {
                if ((row["ItemId"] != null) && (row["ItemId"].ToString() != ""))
                {
                    items.ItemId = long.Parse(row["ItemId"].ToString());
                }
                if ((row["OrderId"] != null) && (row["OrderId"].ToString() != ""))
                {
                    items.OrderId = long.Parse(row["OrderId"].ToString());
                }
                if (row["OrderCode"] != null)
                {
                    items.OrderCode = row["OrderCode"].ToString();
                }
                if ((row["ProductId"] != null) && (row["ProductId"].ToString() != ""))
                {
                    items.ProductId = long.Parse(row["ProductId"].ToString());
                }
                if (row["ProductCode"] != null)
                {
                    items.ProductCode = row["ProductCode"].ToString();
                }
                if (row["SKU"] != null)
                {
                    items.SKU = row["SKU"].ToString();
                }
                if (row["Name"] != null)
                {
                    items.Name = row["Name"].ToString();
                }
                if (row["ThumbnailsUrl"] != null)
                {
                    items.ThumbnailsUrl = row["ThumbnailsUrl"].ToString();
                }
                if (row["Description"] != null)
                {
                    items.Description = row["Description"].ToString();
                }
                if ((row["Quantity"] != null) && (row["Quantity"].ToString() != ""))
                {
                    items.Quantity = int.Parse(row["Quantity"].ToString());
                }
                if ((row["ShipmentQuantity"] != null) && (row["ShipmentQuantity"].ToString() != ""))
                {
                    items.ShipmentQuantity = int.Parse(row["ShipmentQuantity"].ToString());
                }
                if ((row["CostPrice"] != null) && (row["CostPrice"].ToString() != ""))
                {
                    items.CostPrice = decimal.Parse(row["CostPrice"].ToString());
                }
                if ((row["SellPrice"] != null) && (row["SellPrice"].ToString() != ""))
                {
                    items.SellPrice = decimal.Parse(row["SellPrice"].ToString());
                }
                if ((row["AdjustedPrice"] != null) && (row["AdjustedPrice"].ToString() != ""))
                {
                    items.AdjustedPrice = decimal.Parse(row["AdjustedPrice"].ToString());
                }
                if (row["Attribute"] != null)
                {
                    items.Attribute = row["Attribute"].ToString();
                }
                if (row["Remark"] != null)
                {
                    items.Remark = row["Remark"].ToString();
                }
                if ((row["Weight"] != null) && (row["Weight"].ToString() != ""))
                {
                    items.Weight = int.Parse(row["Weight"].ToString());
                }
                if ((row["Deduct"] != null) && (row["Deduct"].ToString() != ""))
                {
                    items.Deduct = new decimal?(decimal.Parse(row["Deduct"].ToString()));
                }
                if ((row["Points"] != null) && (row["Points"].ToString() != ""))
                {
                    items.Points = int.Parse(row["Points"].ToString());
                }
                if ((row["ProductLineId"] != null) && (row["ProductLineId"].ToString() != ""))
                {
                    items.ProductLineId = new int?(int.Parse(row["ProductLineId"].ToString()));
                }
                if ((row["SupplierId"] != null) && (row["SupplierId"].ToString() != ""))
                {
                    items.SupplierId = new int?(int.Parse(row["SupplierId"].ToString()));
                }
                if (row["SupplierName"] != null)
                {
                    items.SupplierName = row["SupplierName"].ToString();
                }
            }
            return items;
        }

        public bool Delete(long ItemId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_OrderItems ");
            builder.Append(" where ItemId=@ItemId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ItemId", SqlDbType.BigInt) };
            cmdParms[0].Value = ItemId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ItemIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_OrderItems ");
            builder.Append(" where ItemId in (" + ItemIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(long ItemId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_OrderItems");
            builder.Append(" where ItemId=@ItemId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ItemId", SqlDbType.BigInt) };
            cmdParms[0].Value = ItemId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ItemId,OrderId,OrderCode,ProductId,ProductCode,SKU,Name,ThumbnailsUrl,Description,Quantity,ShipmentQuantity,CostPrice,SellPrice,AdjustedPrice,Attribute,Remark,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName ");
            builder.Append(" FROM Shop_OrderItems ");
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
            builder.Append(" ItemId,OrderId,OrderCode,ProductId,ProductCode,SKU,Name,ThumbnailsUrl,Description,Quantity,ShipmentQuantity,CostPrice,SellPrice,AdjustedPrice,Attribute,Remark,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName ");
            builder.Append(" FROM Shop_OrderItems ");
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
                builder.Append("order by T.ItemId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_OrderItems T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Shop.Order.OrderItems GetModel(long ItemId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ItemId,OrderId,OrderCode,ProductId,ProductCode,SKU,Name,ThumbnailsUrl,Description,Quantity,ShipmentQuantity,CostPrice,SellPrice,AdjustedPrice,Attribute,Remark,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName from Shop_OrderItems ");
            builder.Append(" where ItemId=@ItemId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ItemId", SqlDbType.BigInt) };
            cmdParms[0].Value = ItemId;
            new Maticsoft.Model.Shop.Order.OrderItems();
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
            builder.Append("select count(1) FROM Shop_OrderItems ");
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

        public DataSet GetSaleRecordByPage(long productId, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by T.ItemId desc");
            }
            builder.Append(")AS Row,  T.SellPrice , T.ShipmentQuantity , p.BuyerName ,p.CreatedDate  from Shop_OrderItems T ");
            builder.AppendFormat(" JOIN Shop_Orders p ON T.ProductId = {0} AND p.OrderStatus = 2  and  T.OrderId=p.OrderId", productId);
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetSaleRecordCount(long productId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT COUNT(1) FROM Shop_OrderItems tt JOIN dbo.Shop_Orders p");
            builder.AppendFormat(" ON tt.ProductId={0} AND p.OrderStatus=2 AND tt.OrderId=p.OrderId", productId);
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Maticsoft.Model.Shop.Order.OrderItems model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_OrderItems set ");
            builder.Append("OrderId=@OrderId,");
            builder.Append("OrderCode=@OrderCode,");
            builder.Append("ProductId=@ProductId,");
            builder.Append("ProductCode=@ProductCode,");
            builder.Append("SKU=@SKU,");
            builder.Append("Name=@Name,");
            builder.Append("ThumbnailsUrl=@ThumbnailsUrl,");
            builder.Append("Description=@Description,");
            builder.Append("Quantity=@Quantity,");
            builder.Append("ShipmentQuantity=@ShipmentQuantity,");
            builder.Append("CostPrice=@CostPrice,");
            builder.Append("SellPrice=@SellPrice,");
            builder.Append("AdjustedPrice=@AdjustedPrice,");
            builder.Append("Attribute=@Attribute,");
            builder.Append("Remark=@Remark,");
            builder.Append("Weight=@Weight,");
            builder.Append("Deduct=@Deduct,");
            builder.Append("Points=@Points,");
            builder.Append("ProductLineId=@ProductLineId,");
            builder.Append("SupplierId=@SupplierId,");
            builder.Append("SupplierName=@SupplierName");
            builder.Append(" where ItemId=@ItemId");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@OrderId", SqlDbType.BigInt, 8), new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50), new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@ProductCode", SqlDbType.NVarChar, 50), new SqlParameter("@SKU", SqlDbType.NVarChar, 200), new SqlParameter("@Name", SqlDbType.NVarChar, 200), new SqlParameter("@ThumbnailsUrl", SqlDbType.NVarChar, 300), new SqlParameter("@Description", SqlDbType.NVarChar, 500), new SqlParameter("@Quantity", SqlDbType.Int, 4), new SqlParameter("@ShipmentQuantity", SqlDbType.Int, 4), new SqlParameter("@CostPrice", SqlDbType.Money, 8), new SqlParameter("@SellPrice", SqlDbType.Money, 8), new SqlParameter("@AdjustedPrice", SqlDbType.Money, 8), new SqlParameter("@Attribute", SqlDbType.Text), new SqlParameter("@Remark", SqlDbType.Text), new SqlParameter("@Weight", SqlDbType.Int, 4), 
                new SqlParameter("@Deduct", SqlDbType.Money, 8), new SqlParameter("@Points", SqlDbType.Int, 4), new SqlParameter("@ProductLineId", SqlDbType.Int, 4), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@SupplierName", SqlDbType.NVarChar, 100), new SqlParameter("@ItemId", SqlDbType.BigInt, 8)
             };
            cmdParms[0].Value = model.OrderId;
            cmdParms[1].Value = model.OrderCode;
            cmdParms[2].Value = model.ProductId;
            cmdParms[3].Value = model.ProductCode;
            cmdParms[4].Value = model.SKU;
            cmdParms[5].Value = model.Name;
            cmdParms[6].Value = model.ThumbnailsUrl;
            cmdParms[7].Value = model.Description;
            cmdParms[8].Value = model.Quantity;
            cmdParms[9].Value = model.ShipmentQuantity;
            cmdParms[10].Value = model.CostPrice;
            cmdParms[11].Value = model.SellPrice;
            cmdParms[12].Value = model.AdjustedPrice;
            cmdParms[13].Value = model.Attribute;
            cmdParms[14].Value = model.Remark;
            cmdParms[15].Value = model.Weight;
            cmdParms[0x10].Value = model.Deduct;
            cmdParms[0x11].Value = model.Points;
            cmdParms[0x12].Value = model.ProductLineId;
            cmdParms[0x13].Value = model.SupplierId;
            cmdParms[20].Value = model.SupplierName;
            cmdParms[0x15].Value = model.ItemId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

