namespace Maticsoft.SQLServerDAL.Shop.Order
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Order;
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Orders : IOrders
    {
        public long Add(OrderInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_Orders(");
            builder.Append("OrderCode,ParentOrderId,CreatedDate,UpdatedDate,BuyerID,BuyerName,BuyerEmail,BuyerCellPhone,RegionId,ShipRegion,ShipAddress,ShipZipCode,ShipName,ShipTelPhone,ShipCellPhone,ShipEmail,ShippingModeId,ShippingModeName,RealShippingModeId,RealShippingModeName,ShipperId,ShipperName,ShipperAddress,ShipperCellPhone,Freight,FreightAdjusted,FreightActual,Weight,ShippingStatus,ShipOrderNumber,ExpressCompanyName,ExpressCompanyAbb,PaymentTypeId,PaymentTypeName,PaymentGateway,PaymentStatus,RefundStatus,PayCurrencyCode,PayCurrencyName,PaymentFee,PaymentFeeAdjusted,GatewayOrderId,OrderTotal,OrderPoint,OrderCostPrice,OrderProfit,OrderOtherCost,OrderOptionPrice,DiscountName,DiscountAmount,DiscountAdjusted,DiscountValue,DiscountValueType,CouponCode,CouponName,CouponAmount,CouponValue,CouponValueType,ActivityName,ActivityFreeAmount,ActivityStatus,GroupBuyId,GroupBuyPrice,GroupBuyStatus,Amount,OrderType,OrderStatus,SellerID,SellerName,SellerEmail,SellerCellPhone,CommentStatus,SupplierId,SupplierName,ReferID,ReferURL,OrderIP,Remark,ProductTotal,HasChildren,IsReviews)");
            builder.Append(" values (");
            builder.Append("@OrderCode,@ParentOrderId,@CreatedDate,@UpdatedDate,@BuyerID,@BuyerName,@BuyerEmail,@BuyerCellPhone,@RegionId,@ShipRegion,@ShipAddress,@ShipZipCode,@ShipName,@ShipTelPhone,@ShipCellPhone,@ShipEmail,@ShippingModeId,@ShippingModeName,@RealShippingModeId,@RealShippingModeName,@ShipperId,@ShipperName,@ShipperAddress,@ShipperCellPhone,@Freight,@FreightAdjusted,@FreightActual,@Weight,@ShippingStatus,@ShipOrderNumber,@ExpressCompanyName,@ExpressCompanyAbb,@PaymentTypeId,@PaymentTypeName,@PaymentGateway,@PaymentStatus,@RefundStatus,@PayCurrencyCode,@PayCurrencyName,@PaymentFee,@PaymentFeeAdjusted,@GatewayOrderId,@OrderTotal,@OrderPoint,@OrderCostPrice,@OrderProfit,@OrderOtherCost,@OrderOptionPrice,@DiscountName,@DiscountAmount,@DiscountAdjusted,@DiscountValue,@DiscountValueType,@CouponCode,@CouponName,@CouponAmount,@CouponValue,@CouponValueType,@ActivityName,@ActivityFreeAmount,@ActivityStatus,@GroupBuyId,@GroupBuyPrice,@GroupBuyStatus,@Amount,@OrderType,@OrderStatus,@SellerID,@SellerName,@SellerEmail,@SellerCellPhone,@CommentStatus,@SupplierId,@SupplierName,@ReferID,@ReferURL,@OrderIP,@Remark,@ProductTotal,@HasChildren,@IsReviews)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50), new SqlParameter("@ParentOrderId", SqlDbType.BigInt, 8), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@UpdatedDate", SqlDbType.DateTime), new SqlParameter("@BuyerID", SqlDbType.Int, 4), new SqlParameter("@BuyerName", SqlDbType.NVarChar, 100), new SqlParameter("@BuyerEmail", SqlDbType.NVarChar, 100), new SqlParameter("@BuyerCellPhone", SqlDbType.NVarChar, 50), new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@ShipRegion", SqlDbType.NVarChar, 300), new SqlParameter("@ShipAddress", SqlDbType.NVarChar, 300), new SqlParameter("@ShipZipCode", SqlDbType.NVarChar, 20), new SqlParameter("@ShipName", SqlDbType.NVarChar, 50), new SqlParameter("@ShipTelPhone", SqlDbType.NVarChar, 50), new SqlParameter("@ShipCellPhone", SqlDbType.NVarChar, 50), new SqlParameter("@ShipEmail", SqlDbType.NVarChar, 100), 
                new SqlParameter("@ShippingModeId", SqlDbType.Int, 4), new SqlParameter("@ShippingModeName", SqlDbType.NVarChar, 100), new SqlParameter("@RealShippingModeId", SqlDbType.Int, 4), new SqlParameter("@RealShippingModeName", SqlDbType.NVarChar, 100), new SqlParameter("@ShipperId", SqlDbType.Int, 4), new SqlParameter("@ShipperName", SqlDbType.NVarChar, 100), new SqlParameter("@ShipperAddress", SqlDbType.NVarChar, 300), new SqlParameter("@ShipperCellPhone", SqlDbType.NVarChar, 20), new SqlParameter("@Freight", SqlDbType.Money, 8), new SqlParameter("@FreightAdjusted", SqlDbType.Money, 8), new SqlParameter("@FreightActual", SqlDbType.Money, 8), new SqlParameter("@Weight", SqlDbType.Int, 4), new SqlParameter("@ShippingStatus", SqlDbType.SmallInt, 2), new SqlParameter("@ShipOrderNumber", SqlDbType.NVarChar, 50), new SqlParameter("@ExpressCompanyName", SqlDbType.NVarChar, 500), new SqlParameter("@ExpressCompanyAbb", SqlDbType.NVarChar, 500), 
                new SqlParameter("@PaymentTypeId", SqlDbType.Int, 4), new SqlParameter("@PaymentTypeName", SqlDbType.NVarChar, 100), new SqlParameter("@PaymentGateway", SqlDbType.NVarChar, 50), new SqlParameter("@PaymentStatus", SqlDbType.SmallInt, 2), new SqlParameter("@RefundStatus", SqlDbType.SmallInt, 2), new SqlParameter("@PayCurrencyCode", SqlDbType.NVarChar, 20), new SqlParameter("@PayCurrencyName", SqlDbType.NVarChar, 20), new SqlParameter("@PaymentFee", SqlDbType.Money, 8), new SqlParameter("@PaymentFeeAdjusted", SqlDbType.Money, 8), new SqlParameter("@GatewayOrderId", SqlDbType.NVarChar, 100), new SqlParameter("@OrderTotal", SqlDbType.Money, 8), new SqlParameter("@OrderPoint", SqlDbType.Int, 4), new SqlParameter("@OrderCostPrice", SqlDbType.Money, 8), new SqlParameter("@OrderProfit", SqlDbType.Money, 8), new SqlParameter("@OrderOtherCost", SqlDbType.Money, 8), new SqlParameter("@OrderOptionPrice", SqlDbType.Money, 8), 
                new SqlParameter("@DiscountName", SqlDbType.NVarChar, 200), new SqlParameter("@DiscountAmount", SqlDbType.Money, 8), new SqlParameter("@DiscountAdjusted", SqlDbType.Money, 8), new SqlParameter("@DiscountValue", SqlDbType.Money, 8), new SqlParameter("@DiscountValueType", SqlDbType.SmallInt, 2), new SqlParameter("@CouponCode", SqlDbType.NVarChar, 50), new SqlParameter("@CouponName", SqlDbType.NVarChar, 100), new SqlParameter("@CouponAmount", SqlDbType.Money, 8), new SqlParameter("@CouponValue", SqlDbType.Money, 8), new SqlParameter("@CouponValueType", SqlDbType.SmallInt, 2), new SqlParameter("@ActivityName", SqlDbType.NVarChar, 200), new SqlParameter("@ActivityFreeAmount", SqlDbType.Money, 8), new SqlParameter("@ActivityStatus", SqlDbType.SmallInt, 2), new SqlParameter("@GroupBuyId", SqlDbType.Int, 4), new SqlParameter("@GroupBuyPrice", SqlDbType.Money, 8), new SqlParameter("@GroupBuyStatus", SqlDbType.SmallInt, 2), 
                new SqlParameter("@Amount", SqlDbType.Money, 8), new SqlParameter("@OrderType", SqlDbType.SmallInt, 2), new SqlParameter("@OrderStatus", SqlDbType.SmallInt, 2), new SqlParameter("@SellerID", SqlDbType.Int, 4), new SqlParameter("@SellerName", SqlDbType.NVarChar, 100), new SqlParameter("@SellerEmail", SqlDbType.NVarChar, 100), new SqlParameter("@SellerCellPhone", SqlDbType.NVarChar, 50), new SqlParameter("@CommentStatus", SqlDbType.SmallInt, 2), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@SupplierName", SqlDbType.NVarChar, 100), new SqlParameter("@ReferID", SqlDbType.NVarChar, 50), new SqlParameter("@ReferURL", SqlDbType.NVarChar, 200), new SqlParameter("@OrderIP", SqlDbType.NVarChar, 50), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@ProductTotal", SqlDbType.Money, 8), new SqlParameter("@HasChildren", SqlDbType.Bit, 1), 
                new SqlParameter("@IsReviews", SqlDbType.Bit, 1)
             };
            cmdParms[0].Value = model.OrderCode;
            cmdParms[1].Value = model.ParentOrderId;
            cmdParms[2].Value = model.CreatedDate;
            cmdParms[3].Value = model.UpdatedDate;
            cmdParms[4].Value = model.BuyerID;
            cmdParms[5].Value = model.BuyerName;
            cmdParms[6].Value = model.BuyerEmail;
            cmdParms[7].Value = model.BuyerCellPhone;
            cmdParms[8].Value = model.RegionId;
            cmdParms[9].Value = model.ShipRegion;
            cmdParms[10].Value = model.ShipAddress;
            cmdParms[11].Value = model.ShipZipCode;
            cmdParms[12].Value = model.ShipName;
            cmdParms[13].Value = model.ShipTelPhone;
            cmdParms[14].Value = model.ShipCellPhone;
            cmdParms[15].Value = model.ShipEmail;
            cmdParms[0x10].Value = model.ShippingModeId;
            cmdParms[0x11].Value = model.ShippingModeName;
            cmdParms[0x12].Value = model.RealShippingModeId;
            cmdParms[0x13].Value = model.RealShippingModeName;
            cmdParms[20].Value = model.ShipperId;
            cmdParms[0x15].Value = model.ShipperName;
            cmdParms[0x16].Value = model.ShipperAddress;
            cmdParms[0x17].Value = model.ShipperCellPhone;
            cmdParms[0x18].Value = model.Freight;
            cmdParms[0x19].Value = model.FreightAdjusted;
            cmdParms[0x1a].Value = model.FreightActual;
            cmdParms[0x1b].Value = model.Weight;
            cmdParms[0x1c].Value = model.ShippingStatus;
            cmdParms[0x1d].Value = model.ShipOrderNumber;
            cmdParms[30].Value = model.ExpressCompanyName;
            cmdParms[0x1f].Value = model.ExpressCompanyAbb;
            cmdParms[0x20].Value = model.PaymentTypeId;
            cmdParms[0x21].Value = model.PaymentTypeName;
            cmdParms[0x22].Value = model.PaymentGateway;
            cmdParms[0x23].Value = model.PaymentStatus;
            cmdParms[0x24].Value = model.RefundStatus;
            cmdParms[0x25].Value = model.PayCurrencyCode;
            cmdParms[0x26].Value = model.PayCurrencyName;
            cmdParms[0x27].Value = model.PaymentFee;
            cmdParms[40].Value = model.PaymentFeeAdjusted;
            cmdParms[0x29].Value = model.GatewayOrderId;
            cmdParms[0x2a].Value = model.OrderTotal;
            cmdParms[0x2b].Value = model.OrderPoint;
            cmdParms[0x2c].Value = model.OrderCostPrice;
            cmdParms[0x2d].Value = model.OrderProfit;
            cmdParms[0x2e].Value = model.OrderOtherCost;
            cmdParms[0x2f].Value = model.OrderOptionPrice;
            cmdParms[0x30].Value = model.DiscountName;
            cmdParms[0x31].Value = model.DiscountAmount;
            cmdParms[50].Value = model.DiscountAdjusted;
            cmdParms[0x33].Value = model.DiscountValue;
            cmdParms[0x34].Value = model.DiscountValueType;
            cmdParms[0x35].Value = model.CouponCode;
            cmdParms[0x36].Value = model.CouponName;
            cmdParms[0x37].Value = model.CouponAmount;
            cmdParms[0x38].Value = model.CouponValue;
            cmdParms[0x39].Value = model.CouponValueType;
            cmdParms[0x3a].Value = model.ActivityName;
            cmdParms[0x3b].Value = model.ActivityFreeAmount;
            cmdParms[60].Value = model.ActivityStatus;
            cmdParms[0x3d].Value = model.GroupBuyId;
            cmdParms[0x3e].Value = model.GroupBuyPrice;
            cmdParms[0x3f].Value = model.GroupBuyStatus;
            cmdParms[0x40].Value = model.Amount;
            cmdParms[0x41].Value = model.OrderType;
            cmdParms[0x42].Value = model.OrderStatus;
            cmdParms[0x43].Value = model.SellerID;
            cmdParms[0x44].Value = model.SellerName;
            cmdParms[0x45].Value = model.SellerEmail;
            cmdParms[70].Value = model.SellerCellPhone;
            cmdParms[0x47].Value = model.CommentStatus;
            cmdParms[0x48].Value = model.SupplierId;
            cmdParms[0x49].Value = model.SupplierName;
            cmdParms[0x4a].Value = model.ReferID;
            cmdParms[0x4b].Value = model.ReferURL;
            cmdParms[0x4c].Value = model.OrderIP;
            cmdParms[0x4d].Value = model.Remark;
            cmdParms[0x4e].Value = model.ProductTotal;
            cmdParms[0x4f].Value = model.HasChildren;
            cmdParms[80].Value = model.IsReviews;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0L;
            }
            return Convert.ToInt64(single);
        }

        public OrderInfo DataRowToModel(DataRow row)
        {
            OrderInfo info = new OrderInfo();
            if (row != null)
            {
                if ((row["OrderId"] != null) && (row["OrderId"].ToString() != ""))
                {
                    info.OrderId = long.Parse(row["OrderId"].ToString());
                }
                if (row["OrderCode"] != null)
                {
                    info.OrderCode = row["OrderCode"].ToString();
                }
                if ((row["ParentOrderId"] != null) && (row["ParentOrderId"].ToString() != ""))
                {
                    info.ParentOrderId = long.Parse(row["ParentOrderId"].ToString());
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    info.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["UpdatedDate"] != null) && (row["UpdatedDate"].ToString() != ""))
                {
                    info.UpdatedDate = new DateTime?(DateTime.Parse(row["UpdatedDate"].ToString()));
                }
                if ((row["BuyerID"] != null) && (row["BuyerID"].ToString() != ""))
                {
                    info.BuyerID = int.Parse(row["BuyerID"].ToString());
                }
                if (row["BuyerName"] != null)
                {
                    info.BuyerName = row["BuyerName"].ToString();
                }
                if (row["BuyerEmail"] != null)
                {
                    info.BuyerEmail = row["BuyerEmail"].ToString();
                }
                if (row["BuyerCellPhone"] != null)
                {
                    info.BuyerCellPhone = row["BuyerCellPhone"].ToString();
                }
                if ((row["RegionId"] != null) && (row["RegionId"].ToString() != ""))
                {
                    info.RegionId = new int?(int.Parse(row["RegionId"].ToString()));
                }
                if (row["ShipRegion"] != null)
                {
                    info.ShipRegion = row["ShipRegion"].ToString();
                }
                if (row["ShipAddress"] != null)
                {
                    info.ShipAddress = row["ShipAddress"].ToString();
                }
                if (row["ShipZipCode"] != null)
                {
                    info.ShipZipCode = row["ShipZipCode"].ToString();
                }
                if (row["ShipName"] != null)
                {
                    info.ShipName = row["ShipName"].ToString();
                }
                if (row["ShipTelPhone"] != null)
                {
                    info.ShipTelPhone = row["ShipTelPhone"].ToString();
                }
                if (row["ShipCellPhone"] != null)
                {
                    info.ShipCellPhone = row["ShipCellPhone"].ToString();
                }
                if (row["ShipEmail"] != null)
                {
                    info.ShipEmail = row["ShipEmail"].ToString();
                }
                if ((row["ShippingModeId"] != null) && (row["ShippingModeId"].ToString() != ""))
                {
                    info.ShippingModeId = new int?(int.Parse(row["ShippingModeId"].ToString()));
                }
                if (row["ShippingModeName"] != null)
                {
                    info.ShippingModeName = row["ShippingModeName"].ToString();
                }
                if ((row["RealShippingModeId"] != null) && (row["RealShippingModeId"].ToString() != ""))
                {
                    info.RealShippingModeId = new int?(int.Parse(row["RealShippingModeId"].ToString()));
                }
                if (row["RealShippingModeName"] != null)
                {
                    info.RealShippingModeName = row["RealShippingModeName"].ToString();
                }
                if ((row["ShipperId"] != null) && (row["ShipperId"].ToString() != ""))
                {
                    info.ShipperId = new int?(int.Parse(row["ShipperId"].ToString()));
                }
                if (row["ShipperName"] != null)
                {
                    info.ShipperName = row["ShipperName"].ToString();
                }
                if (row["ShipperAddress"] != null)
                {
                    info.ShipperAddress = row["ShipperAddress"].ToString();
                }
                if (row["ShipperCellPhone"] != null)
                {
                    info.ShipperCellPhone = row["ShipperCellPhone"].ToString();
                }
                if ((row["Freight"] != null) && (row["Freight"].ToString() != ""))
                {
                    info.Freight = new decimal?(decimal.Parse(row["Freight"].ToString()));
                }
                if ((row["FreightAdjusted"] != null) && (row["FreightAdjusted"].ToString() != ""))
                {
                    info.FreightAdjusted = new decimal?(decimal.Parse(row["FreightAdjusted"].ToString()));
                }
                if ((row["FreightActual"] != null) && (row["FreightActual"].ToString() != ""))
                {
                    info.FreightActual = new decimal?(decimal.Parse(row["FreightActual"].ToString()));
                }
                if ((row["Weight"] != null) && (row["Weight"].ToString() != ""))
                {
                    info.Weight = new int?(int.Parse(row["Weight"].ToString()));
                }
                if ((row["ShippingStatus"] != null) && (row["ShippingStatus"].ToString() != ""))
                {
                    info.ShippingStatus = int.Parse(row["ShippingStatus"].ToString());
                }
                if (row["ShipOrderNumber"] != null)
                {
                    info.ShipOrderNumber = row["ShipOrderNumber"].ToString();
                }
                if (row["ExpressCompanyName"] != null)
                {
                    info.ExpressCompanyName = row["ExpressCompanyName"].ToString();
                }
                if (row["ExpressCompanyAbb"] != null)
                {
                    info.ExpressCompanyAbb = row["ExpressCompanyAbb"].ToString();
                }
                if ((row["PaymentTypeId"] != null) && (row["PaymentTypeId"].ToString() != ""))
                {
                    info.PaymentTypeId = int.Parse(row["PaymentTypeId"].ToString());
                }
                if (row["PaymentTypeName"] != null)
                {
                    info.PaymentTypeName = row["PaymentTypeName"].ToString();
                }
                if (row["PaymentGateway"] != null)
                {
                    info.PaymentGateway = row["PaymentGateway"].ToString();
                }
                if ((row["PaymentStatus"] != null) && (row["PaymentStatus"].ToString() != ""))
                {
                    info.PaymentStatus = int.Parse(row["PaymentStatus"].ToString());
                }
                if ((row["RefundStatus"] != null) && (row["RefundStatus"].ToString() != ""))
                {
                    info.RefundStatus = int.Parse(row["RefundStatus"].ToString());
                }
                if (row["PayCurrencyCode"] != null)
                {
                    info.PayCurrencyCode = row["PayCurrencyCode"].ToString();
                }
                if (row["PayCurrencyName"] != null)
                {
                    info.PayCurrencyName = row["PayCurrencyName"].ToString();
                }
                if ((row["PaymentFee"] != null) && (row["PaymentFee"].ToString() != ""))
                {
                    info.PaymentFee = new decimal?(decimal.Parse(row["PaymentFee"].ToString()));
                }
                if ((row["PaymentFeeAdjusted"] != null) && (row["PaymentFeeAdjusted"].ToString() != ""))
                {
                    info.PaymentFeeAdjusted = new decimal?(decimal.Parse(row["PaymentFeeAdjusted"].ToString()));
                }
                if (row["GatewayOrderId"] != null)
                {
                    info.GatewayOrderId = row["GatewayOrderId"].ToString();
                }
                if ((row["OrderTotal"] != null) && (row["OrderTotal"].ToString() != ""))
                {
                    info.OrderTotal = decimal.Parse(row["OrderTotal"].ToString());
                }
                if ((row["OrderPoint"] != null) && (row["OrderPoint"].ToString() != ""))
                {
                    info.OrderPoint = int.Parse(row["OrderPoint"].ToString());
                }
                if ((row["OrderCostPrice"] != null) && (row["OrderCostPrice"].ToString() != ""))
                {
                    info.OrderCostPrice = new decimal?(decimal.Parse(row["OrderCostPrice"].ToString()));
                }
                if ((row["OrderProfit"] != null) && (row["OrderProfit"].ToString() != ""))
                {
                    info.OrderProfit = new decimal?(decimal.Parse(row["OrderProfit"].ToString()));
                }
                if ((row["OrderOtherCost"] != null) && (row["OrderOtherCost"].ToString() != ""))
                {
                    info.OrderOtherCost = new decimal?(decimal.Parse(row["OrderOtherCost"].ToString()));
                }
                if ((row["OrderOptionPrice"] != null) && (row["OrderOptionPrice"].ToString() != ""))
                {
                    info.OrderOptionPrice = new decimal?(decimal.Parse(row["OrderOptionPrice"].ToString()));
                }
                if (row["DiscountName"] != null)
                {
                    info.DiscountName = row["DiscountName"].ToString();
                }
                if ((row["DiscountAmount"] != null) && (row["DiscountAmount"].ToString() != ""))
                {
                    info.DiscountAmount = new decimal?(decimal.Parse(row["DiscountAmount"].ToString()));
                }
                if ((row["DiscountAdjusted"] != null) && (row["DiscountAdjusted"].ToString() != ""))
                {
                    info.DiscountAdjusted = new decimal?(decimal.Parse(row["DiscountAdjusted"].ToString()));
                }
                if ((row["DiscountValue"] != null) && (row["DiscountValue"].ToString() != ""))
                {
                    info.DiscountValue = new decimal?(decimal.Parse(row["DiscountValue"].ToString()));
                }
                if ((row["DiscountValueType"] != null) && (row["DiscountValueType"].ToString() != ""))
                {
                    info.DiscountValueType = new int?(int.Parse(row["DiscountValueType"].ToString()));
                }
                if (row["CouponCode"] != null)
                {
                    info.CouponCode = row["CouponCode"].ToString();
                }
                if (row["CouponName"] != null)
                {
                    info.CouponName = row["CouponName"].ToString();
                }
                if ((row["CouponAmount"] != null) && (row["CouponAmount"].ToString() != ""))
                {
                    info.CouponAmount = new decimal?(decimal.Parse(row["CouponAmount"].ToString()));
                }
                if ((row["CouponValue"] != null) && (row["CouponValue"].ToString() != ""))
                {
                    info.CouponValue = new decimal?(decimal.Parse(row["CouponValue"].ToString()));
                }
                if ((row["CouponValueType"] != null) && (row["CouponValueType"].ToString() != ""))
                {
                    info.CouponValueType = new int?(int.Parse(row["CouponValueType"].ToString()));
                }
                if (row["ActivityName"] != null)
                {
                    info.ActivityName = row["ActivityName"].ToString();
                }
                if ((row["ActivityFreeAmount"] != null) && (row["ActivityFreeAmount"].ToString() != ""))
                {
                    info.ActivityFreeAmount = new decimal?(decimal.Parse(row["ActivityFreeAmount"].ToString()));
                }
                if ((row["ActivityStatus"] != null) && (row["ActivityStatus"].ToString() != ""))
                {
                    info.ActivityStatus = int.Parse(row["ActivityStatus"].ToString());
                }
                if ((row["GroupBuyId"] != null) && (row["GroupBuyId"].ToString() != ""))
                {
                    info.GroupBuyId = new int?(int.Parse(row["GroupBuyId"].ToString()));
                }
                if ((row["GroupBuyPrice"] != null) && (row["GroupBuyPrice"].ToString() != ""))
                {
                    info.GroupBuyPrice = new decimal?(decimal.Parse(row["GroupBuyPrice"].ToString()));
                }
                if ((row["GroupBuyStatus"] != null) && (row["GroupBuyStatus"].ToString() != ""))
                {
                    info.GroupBuyStatus = int.Parse(row["GroupBuyStatus"].ToString());
                }
                if ((row["Amount"] != null) && (row["Amount"].ToString() != ""))
                {
                    info.Amount = decimal.Parse(row["Amount"].ToString());
                }
                if ((row["OrderType"] != null) && (row["OrderType"].ToString() != ""))
                {
                    info.OrderType = int.Parse(row["OrderType"].ToString());
                }
                if ((row["OrderStatus"] != null) && (row["OrderStatus"].ToString() != ""))
                {
                    info.OrderStatus = int.Parse(row["OrderStatus"].ToString());
                }
                if ((row["SellerID"] != null) && (row["SellerID"].ToString() != ""))
                {
                    info.SellerID = new int?(int.Parse(row["SellerID"].ToString()));
                }
                if (row["SellerName"] != null)
                {
                    info.SellerName = row["SellerName"].ToString();
                }
                if (row["SellerEmail"] != null)
                {
                    info.SellerEmail = row["SellerEmail"].ToString();
                }
                if (row["SellerCellPhone"] != null)
                {
                    info.SellerCellPhone = row["SellerCellPhone"].ToString();
                }
                if ((row["CommentStatus"] != null) && (row["CommentStatus"].ToString() != ""))
                {
                    info.CommentStatus = int.Parse(row["CommentStatus"].ToString());
                }
                if ((row["SupplierId"] != null) && (row["SupplierId"].ToString() != ""))
                {
                    info.SupplierId = new int?(int.Parse(row["SupplierId"].ToString()));
                }
                if (row["SupplierName"] != null)
                {
                    info.SupplierName = row["SupplierName"].ToString();
                }
                if (row["ReferID"] != null)
                {
                    info.ReferID = row["ReferID"].ToString();
                }
                if (row["ReferURL"] != null)
                {
                    info.ReferURL = row["ReferURL"].ToString();
                }
                if (row["OrderIP"] != null)
                {
                    info.OrderIP = row["OrderIP"].ToString();
                }
                if (row["Remark"] != null)
                {
                    info.Remark = row["Remark"].ToString();
                }
                if ((row["ProductTotal"] != null) && (row["ProductTotal"].ToString() != ""))
                {
                    info.ProductTotal = decimal.Parse(row["ProductTotal"].ToString());
                }
                if ((row["HasChildren"] != null) && (row["HasChildren"].ToString() != ""))
                {
                    if ((row["HasChildren"].ToString() == "1") || (row["HasChildren"].ToString().ToLower() == "true"))
                    {
                        info.HasChildren = true;
                    }
                    else
                    {
                        info.HasChildren = false;
                    }
                }
                if ((row["IsReviews"] != null) && (row["IsReviews"].ToString() != ""))
                {
                    if ((row["IsReviews"].ToString() == "1") || (row["IsReviews"].ToString().ToLower() == "true"))
                    {
                        info.IsReviews = true;
                        return info;
                    }
                    info.IsReviews = false;
                }
            }
            return info;
        }

        public bool Delete(long OrderId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_Orders ");
            builder.Append(" where OrderId=@OrderId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@OrderId", SqlDbType.BigInt) };
            cmdParms[0].Value = OrderId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string OrderIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_Orders ");
            builder.Append(" where OrderId in (" + OrderIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(long OrderId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_Orders");
            builder.Append(" where OrderId=@OrderId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@OrderId", SqlDbType.BigInt) };
            cmdParms[0].Value = OrderId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select OrderId,OrderCode,ParentOrderId,CreatedDate,UpdatedDate,BuyerID,BuyerName,BuyerEmail,BuyerCellPhone,RegionId,ShipRegion,ShipAddress,ShipZipCode,ShipName,ShipTelPhone,ShipCellPhone,ShipEmail,ShippingModeId,ShippingModeName,RealShippingModeId,RealShippingModeName,ShipperId,ShipperName,ShipperAddress,ShipperCellPhone,Freight,FreightAdjusted,FreightActual,Weight,ShippingStatus,ShipOrderNumber,ExpressCompanyName,ExpressCompanyAbb,PaymentTypeId,PaymentTypeName,PaymentGateway,PaymentStatus,RefundStatus,PayCurrencyCode,PayCurrencyName,PaymentFee,PaymentFeeAdjusted,GatewayOrderId,OrderTotal,OrderPoint,OrderCostPrice,OrderProfit,OrderOtherCost,OrderOptionPrice,DiscountName,DiscountAmount,DiscountAdjusted,DiscountValue,DiscountValueType,CouponCode,CouponName,CouponAmount,CouponValue,CouponValueType,ActivityName,ActivityFreeAmount,ActivityStatus,GroupBuyId,GroupBuyPrice,GroupBuyStatus,Amount,OrderType,OrderStatus,SellerID,SellerName,SellerEmail,SellerCellPhone,CommentStatus,SupplierId,SupplierName,ReferID,ReferURL,OrderIP,Remark,ProductTotal,HasChildren,IsReviews ");
            builder.Append(" FROM Shop_Orders ");
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
            builder.Append(" OrderId,OrderCode,ParentOrderId,CreatedDate,UpdatedDate,BuyerID,BuyerName,BuyerEmail,BuyerCellPhone,RegionId,ShipRegion,ShipAddress,ShipZipCode,ShipName,ShipTelPhone,ShipCellPhone,ShipEmail,ShippingModeId,ShippingModeName,RealShippingModeId,RealShippingModeName,ShipperId,ShipperName,ShipperAddress,ShipperCellPhone,Freight,FreightAdjusted,FreightActual,Weight,ShippingStatus,ShipOrderNumber,ExpressCompanyName,ExpressCompanyAbb,PaymentTypeId,PaymentTypeName,PaymentGateway,PaymentStatus,RefundStatus,PayCurrencyCode,PayCurrencyName,PaymentFee,PaymentFeeAdjusted,GatewayOrderId,OrderTotal,OrderPoint,OrderCostPrice,OrderProfit,OrderOtherCost,OrderOptionPrice,DiscountName,DiscountAmount,DiscountAdjusted,DiscountValue,DiscountValueType,CouponCode,CouponName,CouponAmount,CouponValue,CouponValueType,ActivityName,ActivityFreeAmount,ActivityStatus,GroupBuyId,GroupBuyPrice,GroupBuyStatus,Amount,OrderType,OrderStatus,SellerID,SellerName,SellerEmail,SellerCellPhone,CommentStatus,SupplierId,SupplierName,ReferID,ReferURL,OrderIP,Remark,ProductTotal,HasChildren,IsReviews ");
            builder.Append(" FROM Shop_Orders ");
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
                builder.Append("order by T.OrderId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_Orders T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public OrderInfo GetModel(long OrderId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 OrderId,OrderCode,ParentOrderId,CreatedDate,UpdatedDate,BuyerID,BuyerName,BuyerEmail,BuyerCellPhone,RegionId,ShipRegion,ShipAddress,ShipZipCode,ShipName,ShipTelPhone,ShipCellPhone,ShipEmail,ShippingModeId,ShippingModeName,RealShippingModeId,RealShippingModeName,ShipperId,ShipperName,ShipperAddress,ShipperCellPhone,Freight,FreightAdjusted,FreightActual,Weight,ShippingStatus,ShipOrderNumber,ExpressCompanyName,ExpressCompanyAbb,PaymentTypeId,PaymentTypeName,PaymentGateway,PaymentStatus,RefundStatus,PayCurrencyCode,PayCurrencyName,PaymentFee,PaymentFeeAdjusted,GatewayOrderId,OrderTotal,OrderPoint,OrderCostPrice,OrderProfit,OrderOtherCost,OrderOptionPrice,DiscountName,DiscountAmount,DiscountAdjusted,DiscountValue,DiscountValueType,CouponCode,CouponName,CouponAmount,CouponValue,CouponValueType,ActivityName,ActivityFreeAmount,ActivityStatus,GroupBuyId,GroupBuyPrice,GroupBuyStatus,Amount,OrderType,OrderStatus,SellerID,SellerName,SellerEmail,SellerCellPhone,CommentStatus,SupplierId,SupplierName,ReferID,ReferURL,OrderIP,Remark,ProductTotal,HasChildren,IsReviews from Shop_Orders ");
            builder.Append(" where OrderId=@OrderId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@OrderId", SqlDbType.BigInt) };
            cmdParms[0].Value = OrderId;
            new OrderInfo();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetPaymentStatusCounts(int userid, int PaymentStatus, int OrderStatusCancel)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT COUNT (*) FROM  Shop_Orders WHERE BuyerID=@BuyerID AND PaymentStatus=@PaymentStatus AND OrderStatus!=@OrderStatus");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BuyerID", userid), new SqlParameter("@PaymentStatus", PaymentStatus), new SqlParameter("@OrderStatus", OrderStatusCancel) };
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Shop_Orders ");
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

        public bool ReturnStatus(long orderId)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }

        public bool SetOrderSuccess(long orderId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Orders set ");
            builder.Append("OrderStatus=2,PaymentStatus=2,ShippingStatus=3");
            builder.Append(" where OrderId=@OrderId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@OrderId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = orderId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Update(OrderInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Orders set ");
            builder.Append("OrderCode=@OrderCode,");
            builder.Append("ParentOrderId=@ParentOrderId,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("UpdatedDate=@UpdatedDate,");
            builder.Append("BuyerID=@BuyerID,");
            builder.Append("BuyerName=@BuyerName,");
            builder.Append("BuyerEmail=@BuyerEmail,");
            builder.Append("BuyerCellPhone=@BuyerCellPhone,");
            builder.Append("RegionId=@RegionId,");
            builder.Append("ShipRegion=@ShipRegion,");
            builder.Append("ShipAddress=@ShipAddress,");
            builder.Append("ShipZipCode=@ShipZipCode,");
            builder.Append("ShipName=@ShipName,");
            builder.Append("ShipTelPhone=@ShipTelPhone,");
            builder.Append("ShipCellPhone=@ShipCellPhone,");
            builder.Append("ShipEmail=@ShipEmail,");
            builder.Append("ShippingModeId=@ShippingModeId,");
            builder.Append("ShippingModeName=@ShippingModeName,");
            builder.Append("RealShippingModeId=@RealShippingModeId,");
            builder.Append("RealShippingModeName=@RealShippingModeName,");
            builder.Append("ShipperId=@ShipperId,");
            builder.Append("ShipperName=@ShipperName,");
            builder.Append("ShipperAddress=@ShipperAddress,");
            builder.Append("ShipperCellPhone=@ShipperCellPhone,");
            builder.Append("Freight=@Freight,");
            builder.Append("FreightAdjusted=@FreightAdjusted,");
            builder.Append("FreightActual=@FreightActual,");
            builder.Append("Weight=@Weight,");
            builder.Append("ShippingStatus=@ShippingStatus,");
            builder.Append("ShipOrderNumber=@ShipOrderNumber,");
            builder.Append("ExpressCompanyName=@ExpressCompanyName,");
            builder.Append("ExpressCompanyAbb=@ExpressCompanyAbb,");
            builder.Append("PaymentTypeId=@PaymentTypeId,");
            builder.Append("PaymentTypeName=@PaymentTypeName,");
            builder.Append("PaymentGateway=@PaymentGateway,");
            builder.Append("PaymentStatus=@PaymentStatus,");
            builder.Append("RefundStatus=@RefundStatus,");
            builder.Append("PayCurrencyCode=@PayCurrencyCode,");
            builder.Append("PayCurrencyName=@PayCurrencyName,");
            builder.Append("PaymentFee=@PaymentFee,");
            builder.Append("PaymentFeeAdjusted=@PaymentFeeAdjusted,");
            builder.Append("GatewayOrderId=@GatewayOrderId,");
            builder.Append("OrderTotal=@OrderTotal,");
            builder.Append("OrderPoint=@OrderPoint,");
            builder.Append("OrderCostPrice=@OrderCostPrice,");
            builder.Append("OrderProfit=@OrderProfit,");
            builder.Append("OrderOtherCost=@OrderOtherCost,");
            builder.Append("OrderOptionPrice=@OrderOptionPrice,");
            builder.Append("DiscountName=@DiscountName,");
            builder.Append("DiscountAmount=@DiscountAmount,");
            builder.Append("DiscountAdjusted=@DiscountAdjusted,");
            builder.Append("DiscountValue=@DiscountValue,");
            builder.Append("DiscountValueType=@DiscountValueType,");
            builder.Append("CouponCode=@CouponCode,");
            builder.Append("CouponName=@CouponName,");
            builder.Append("CouponAmount=@CouponAmount,");
            builder.Append("CouponValue=@CouponValue,");
            builder.Append("CouponValueType=@CouponValueType,");
            builder.Append("ActivityName=@ActivityName,");
            builder.Append("ActivityFreeAmount=@ActivityFreeAmount,");
            builder.Append("ActivityStatus=@ActivityStatus,");
            builder.Append("GroupBuyId=@GroupBuyId,");
            builder.Append("GroupBuyPrice=@GroupBuyPrice,");
            builder.Append("GroupBuyStatus=@GroupBuyStatus,");
            builder.Append("Amount=@Amount,");
            builder.Append("OrderType=@OrderType,");
            builder.Append("OrderStatus=@OrderStatus,");
            builder.Append("SellerID=@SellerID,");
            builder.Append("SellerName=@SellerName,");
            builder.Append("SellerEmail=@SellerEmail,");
            builder.Append("SellerCellPhone=@SellerCellPhone,");
            builder.Append("CommentStatus=@CommentStatus,");
            builder.Append("SupplierId=@SupplierId,");
            builder.Append("SupplierName=@SupplierName,");
            builder.Append("ReferID=@ReferID,");
            builder.Append("ReferURL=@ReferURL,");
            builder.Append("OrderIP=@OrderIP,");
            builder.Append("Remark=@Remark,");
            builder.Append("ProductTotal=@ProductTotal,");
            builder.Append("HasChildren=@HasChildren,");
            builder.Append("IsReviews=@IsReviews");
            builder.Append(" where OrderId=@OrderId");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50), new SqlParameter("@ParentOrderId", SqlDbType.BigInt, 8), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@UpdatedDate", SqlDbType.DateTime), new SqlParameter("@BuyerID", SqlDbType.Int, 4), new SqlParameter("@BuyerName", SqlDbType.NVarChar, 100), new SqlParameter("@BuyerEmail", SqlDbType.NVarChar, 100), new SqlParameter("@BuyerCellPhone", SqlDbType.NVarChar, 50), new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@ShipRegion", SqlDbType.NVarChar, 300), new SqlParameter("@ShipAddress", SqlDbType.NVarChar, 300), new SqlParameter("@ShipZipCode", SqlDbType.NVarChar, 20), new SqlParameter("@ShipName", SqlDbType.NVarChar, 50), new SqlParameter("@ShipTelPhone", SqlDbType.NVarChar, 50), new SqlParameter("@ShipCellPhone", SqlDbType.NVarChar, 50), new SqlParameter("@ShipEmail", SqlDbType.NVarChar, 100), 
                new SqlParameter("@ShippingModeId", SqlDbType.Int, 4), new SqlParameter("@ShippingModeName", SqlDbType.NVarChar, 100), new SqlParameter("@RealShippingModeId", SqlDbType.Int, 4), new SqlParameter("@RealShippingModeName", SqlDbType.NVarChar, 100), new SqlParameter("@ShipperId", SqlDbType.Int, 4), new SqlParameter("@ShipperName", SqlDbType.NVarChar, 100), new SqlParameter("@ShipperAddress", SqlDbType.NVarChar, 300), new SqlParameter("@ShipperCellPhone", SqlDbType.NVarChar, 20), new SqlParameter("@Freight", SqlDbType.Money, 8), new SqlParameter("@FreightAdjusted", SqlDbType.Money, 8), new SqlParameter("@FreightActual", SqlDbType.Money, 8), new SqlParameter("@Weight", SqlDbType.Int, 4), new SqlParameter("@ShippingStatus", SqlDbType.SmallInt, 2), new SqlParameter("@ShipOrderNumber", SqlDbType.NVarChar, 50), new SqlParameter("@ExpressCompanyName", SqlDbType.NVarChar, 500), new SqlParameter("@ExpressCompanyAbb", SqlDbType.NVarChar, 500), 
                new SqlParameter("@PaymentTypeId", SqlDbType.Int, 4), new SqlParameter("@PaymentTypeName", SqlDbType.NVarChar, 100), new SqlParameter("@PaymentGateway", SqlDbType.NVarChar, 50), new SqlParameter("@PaymentStatus", SqlDbType.SmallInt, 2), new SqlParameter("@RefundStatus", SqlDbType.SmallInt, 2), new SqlParameter("@PayCurrencyCode", SqlDbType.NVarChar, 20), new SqlParameter("@PayCurrencyName", SqlDbType.NVarChar, 20), new SqlParameter("@PaymentFee", SqlDbType.Money, 8), new SqlParameter("@PaymentFeeAdjusted", SqlDbType.Money, 8), new SqlParameter("@GatewayOrderId", SqlDbType.NVarChar, 100), new SqlParameter("@OrderTotal", SqlDbType.Money, 8), new SqlParameter("@OrderPoint", SqlDbType.Int, 4), new SqlParameter("@OrderCostPrice", SqlDbType.Money, 8), new SqlParameter("@OrderProfit", SqlDbType.Money, 8), new SqlParameter("@OrderOtherCost", SqlDbType.Money, 8), new SqlParameter("@OrderOptionPrice", SqlDbType.Money, 8), 
                new SqlParameter("@DiscountName", SqlDbType.NVarChar, 200), new SqlParameter("@DiscountAmount", SqlDbType.Money, 8), new SqlParameter("@DiscountAdjusted", SqlDbType.Money, 8), new SqlParameter("@DiscountValue", SqlDbType.Money, 8), new SqlParameter("@DiscountValueType", SqlDbType.SmallInt, 2), new SqlParameter("@CouponCode", SqlDbType.NVarChar, 50), new SqlParameter("@CouponName", SqlDbType.NVarChar, 100), new SqlParameter("@CouponAmount", SqlDbType.Money, 8), new SqlParameter("@CouponValue", SqlDbType.Money, 8), new SqlParameter("@CouponValueType", SqlDbType.SmallInt, 2), new SqlParameter("@ActivityName", SqlDbType.NVarChar, 200), new SqlParameter("@ActivityFreeAmount", SqlDbType.Money, 8), new SqlParameter("@ActivityStatus", SqlDbType.SmallInt, 2), new SqlParameter("@GroupBuyId", SqlDbType.Int, 4), new SqlParameter("@GroupBuyPrice", SqlDbType.Money, 8), new SqlParameter("@GroupBuyStatus", SqlDbType.SmallInt, 2), 
                new SqlParameter("@Amount", SqlDbType.Money, 8), new SqlParameter("@OrderType", SqlDbType.SmallInt, 2), new SqlParameter("@OrderStatus", SqlDbType.SmallInt, 2), new SqlParameter("@SellerID", SqlDbType.Int, 4), new SqlParameter("@SellerName", SqlDbType.NVarChar, 100), new SqlParameter("@SellerEmail", SqlDbType.NVarChar, 100), new SqlParameter("@SellerCellPhone", SqlDbType.NVarChar, 50), new SqlParameter("@CommentStatus", SqlDbType.SmallInt, 2), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@SupplierName", SqlDbType.NVarChar, 100), new SqlParameter("@ReferID", SqlDbType.NVarChar, 50), new SqlParameter("@ReferURL", SqlDbType.NVarChar, 200), new SqlParameter("@OrderIP", SqlDbType.NVarChar, 50), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@ProductTotal", SqlDbType.Money, 8), new SqlParameter("@HasChildren", SqlDbType.Bit, 1), 
                new SqlParameter("@IsReviews", SqlDbType.Bit, 1), new SqlParameter("@OrderId", SqlDbType.BigInt, 8)
             };
            cmdParms[0].Value = model.OrderCode;
            cmdParms[1].Value = model.ParentOrderId;
            cmdParms[2].Value = model.CreatedDate;
            cmdParms[3].Value = model.UpdatedDate;
            cmdParms[4].Value = model.BuyerID;
            cmdParms[5].Value = model.BuyerName;
            cmdParms[6].Value = model.BuyerEmail;
            cmdParms[7].Value = model.BuyerCellPhone;
            cmdParms[8].Value = model.RegionId;
            cmdParms[9].Value = model.ShipRegion;
            cmdParms[10].Value = model.ShipAddress;
            cmdParms[11].Value = model.ShipZipCode;
            cmdParms[12].Value = model.ShipName;
            cmdParms[13].Value = model.ShipTelPhone;
            cmdParms[14].Value = model.ShipCellPhone;
            cmdParms[15].Value = model.ShipEmail;
            cmdParms[0x10].Value = model.ShippingModeId;
            cmdParms[0x11].Value = model.ShippingModeName;
            cmdParms[0x12].Value = model.RealShippingModeId;
            cmdParms[0x13].Value = model.RealShippingModeName;
            cmdParms[20].Value = model.ShipperId;
            cmdParms[0x15].Value = model.ShipperName;
            cmdParms[0x16].Value = model.ShipperAddress;
            cmdParms[0x17].Value = model.ShipperCellPhone;
            cmdParms[0x18].Value = model.Freight;
            cmdParms[0x19].Value = model.FreightAdjusted;
            cmdParms[0x1a].Value = model.FreightActual;
            cmdParms[0x1b].Value = model.Weight;
            cmdParms[0x1c].Value = model.ShippingStatus;
            cmdParms[0x1d].Value = model.ShipOrderNumber;
            cmdParms[30].Value = model.ExpressCompanyName;
            cmdParms[0x1f].Value = model.ExpressCompanyAbb;
            cmdParms[0x20].Value = model.PaymentTypeId;
            cmdParms[0x21].Value = model.PaymentTypeName;
            cmdParms[0x22].Value = model.PaymentGateway;
            cmdParms[0x23].Value = model.PaymentStatus;
            cmdParms[0x24].Value = model.RefundStatus;
            cmdParms[0x25].Value = model.PayCurrencyCode;
            cmdParms[0x26].Value = model.PayCurrencyName;
            cmdParms[0x27].Value = model.PaymentFee;
            cmdParms[40].Value = model.PaymentFeeAdjusted;
            cmdParms[0x29].Value = model.GatewayOrderId;
            cmdParms[0x2a].Value = model.OrderTotal;
            cmdParms[0x2b].Value = model.OrderPoint;
            cmdParms[0x2c].Value = model.OrderCostPrice;
            cmdParms[0x2d].Value = model.OrderProfit;
            cmdParms[0x2e].Value = model.OrderOtherCost;
            cmdParms[0x2f].Value = model.OrderOptionPrice;
            cmdParms[0x30].Value = model.DiscountName;
            cmdParms[0x31].Value = model.DiscountAmount;
            cmdParms[50].Value = model.DiscountAdjusted;
            cmdParms[0x33].Value = model.DiscountValue;
            cmdParms[0x34].Value = model.DiscountValueType;
            cmdParms[0x35].Value = model.CouponCode;
            cmdParms[0x36].Value = model.CouponName;
            cmdParms[0x37].Value = model.CouponAmount;
            cmdParms[0x38].Value = model.CouponValue;
            cmdParms[0x39].Value = model.CouponValueType;
            cmdParms[0x3a].Value = model.ActivityName;
            cmdParms[0x3b].Value = model.ActivityFreeAmount;
            cmdParms[60].Value = model.ActivityStatus;
            cmdParms[0x3d].Value = model.GroupBuyId;
            cmdParms[0x3e].Value = model.GroupBuyPrice;
            cmdParms[0x3f].Value = model.GroupBuyStatus;
            cmdParms[0x40].Value = model.Amount;
            cmdParms[0x41].Value = model.OrderType;
            cmdParms[0x42].Value = model.OrderStatus;
            cmdParms[0x43].Value = model.SellerID;
            cmdParms[0x44].Value = model.SellerName;
            cmdParms[0x45].Value = model.SellerEmail;
            cmdParms[70].Value = model.SellerCellPhone;
            cmdParms[0x47].Value = model.CommentStatus;
            cmdParms[0x48].Value = model.SupplierId;
            cmdParms[0x49].Value = model.SupplierName;
            cmdParms[0x4a].Value = model.ReferID;
            cmdParms[0x4b].Value = model.ReferURL;
            cmdParms[0x4c].Value = model.OrderIP;
            cmdParms[0x4d].Value = model.Remark;
            cmdParms[0x4e].Value = model.ProductTotal;
            cmdParms[0x4f].Value = model.HasChildren;
            cmdParms[80].Value = model.IsReviews;
            cmdParms[0x51].Value = model.OrderId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateOrderStatus(long orderId, int status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Orders set ");
            builder.Append("OrderStatus=@OrderStatus,");
            builder.Append(" where OrderId=@OrderId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@OrderStatus", SqlDbType.SmallInt, 2), new SqlParameter("@OrderId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = status;
            cmdParms[1].Value = orderId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateShipped(OrderInfo model)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Orders set ");
            builder.Append("OrderCode=@OrderCode,");
            builder.Append("ParentOrderId=@ParentOrderId,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("UpdatedDate=@UpdatedDate,");
            builder.Append("BuyerID=@BuyerID,");
            builder.Append("BuyerName=@BuyerName,");
            builder.Append("BuyerEmail=@BuyerEmail,");
            builder.Append("BuyerCellPhone=@BuyerCellPhone,");
            builder.Append("RegionId=@RegionId,");
            builder.Append("ShipRegion=@ShipRegion,");
            builder.Append("ShipAddress=@ShipAddress,");
            builder.Append("ShipZipCode=@ShipZipCode,");
            builder.Append("ShipName=@ShipName,");
            builder.Append("ShipTelPhone=@ShipTelPhone,");
            builder.Append("ShipCellPhone=@ShipCellPhone,");
            builder.Append("ShipEmail=@ShipEmail,");
            builder.Append("ShippingModeId=@ShippingModeId,");
            builder.Append("ShippingModeName=@ShippingModeName,");
            builder.Append("RealShippingModeId=@RealShippingModeId,");
            builder.Append("RealShippingModeName=@RealShippingModeName,");
            builder.Append("ShipperId=@ShipperId,");
            builder.Append("ShipperName=@ShipperName,");
            builder.Append("ShipperAddress=@ShipperAddress,");
            builder.Append("ShipperCellPhone=@ShipperCellPhone,");
            builder.Append("Freight=@Freight,");
            builder.Append("FreightAdjusted=@FreightAdjusted,");
            builder.Append("FreightActual=@FreightActual,");
            builder.Append("Weight=@Weight,");
            builder.Append("ShippingStatus=@ShippingStatus,");
            builder.Append("ShipOrderNumber=@ShipOrderNumber,");
            builder.Append("ExpressCompanyName=@ExpressCompanyName,");
            builder.Append("ExpressCompanyAbb=@ExpressCompanyAbb,");
            builder.Append("PaymentTypeId=@PaymentTypeId,");
            builder.Append("PaymentTypeName=@PaymentTypeName,");
            builder.Append("PaymentGateway=@PaymentGateway,");
            builder.Append("PaymentStatus=@PaymentStatus,");
            builder.Append("RefundStatus=@RefundStatus,");
            builder.Append("PayCurrencyCode=@PayCurrencyCode,");
            builder.Append("PayCurrencyName=@PayCurrencyName,");
            builder.Append("PaymentFee=@PaymentFee,");
            builder.Append("PaymentFeeAdjusted=@PaymentFeeAdjusted,");
            builder.Append("GatewayOrderId=@GatewayOrderId,");
            builder.Append("OrderTotal=@OrderTotal,");
            builder.Append("OrderPoint=@OrderPoint,");
            builder.Append("OrderCostPrice=@OrderCostPrice,");
            builder.Append("OrderProfit=@OrderProfit,");
            builder.Append("OrderOtherCost=@OrderOtherCost,");
            builder.Append("OrderOptionPrice=@OrderOptionPrice,");
            builder.Append("DiscountName=@DiscountName,");
            builder.Append("DiscountAmount=@DiscountAmount,");
            builder.Append("DiscountAdjusted=@DiscountAdjusted,");
            builder.Append("DiscountValue=@DiscountValue,");
            builder.Append("DiscountValueType=@DiscountValueType,");
            builder.Append("CouponCode=@CouponCode,");
            builder.Append("CouponName=@CouponName,");
            builder.Append("CouponAmount=@CouponAmount,");
            builder.Append("CouponValue=@CouponValue,");
            builder.Append("CouponValueType=@CouponValueType,");
            builder.Append("ActivityName=@ActivityName,");
            builder.Append("ActivityFreeAmount=@ActivityFreeAmount,");
            builder.Append("ActivityStatus=@ActivityStatus,");
            builder.Append("GroupBuyId=@GroupBuyId,");
            builder.Append("GroupBuyPrice=@GroupBuyPrice,");
            builder.Append("GroupBuyStatus=@GroupBuyStatus,");
            builder.Append("Amount=@Amount,");
            builder.Append("OrderType=@OrderType,");
            builder.Append("OrderStatus=@OrderStatus,");
            builder.Append("SellerID=@SellerID,");
            builder.Append("SellerName=@SellerName,");
            builder.Append("SellerEmail=@SellerEmail,");
            builder.Append("SellerCellPhone=@SellerCellPhone,");
            builder.Append("SupplierId=@SupplierId,");
            builder.Append("SupplierName=@SupplierName,");
            builder.Append("ReferID=@ReferID,");
            builder.Append("ReferURL=@ReferURL,");
            builder.Append("OrderIP=@OrderIP,");
            builder.Append("Remark=@Remark,");
            builder.Append("CommentStatus=@CommentStatus");
            builder.Append(" where OrderId=@OrderId");
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50), new SqlParameter("@ParentOrderId", SqlDbType.BigInt, 8), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@UpdatedDate", SqlDbType.DateTime), new SqlParameter("@BuyerID", SqlDbType.Int, 4), new SqlParameter("@BuyerName", SqlDbType.NVarChar, 100), new SqlParameter("@BuyerEmail", SqlDbType.NVarChar, 100), new SqlParameter("@BuyerCellPhone", SqlDbType.NVarChar, 50), new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@ShipRegion", SqlDbType.NVarChar, 300), new SqlParameter("@ShipAddress", SqlDbType.NVarChar, 300), new SqlParameter("@ShipZipCode", SqlDbType.NVarChar, 20), new SqlParameter("@ShipName", SqlDbType.NVarChar, 50), new SqlParameter("@ShipTelPhone", SqlDbType.NVarChar, 50), new SqlParameter("@ShipCellPhone", SqlDbType.NVarChar, 50), new SqlParameter("@ShipEmail", SqlDbType.NVarChar, 100), 
                new SqlParameter("@ShippingModeId", SqlDbType.Int, 4), new SqlParameter("@ShippingModeName", SqlDbType.NVarChar, 100), new SqlParameter("@RealShippingModeId", SqlDbType.Int, 4), new SqlParameter("@RealShippingModeName", SqlDbType.NVarChar, 100), new SqlParameter("@ShipperId", SqlDbType.Int, 4), new SqlParameter("@ShipperName", SqlDbType.NVarChar, 100), new SqlParameter("@ShipperAddress", SqlDbType.NVarChar, 300), new SqlParameter("@ShipperCellPhone", SqlDbType.NVarChar, 20), new SqlParameter("@Freight", SqlDbType.Money, 8), new SqlParameter("@FreightAdjusted", SqlDbType.Money, 8), new SqlParameter("@FreightActual", SqlDbType.Money, 8), new SqlParameter("@Weight", SqlDbType.Int, 4), new SqlParameter("@ShippingStatus", SqlDbType.SmallInt, 2), new SqlParameter("@ShipOrderNumber", SqlDbType.NVarChar, 50), new SqlParameter("@ExpressCompanyName", SqlDbType.NVarChar, 500), new SqlParameter("@ExpressCompanyAbb", SqlDbType.NVarChar, 500), 
                new SqlParameter("@PaymentTypeId", SqlDbType.Int, 4), new SqlParameter("@PaymentTypeName", SqlDbType.NVarChar, 100), new SqlParameter("@PaymentGateway", SqlDbType.NVarChar, 50), new SqlParameter("@PaymentStatus", SqlDbType.SmallInt, 2), new SqlParameter("@RefundStatus", SqlDbType.SmallInt, 2), new SqlParameter("@PayCurrencyCode", SqlDbType.NVarChar, 20), new SqlParameter("@PayCurrencyName", SqlDbType.NVarChar, 20), new SqlParameter("@PaymentFee", SqlDbType.Money, 8), new SqlParameter("@PaymentFeeAdjusted", SqlDbType.Money, 8), new SqlParameter("@GatewayOrderId", SqlDbType.NVarChar, 100), new SqlParameter("@OrderTotal", SqlDbType.Money, 8), new SqlParameter("@OrderPoint", SqlDbType.Int, 4), new SqlParameter("@OrderCostPrice", SqlDbType.Money, 8), new SqlParameter("@OrderProfit", SqlDbType.Money, 8), new SqlParameter("@OrderOtherCost", SqlDbType.Money, 8), new SqlParameter("@OrderOptionPrice", SqlDbType.Money, 8), 
                new SqlParameter("@DiscountName", SqlDbType.NVarChar, 200), new SqlParameter("@DiscountAmount", SqlDbType.Money, 8), new SqlParameter("@DiscountAdjusted", SqlDbType.Money, 8), new SqlParameter("@DiscountValue", SqlDbType.Money, 8), new SqlParameter("@DiscountValueType", SqlDbType.SmallInt, 2), new SqlParameter("@CouponCode", SqlDbType.NVarChar, 50), new SqlParameter("@CouponName", SqlDbType.NVarChar, 100), new SqlParameter("@CouponAmount", SqlDbType.Money, 8), new SqlParameter("@CouponValue", SqlDbType.Money, 8), new SqlParameter("@CouponValueType", SqlDbType.SmallInt, 2), new SqlParameter("@ActivityName", SqlDbType.NVarChar, 200), new SqlParameter("@ActivityFreeAmount", SqlDbType.Money, 8), new SqlParameter("@ActivityStatus", SqlDbType.SmallInt, 2), new SqlParameter("@GroupBuyId", SqlDbType.Int, 4), new SqlParameter("@GroupBuyPrice", SqlDbType.Money, 8), new SqlParameter("@GroupBuyStatus", SqlDbType.SmallInt, 2), 
                new SqlParameter("@Amount", SqlDbType.Money, 8), new SqlParameter("@OrderType", SqlDbType.SmallInt, 2), new SqlParameter("@OrderStatus", SqlDbType.SmallInt, 2), new SqlParameter("@SellerID", SqlDbType.Int, 4), new SqlParameter("@SellerName", SqlDbType.NVarChar, 100), new SqlParameter("@SellerEmail", SqlDbType.NVarChar, 100), new SqlParameter("@SellerCellPhone", SqlDbType.NVarChar, 50), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@SupplierName", SqlDbType.NVarChar, 100), new SqlParameter("@ReferID", SqlDbType.NVarChar, 50), new SqlParameter("@ReferURL", SqlDbType.NVarChar, 200), new SqlParameter("@OrderIP", SqlDbType.NVarChar, 50), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@CommentStatus", SqlDbType.SmallInt, 2), new SqlParameter("@OrderId", SqlDbType.BigInt, 8)
             };
            para[0].Value = model.OrderCode;
            para[1].Value = model.ParentOrderId;
            para[2].Value = model.CreatedDate;
            para[3].Value = model.UpdatedDate;
            para[4].Value = model.BuyerID;
            para[5].Value = model.BuyerName;
            para[6].Value = model.BuyerEmail;
            para[7].Value = model.BuyerCellPhone;
            para[8].Value = model.RegionId;
            para[9].Value = model.ShipRegion;
            para[10].Value = model.ShipAddress;
            para[11].Value = model.ShipZipCode;
            para[12].Value = model.ShipName;
            para[13].Value = model.ShipTelPhone;
            para[14].Value = model.ShipCellPhone;
            para[15].Value = model.ShipEmail;
            para[0x10].Value = model.ShippingModeId;
            para[0x11].Value = model.ShippingModeName;
            para[0x12].Value = model.RealShippingModeId;
            para[0x13].Value = model.RealShippingModeName;
            para[20].Value = model.ShipperId;
            para[0x15].Value = model.ShipperName;
            para[0x16].Value = model.ShipperAddress;
            para[0x17].Value = model.ShipperCellPhone;
            para[0x18].Value = model.Freight;
            para[0x19].Value = model.FreightAdjusted;
            para[0x1a].Value = model.FreightActual;
            para[0x1b].Value = model.Weight;
            para[0x1c].Value = model.ShippingStatus;
            para[0x1d].Value = model.ShipOrderNumber;
            para[30].Value = model.ExpressCompanyName;
            para[0x1f].Value = model.ExpressCompanyAbb;
            para[0x20].Value = model.PaymentTypeId;
            para[0x21].Value = model.PaymentTypeName;
            para[0x22].Value = model.PaymentGateway;
            para[0x23].Value = model.PaymentStatus;
            para[0x24].Value = model.RefundStatus;
            para[0x25].Value = model.PayCurrencyCode;
            para[0x26].Value = model.PayCurrencyName;
            para[0x27].Value = model.PaymentFee;
            para[40].Value = model.PaymentFeeAdjusted;
            para[0x29].Value = model.GatewayOrderId;
            para[0x2a].Value = model.OrderTotal;
            para[0x2b].Value = model.OrderPoint;
            para[0x2c].Value = model.OrderCostPrice;
            para[0x2d].Value = model.OrderProfit;
            para[0x2e].Value = model.OrderOtherCost;
            para[0x2f].Value = model.OrderOptionPrice;
            para[0x30].Value = model.DiscountName;
            para[0x31].Value = model.DiscountAmount;
            para[50].Value = model.DiscountAdjusted;
            para[0x33].Value = model.DiscountValue;
            para[0x34].Value = model.DiscountValueType;
            para[0x35].Value = model.CouponCode;
            para[0x36].Value = model.CouponName;
            para[0x37].Value = model.CouponAmount;
            para[0x38].Value = model.CouponValue;
            para[0x39].Value = model.CouponValueType;
            para[0x3a].Value = model.ActivityName;
            para[0x3b].Value = model.ActivityFreeAmount;
            para[60].Value = model.ActivityStatus;
            para[0x3d].Value = model.GroupBuyId;
            para[0x3e].Value = model.GroupBuyPrice;
            para[0x3f].Value = model.GroupBuyStatus;
            para[0x40].Value = model.Amount;
            para[0x41].Value = model.OrderType;
            para[0x42].Value = model.OrderStatus;
            para[0x43].Value = model.SellerID;
            para[0x44].Value = model.SellerName;
            para[0x45].Value = model.SellerEmail;
            para[70].Value = model.SellerCellPhone;
            para[0x47].Value = model.SupplierId;
            para[0x48].Value = model.SupplierName;
            para[0x49].Value = model.ReferID;
            para[0x4a].Value = model.ReferURL;
            para[0x4b].Value = model.OrderIP;
            para[0x4c].Value = model.Remark;
            para[0x4d].Value = model.CommentStatus;
            para[0x4e].Value = model.OrderId;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("UPDATE Shop_OrderItems SET ShipmentQuantity=Quantity WHERE OrderId =@OrderId ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@OrderId", SqlDbType.BigInt, 8) };
            parameterArray2[0].Value = model.OrderId;
            item = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(item);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }
    }
}

