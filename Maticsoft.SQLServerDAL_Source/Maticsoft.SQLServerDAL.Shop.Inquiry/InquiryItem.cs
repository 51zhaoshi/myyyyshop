namespace Maticsoft.SQLServerDAL.Shop.Inquiry
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Inquiry;
    using Maticsoft.Model.Shop.Inquiry;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class InquiryItem : IInquiryItem
    {
        public long Add(Maticsoft.Model.Shop.Inquiry.InquiryItem model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_InquiryItem(");
            builder.Append("InquiryId,TargetId,Type,ProductCode,SKU,Name,ThumbnailsUrl,Description,Quantity,CostPrice,SellPrice,AdjustedPrice,Attribute,Remark,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName)");
            builder.Append(" values (");
            builder.Append("@InquiryId,@TargetId,@Type,@ProductCode,@SKU,@Name,@ThumbnailsUrl,@Description,@Quantity,@CostPrice,@SellPrice,@AdjustedPrice,@Attribute,@Remark,@Weight,@Deduct,@Points,@ProductLineId,@SupplierId,@SupplierName)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@InquiryId", SqlDbType.BigInt, 8), new SqlParameter("@TargetId", SqlDbType.BigInt, 8), new SqlParameter("@Type", SqlDbType.SmallInt, 2), new SqlParameter("@ProductCode", SqlDbType.NVarChar, 50), new SqlParameter("@SKU", SqlDbType.NVarChar, 200), new SqlParameter("@Name", SqlDbType.NVarChar, 200), new SqlParameter("@ThumbnailsUrl", SqlDbType.NVarChar, 300), new SqlParameter("@Description", SqlDbType.NVarChar, 500), new SqlParameter("@Quantity", SqlDbType.Int, 4), new SqlParameter("@CostPrice", SqlDbType.Money, 8), new SqlParameter("@SellPrice", SqlDbType.Money, 8), new SqlParameter("@AdjustedPrice", SqlDbType.Money, 8), new SqlParameter("@Attribute", SqlDbType.Text), new SqlParameter("@Remark", SqlDbType.Text), new SqlParameter("@Weight", SqlDbType.Int, 4), new SqlParameter("@Deduct", SqlDbType.Money, 8), 
                new SqlParameter("@Points", SqlDbType.Int, 4), new SqlParameter("@ProductLineId", SqlDbType.Int, 4), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@SupplierName", SqlDbType.NVarChar, 100)
             };
            cmdParms[0].Value = model.InquiryId;
            cmdParms[1].Value = model.TargetId;
            cmdParms[2].Value = model.Type;
            cmdParms[3].Value = model.ProductCode;
            cmdParms[4].Value = model.SKU;
            cmdParms[5].Value = model.Name;
            cmdParms[6].Value = model.ThumbnailsUrl;
            cmdParms[7].Value = model.Description;
            cmdParms[8].Value = model.Quantity;
            cmdParms[9].Value = model.CostPrice;
            cmdParms[10].Value = model.SellPrice;
            cmdParms[11].Value = model.AdjustedPrice;
            cmdParms[12].Value = model.Attribute;
            cmdParms[13].Value = model.Remark;
            cmdParms[14].Value = model.Weight;
            cmdParms[15].Value = model.Deduct;
            cmdParms[0x10].Value = model.Points;
            cmdParms[0x11].Value = model.ProductLineId;
            cmdParms[0x12].Value = model.SupplierId;
            cmdParms[0x13].Value = model.SupplierName;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0L;
            }
            return Convert.ToInt64(single);
        }

        public Maticsoft.Model.Shop.Inquiry.InquiryItem DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Inquiry.InquiryItem item = new Maticsoft.Model.Shop.Inquiry.InquiryItem();
            if (row != null)
            {
                if ((row["ItemId"] != null) && (row["ItemId"].ToString() != ""))
                {
                    item.ItemId = long.Parse(row["ItemId"].ToString());
                }
                if ((row["InquiryId"] != null) && (row["InquiryId"].ToString() != ""))
                {
                    item.InquiryId = long.Parse(row["InquiryId"].ToString());
                }
                if ((row["TargetId"] != null) && (row["TargetId"].ToString() != ""))
                {
                    item.TargetId = long.Parse(row["TargetId"].ToString());
                }
                if ((row["Type"] != null) && (row["Type"].ToString() != ""))
                {
                    item.Type = int.Parse(row["Type"].ToString());
                }
                if (row["ProductCode"] != null)
                {
                    item.ProductCode = row["ProductCode"].ToString();
                }
                if (row["SKU"] != null)
                {
                    item.SKU = row["SKU"].ToString();
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
                if ((row["Quantity"] != null) && (row["Quantity"].ToString() != ""))
                {
                    item.Quantity = int.Parse(row["Quantity"].ToString());
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
                if (row["Attribute"] != null)
                {
                    item.Attribute = row["Attribute"].ToString();
                }
                if (row["Remark"] != null)
                {
                    item.Remark = row["Remark"].ToString();
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

        public bool Delete(long ItemId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_InquiryItem ");
            builder.Append(" where ItemId=@ItemId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ItemId", SqlDbType.BigInt) };
            cmdParms[0].Value = ItemId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ItemIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_InquiryItem ");
            builder.Append(" where ItemId in (" + ItemIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(long ItemId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_InquiryItem");
            builder.Append(" where ItemId=@ItemId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ItemId", SqlDbType.BigInt) };
            cmdParms[0].Value = ItemId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ItemId,InquiryId,TargetId,Type,ProductCode,SKU,Name,ThumbnailsUrl,Description,Quantity,CostPrice,SellPrice,AdjustedPrice,Attribute,Remark,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName ");
            builder.Append(" FROM Shop_InquiryItem ");
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
            builder.Append(" ItemId,InquiryId,TargetId,Type,ProductCode,SKU,Name,ThumbnailsUrl,Description,Quantity,CostPrice,SellPrice,AdjustedPrice,Attribute,Remark,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName ");
            builder.Append(" FROM Shop_InquiryItem ");
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
            builder.Append(")AS Row, T.*  from Shop_InquiryItem T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Shop.Inquiry.InquiryItem GetModel(long ItemId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ItemId,InquiryId,TargetId,Type,ProductCode,SKU,Name,ThumbnailsUrl,Description,Quantity,CostPrice,SellPrice,AdjustedPrice,Attribute,Remark,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName from Shop_InquiryItem ");
            builder.Append(" where ItemId=@ItemId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ItemId", SqlDbType.BigInt) };
            cmdParms[0].Value = ItemId;
            new Maticsoft.Model.Shop.Inquiry.InquiryItem();
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
            builder.Append("select count(1) FROM Shop_InquiryItem ");
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

        public bool Update(Maticsoft.Model.Shop.Inquiry.InquiryItem model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_InquiryItem set ");
            builder.Append("InquiryId=@InquiryId,");
            builder.Append("TargetId=@TargetId,");
            builder.Append("Type=@Type,");
            builder.Append("ProductCode=@ProductCode,");
            builder.Append("SKU=@SKU,");
            builder.Append("Name=@Name,");
            builder.Append("ThumbnailsUrl=@ThumbnailsUrl,");
            builder.Append("Description=@Description,");
            builder.Append("Quantity=@Quantity,");
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
                new SqlParameter("@InquiryId", SqlDbType.BigInt, 8), new SqlParameter("@TargetId", SqlDbType.BigInt, 8), new SqlParameter("@Type", SqlDbType.SmallInt, 2), new SqlParameter("@ProductCode", SqlDbType.NVarChar, 50), new SqlParameter("@SKU", SqlDbType.NVarChar, 200), new SqlParameter("@Name", SqlDbType.NVarChar, 200), new SqlParameter("@ThumbnailsUrl", SqlDbType.NVarChar, 300), new SqlParameter("@Description", SqlDbType.NVarChar, 500), new SqlParameter("@Quantity", SqlDbType.Int, 4), new SqlParameter("@CostPrice", SqlDbType.Money, 8), new SqlParameter("@SellPrice", SqlDbType.Money, 8), new SqlParameter("@AdjustedPrice", SqlDbType.Money, 8), new SqlParameter("@Attribute", SqlDbType.Text), new SqlParameter("@Remark", SqlDbType.Text), new SqlParameter("@Weight", SqlDbType.Int, 4), new SqlParameter("@Deduct", SqlDbType.Money, 8), 
                new SqlParameter("@Points", SqlDbType.Int, 4), new SqlParameter("@ProductLineId", SqlDbType.Int, 4), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@SupplierName", SqlDbType.NVarChar, 100), new SqlParameter("@ItemId", SqlDbType.BigInt, 8)
             };
            cmdParms[0].Value = model.InquiryId;
            cmdParms[1].Value = model.TargetId;
            cmdParms[2].Value = model.Type;
            cmdParms[3].Value = model.ProductCode;
            cmdParms[4].Value = model.SKU;
            cmdParms[5].Value = model.Name;
            cmdParms[6].Value = model.ThumbnailsUrl;
            cmdParms[7].Value = model.Description;
            cmdParms[8].Value = model.Quantity;
            cmdParms[9].Value = model.CostPrice;
            cmdParms[10].Value = model.SellPrice;
            cmdParms[11].Value = model.AdjustedPrice;
            cmdParms[12].Value = model.Attribute;
            cmdParms[13].Value = model.Remark;
            cmdParms[14].Value = model.Weight;
            cmdParms[15].Value = model.Deduct;
            cmdParms[0x10].Value = model.Points;
            cmdParms[0x11].Value = model.ProductLineId;
            cmdParms[0x12].Value = model.SupplierId;
            cmdParms[0x13].Value = model.SupplierName;
            cmdParms[20].Value = model.ItemId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

