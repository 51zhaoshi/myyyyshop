namespace Maticsoft.SQLServerDAL.Shop.Order
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Order;
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class OrdersHistory : IOrdersHistory
    {
        public bool Add(Maticsoft.Model.Shop.Order.OrdersHistory model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_OrdersHistory(");
            builder.Append("OrderId,OrderCode,ParentOrderId,CreatedDate,UpdatedDate,BuyerID,BuyerName,BuyerEmail,BuyerCellPhone,RegionId,ShipRegion,ShipAddress,ShipZipCode,ShipName,ShipTelPhone,ShipCellPhone,ShipEmail,ShippingModeId,ShippingModeName,RealShippingModeId,RealShippingModeName,ShipperId,ShipperName,ShipperAddress,ShipperCellPhone,Freight,FreightAdjusted,FreightActual,Weight,ShippingStatus,ShipOrderNumber,ExpressCompanyName,ExpressCompanyAbb,PaymentTypeId,PaymentTypeName,PaymentGateway,PaymentStatus,RefundStatus,PayCurrencyCode,PayCurrencyName,PaymentFee,PaymentFeeAdjusted,GatewayOrderId,OrderTotal,OrderPoint,OrderCostPrice,OrderProfit,OrderOtherCost,OrderOptionPrice,DiscountName,DiscountAmount,DiscountAdjusted,DiscountValue,DiscountValueType,CouponCode,CouponName,CouponAmount,CouponValue,CouponValueType,ActivityName,ActivityFreeAmount,ActivityStatus,GroupBuyId,GroupBuyPrice,GroupBuyStatus,Amount,OrderType,OrderStatus,SellerID,SellerName,SellerEmail,SellerCellPhone,SupplierId,SupplierName,ReferID,ReferURL,OrderIP,Remark)");
            builder.Append(" values (");
            builder.Append("@OrderId,@OrderCode,@ParentOrderId,@CreatedDate,@UpdatedDate,@BuyerID,@BuyerName,@BuyerEmail,@BuyerCellPhone,@RegionId,@ShipRegion,@ShipAddress,@ShipZipCode,@ShipName,@ShipTelPhone,@ShipCellPhone,@ShipEmail,@ShippingModeId,@ShippingModeName,@RealShippingModeId,@RealShippingModeName,@ShipperId,@ShipperName,@ShipperAddress,@ShipperCellPhone,@Freight,@FreightAdjusted,@FreightActual,@Weight,@ShippingStatus,@ShipOrderNumber,@ExpressCompanyName,@ExpressCompanyAbb,@PaymentTypeId,@PaymentTypeName,@PaymentGateway,@PaymentStatus,@RefundStatus,@PayCurrencyCode,@PayCurrencyName,@PaymentFee,@PaymentFeeAdjusted,@GatewayOrderId,@OrderTotal,@OrderPoint,@OrderCostPrice,@OrderProfit,@OrderOtherCost,@OrderOptionPrice,@DiscountName,@DiscountAmount,@DiscountAdjusted,@DiscountValue,@DiscountValueType,@CouponCode,@CouponName,@CouponAmount,@CouponValue,@CouponValueType,@ActivityName,@ActivityFreeAmount,@ActivityStatus,@GroupBuyId,@GroupBuyPrice,@GroupBuyStatus,@Amount,@OrderType,@OrderStatus,@SellerID,@SellerName,@SellerEmail,@SellerCellPhone,@SupplierId,@SupplierName,@ReferID,@ReferURL,@OrderIP,@Remark)");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@OrderId", SqlDbType.BigInt, 8), new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50), new SqlParameter("@ParentOrderId", SqlDbType.BigInt, 8), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@UpdatedDate", SqlDbType.DateTime), new SqlParameter("@BuyerID", SqlDbType.Int, 4), new SqlParameter("@BuyerName", SqlDbType.NVarChar, 100), new SqlParameter("@BuyerEmail", SqlDbType.NVarChar, 100), new SqlParameter("@BuyerCellPhone", SqlDbType.NVarChar, 50), new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@ShipRegion", SqlDbType.NVarChar, 300), new SqlParameter("@ShipAddress", SqlDbType.NVarChar, 300), new SqlParameter("@ShipZipCode", SqlDbType.NVarChar, 20), new SqlParameter("@ShipName", SqlDbType.NVarChar, 50), new SqlParameter("@ShipTelPhone", SqlDbType.NVarChar, 50), new SqlParameter("@ShipCellPhone", SqlDbType.NVarChar, 50), 
                new SqlParameter("@ShipEmail", SqlDbType.NVarChar, 100), new SqlParameter("@ShippingModeId", SqlDbType.Int, 4), new SqlParameter("@ShippingModeName", SqlDbType.NVarChar, 100), new SqlParameter("@RealShippingModeId", SqlDbType.Int, 4), new SqlParameter("@RealShippingModeName", SqlDbType.NVarChar, 100), new SqlParameter("@ShipperId", SqlDbType.Int, 4), new SqlParameter("@ShipperName", SqlDbType.NVarChar, 100), new SqlParameter("@ShipperAddress", SqlDbType.NVarChar, 300), new SqlParameter("@ShipperCellPhone", SqlDbType.NVarChar, 20), new SqlParameter("@Freight", SqlDbType.Money, 8), new SqlParameter("@FreightAdjusted", SqlDbType.Money, 8), new SqlParameter("@FreightActual", SqlDbType.Money, 8), new SqlParameter("@Weight", SqlDbType.Int, 4), new SqlParameter("@ShippingStatus", SqlDbType.SmallInt, 2), new SqlParameter("@ShipOrderNumber", SqlDbType.NVarChar, 50), new SqlParameter("@ExpressCompanyName", SqlDbType.NVarChar, 500), 
                new SqlParameter("@ExpressCompanyAbb", SqlDbType.NVarChar, 500), new SqlParameter("@PaymentTypeId", SqlDbType.Int, 4), new SqlParameter("@PaymentTypeName", SqlDbType.NVarChar, 100), new SqlParameter("@PaymentGateway", SqlDbType.NVarChar, 50), new SqlParameter("@PaymentStatus", SqlDbType.SmallInt, 2), new SqlParameter("@RefundStatus", SqlDbType.SmallInt, 2), new SqlParameter("@PayCurrencyCode", SqlDbType.NVarChar, 20), new SqlParameter("@PayCurrencyName", SqlDbType.NVarChar, 20), new SqlParameter("@PaymentFee", SqlDbType.Money, 8), new SqlParameter("@PaymentFeeAdjusted", SqlDbType.Money, 8), new SqlParameter("@GatewayOrderId", SqlDbType.NVarChar, 100), new SqlParameter("@OrderTotal", SqlDbType.Money, 8), new SqlParameter("@OrderPoint", SqlDbType.Int, 4), new SqlParameter("@OrderCostPrice", SqlDbType.Money, 8), new SqlParameter("@OrderProfit", SqlDbType.Money, 8), new SqlParameter("@OrderOtherCost", SqlDbType.Money, 8), 
                new SqlParameter("@OrderOptionPrice", SqlDbType.Money, 8), new SqlParameter("@DiscountName", SqlDbType.NVarChar, 200), new SqlParameter("@DiscountAmount", SqlDbType.Money, 8), new SqlParameter("@DiscountAdjusted", SqlDbType.Money, 8), new SqlParameter("@DiscountValue", SqlDbType.Money, 8), new SqlParameter("@DiscountValueType", SqlDbType.SmallInt, 2), new SqlParameter("@CouponCode", SqlDbType.NVarChar, 50), new SqlParameter("@CouponName", SqlDbType.NVarChar, 100), new SqlParameter("@CouponAmount", SqlDbType.Money, 8), new SqlParameter("@CouponValue", SqlDbType.Money, 8), new SqlParameter("@CouponValueType", SqlDbType.SmallInt, 2), new SqlParameter("@ActivityName", SqlDbType.NVarChar, 200), new SqlParameter("@ActivityFreeAmount", SqlDbType.Money, 8), new SqlParameter("@ActivityStatus", SqlDbType.SmallInt, 2), new SqlParameter("@GroupBuyId", SqlDbType.Int, 4), new SqlParameter("@GroupBuyPrice", SqlDbType.Money, 8), 
                new SqlParameter("@GroupBuyStatus", SqlDbType.SmallInt, 2), new SqlParameter("@Amount", SqlDbType.Money, 8), new SqlParameter("@OrderType", SqlDbType.SmallInt, 2), new SqlParameter("@OrderStatus", SqlDbType.SmallInt, 2), new SqlParameter("@SellerID", SqlDbType.Int, 4), new SqlParameter("@SellerName", SqlDbType.NVarChar, 100), new SqlParameter("@SellerEmail", SqlDbType.NVarChar, 100), new SqlParameter("@SellerCellPhone", SqlDbType.NVarChar, 50), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@SupplierName", SqlDbType.NVarChar, 100), new SqlParameter("@ReferID", SqlDbType.NVarChar, 50), new SqlParameter("@ReferURL", SqlDbType.NVarChar, 200), new SqlParameter("@OrderIP", SqlDbType.NVarChar, 50), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x7d0)
             };
            cmdParms[0].Value = model.OrderId;
            cmdParms[1].Value = model.OrderCode;
            cmdParms[2].Value = model.ParentOrderId;
            cmdParms[3].Value = model.CreatedDate;
            cmdParms[4].Value = model.UpdatedDate;
            cmdParms[5].Value = model.BuyerID;
            cmdParms[6].Value = model.BuyerName;
            cmdParms[7].Value = model.BuyerEmail;
            cmdParms[8].Value = model.BuyerCellPhone;
            cmdParms[9].Value = model.RegionId;
            cmdParms[10].Value = model.ShipRegion;
            cmdParms[11].Value = model.ShipAddress;
            cmdParms[12].Value = model.ShipZipCode;
            cmdParms[13].Value = model.ShipName;
            cmdParms[14].Value = model.ShipTelPhone;
            cmdParms[15].Value = model.ShipCellPhone;
            cmdParms[0x10].Value = model.ShipEmail;
            cmdParms[0x11].Value = model.ShippingModeId;
            cmdParms[0x12].Value = model.ShippingModeName;
            cmdParms[0x13].Value = model.RealShippingModeId;
            cmdParms[20].Value = model.RealShippingModeName;
            cmdParms[0x15].Value = model.ShipperId;
            cmdParms[0x16].Value = model.ShipperName;
            cmdParms[0x17].Value = model.ShipperAddress;
            cmdParms[0x18].Value = model.ShipperCellPhone;
            cmdParms[0x19].Value = model.Freight;
            cmdParms[0x1a].Value = model.FreightAdjusted;
            cmdParms[0x1b].Value = model.FreightActual;
            cmdParms[0x1c].Value = model.Weight;
            cmdParms[0x1d].Value = model.ShippingStatus;
            cmdParms[30].Value = model.ShipOrderNumber;
            cmdParms[0x1f].Value = model.ExpressCompanyName;
            cmdParms[0x20].Value = model.ExpressCompanyAbb;
            cmdParms[0x21].Value = model.PaymentTypeId;
            cmdParms[0x22].Value = model.PaymentTypeName;
            cmdParms[0x23].Value = model.PaymentGateway;
            cmdParms[0x24].Value = model.PaymentStatus;
            cmdParms[0x25].Value = model.RefundStatus;
            cmdParms[0x26].Value = model.PayCurrencyCode;
            cmdParms[0x27].Value = model.PayCurrencyName;
            cmdParms[40].Value = model.PaymentFee;
            cmdParms[0x29].Value = model.PaymentFeeAdjusted;
            cmdParms[0x2a].Value = model.GatewayOrderId;
            cmdParms[0x2b].Value = model.OrderTotal;
            cmdParms[0x2c].Value = model.OrderPoint;
            cmdParms[0x2d].Value = model.OrderCostPrice;
            cmdParms[0x2e].Value = model.OrderProfit;
            cmdParms[0x2f].Value = model.OrderOtherCost;
            cmdParms[0x30].Value = model.OrderOptionPrice;
            cmdParms[0x31].Value = model.DiscountName;
            cmdParms[50].Value = model.DiscountAmount;
            cmdParms[0x33].Value = model.DiscountAdjusted;
            cmdParms[0x34].Value = model.DiscountValue;
            cmdParms[0x35].Value = model.DiscountValueType;
            cmdParms[0x36].Value = model.CouponCode;
            cmdParms[0x37].Value = model.CouponName;
            cmdParms[0x38].Value = model.CouponAmount;
            cmdParms[0x39].Value = model.CouponValue;
            cmdParms[0x3a].Value = model.CouponValueType;
            cmdParms[0x3b].Value = model.ActivityName;
            cmdParms[60].Value = model.ActivityFreeAmount;
            cmdParms[0x3d].Value = model.ActivityStatus;
            cmdParms[0x3e].Value = model.GroupBuyId;
            cmdParms[0x3f].Value = model.GroupBuyPrice;
            cmdParms[0x40].Value = model.GroupBuyStatus;
            cmdParms[0x41].Value = model.Amount;
            cmdParms[0x42].Value = model.OrderType;
            cmdParms[0x43].Value = model.OrderStatus;
            cmdParms[0x44].Value = model.SellerID;
            cmdParms[0x45].Value = model.SellerName;
            cmdParms[70].Value = model.SellerEmail;
            cmdParms[0x47].Value = model.SellerCellPhone;
            cmdParms[0x48].Value = model.SupplierId;
            cmdParms[0x49].Value = model.SupplierName;
            cmdParms[0x4a].Value = model.ReferID;
            cmdParms[0x4b].Value = model.ReferURL;
            cmdParms[0x4c].Value = model.OrderIP;
            cmdParms[0x4d].Value = model.Remark;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Maticsoft.Model.Shop.Order.OrdersHistory DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Order.OrdersHistory history = new Maticsoft.Model.Shop.Order.OrdersHistory();
            if (row != null)
            {
                if ((row["OrderId"] != null) && (row["OrderId"].ToString() != ""))
                {
                    history.OrderId = long.Parse(row["OrderId"].ToString());
                }
                if (row["OrderCode"] != null)
                {
                    history.OrderCode = row["OrderCode"].ToString();
                }
                if ((row["ParentOrderId"] != null) && (row["ParentOrderId"].ToString() != ""))
                {
                    history.ParentOrderId = long.Parse(row["ParentOrderId"].ToString());
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    history.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["UpdatedDate"] != null) && (row["UpdatedDate"].ToString() != ""))
                {
                    history.UpdatedDate = new DateTime?(DateTime.Parse(row["UpdatedDate"].ToString()));
                }
                if ((row["BuyerID"] != null) && (row["BuyerID"].ToString() != ""))
                {
                    history.BuyerID = int.Parse(row["BuyerID"].ToString());
                }
                if (row["BuyerName"] != null)
                {
                    history.BuyerName = row["BuyerName"].ToString();
                }
                if (row["BuyerEmail"] != null)
                {
                    history.BuyerEmail = row["BuyerEmail"].ToString();
                }
                if (row["BuyerCellPhone"] != null)
                {
                    history.BuyerCellPhone = row["BuyerCellPhone"].ToString();
                }
                if ((row["RegionId"] != null) && (row["RegionId"].ToString() != ""))
                {
                    history.RegionId = new int?(int.Parse(row["RegionId"].ToString()));
                }
                if (row["ShipRegion"] != null)
                {
                    history.ShipRegion = row["ShipRegion"].ToString();
                }
                if (row["ShipAddress"] != null)
                {
                    history.ShipAddress = row["ShipAddress"].ToString();
                }
                if (row["ShipZipCode"] != null)
                {
                    history.ShipZipCode = row["ShipZipCode"].ToString();
                }
                if (row["ShipName"] != null)
                {
                    history.ShipName = row["ShipName"].ToString();
                }
                if (row["ShipTelPhone"] != null)
                {
                    history.ShipTelPhone = row["ShipTelPhone"].ToString();
                }
                if (row["ShipCellPhone"] != null)
                {
                    history.ShipCellPhone = row["ShipCellPhone"].ToString();
                }
                if (row["ShipEmail"] != null)
                {
                    history.ShipEmail = row["ShipEmail"].ToString();
                }
                if ((row["ShippingModeId"] != null) && (row["ShippingModeId"].ToString() != ""))
                {
                    history.ShippingModeId = new int?(int.Parse(row["ShippingModeId"].ToString()));
                }
                if (row["ShippingModeName"] != null)
                {
                    history.ShippingModeName = row["ShippingModeName"].ToString();
                }
                if ((row["RealShippingModeId"] != null) && (row["RealShippingModeId"].ToString() != ""))
                {
                    history.RealShippingModeId = new int?(int.Parse(row["RealShippingModeId"].ToString()));
                }
                if (row["RealShippingModeName"] != null)
                {
                    history.RealShippingModeName = row["RealShippingModeName"].ToString();
                }
                if ((row["ShipperId"] != null) && (row["ShipperId"].ToString() != ""))
                {
                    history.ShipperId = new int?(int.Parse(row["ShipperId"].ToString()));
                }
                if (row["ShipperName"] != null)
                {
                    history.ShipperName = row["ShipperName"].ToString();
                }
                if (row["ShipperAddress"] != null)
                {
                    history.ShipperAddress = row["ShipperAddress"].ToString();
                }
                if (row["ShipperCellPhone"] != null)
                {
                    history.ShipperCellPhone = row["ShipperCellPhone"].ToString();
                }
                if ((row["Freight"] != null) && (row["Freight"].ToString() != ""))
                {
                    history.Freight = new decimal?(decimal.Parse(row["Freight"].ToString()));
                }
                if ((row["FreightAdjusted"] != null) && (row["FreightAdjusted"].ToString() != ""))
                {
                    history.FreightAdjusted = new decimal?(decimal.Parse(row["FreightAdjusted"].ToString()));
                }
                if ((row["FreightActual"] != null) && (row["FreightActual"].ToString() != ""))
                {
                    history.FreightActual = new decimal?(decimal.Parse(row["FreightActual"].ToString()));
                }
                if ((row["Weight"] != null) && (row["Weight"].ToString() != ""))
                {
                    history.Weight = new int?(int.Parse(row["Weight"].ToString()));
                }
                if ((row["ShippingStatus"] != null) && (row["ShippingStatus"].ToString() != ""))
                {
                    history.ShippingStatus = int.Parse(row["ShippingStatus"].ToString());
                }
                if (row["ShipOrderNumber"] != null)
                {
                    history.ShipOrderNumber = row["ShipOrderNumber"].ToString();
                }
                if (row["ExpressCompanyName"] != null)
                {
                    history.ExpressCompanyName = row["ExpressCompanyName"].ToString();
                }
                if (row["ExpressCompanyAbb"] != null)
                {
                    history.ExpressCompanyAbb = row["ExpressCompanyAbb"].ToString();
                }
                if ((row["PaymentTypeId"] != null) && (row["PaymentTypeId"].ToString() != ""))
                {
                    history.PaymentTypeId = int.Parse(row["PaymentTypeId"].ToString());
                }
                if (row["PaymentTypeName"] != null)
                {
                    history.PaymentTypeName = row["PaymentTypeName"].ToString();
                }
                if (row["PaymentGateway"] != null)
                {
                    history.PaymentGateway = row["PaymentGateway"].ToString();
                }
                if ((row["PaymentStatus"] != null) && (row["PaymentStatus"].ToString() != ""))
                {
                    history.PaymentStatus = int.Parse(row["PaymentStatus"].ToString());
                }
                if ((row["RefundStatus"] != null) && (row["RefundStatus"].ToString() != ""))
                {
                    history.RefundStatus = int.Parse(row["RefundStatus"].ToString());
                }
                if (row["PayCurrencyCode"] != null)
                {
                    history.PayCurrencyCode = row["PayCurrencyCode"].ToString();
                }
                if (row["PayCurrencyName"] != null)
                {
                    history.PayCurrencyName = row["PayCurrencyName"].ToString();
                }
                if ((row["PaymentFee"] != null) && (row["PaymentFee"].ToString() != ""))
                {
                    history.PaymentFee = new decimal?(decimal.Parse(row["PaymentFee"].ToString()));
                }
                if ((row["PaymentFeeAdjusted"] != null) && (row["PaymentFeeAdjusted"].ToString() != ""))
                {
                    history.PaymentFeeAdjusted = new decimal?(decimal.Parse(row["PaymentFeeAdjusted"].ToString()));
                }
                if (row["GatewayOrderId"] != null)
                {
                    history.GatewayOrderId = row["GatewayOrderId"].ToString();
                }
                if ((row["OrderTotal"] != null) && (row["OrderTotal"].ToString() != ""))
                {
                    history.OrderTotal = decimal.Parse(row["OrderTotal"].ToString());
                }
                if ((row["OrderPoint"] != null) && (row["OrderPoint"].ToString() != ""))
                {
                    history.OrderPoint = int.Parse(row["OrderPoint"].ToString());
                }
                if ((row["OrderCostPrice"] != null) && (row["OrderCostPrice"].ToString() != ""))
                {
                    history.OrderCostPrice = new decimal?(decimal.Parse(row["OrderCostPrice"].ToString()));
                }
                if ((row["OrderProfit"] != null) && (row["OrderProfit"].ToString() != ""))
                {
                    history.OrderProfit = new decimal?(decimal.Parse(row["OrderProfit"].ToString()));
                }
                if ((row["OrderOtherCost"] != null) && (row["OrderOtherCost"].ToString() != ""))
                {
                    history.OrderOtherCost = new decimal?(decimal.Parse(row["OrderOtherCost"].ToString()));
                }
                if ((row["OrderOptionPrice"] != null) && (row["OrderOptionPrice"].ToString() != ""))
                {
                    history.OrderOptionPrice = new decimal?(decimal.Parse(row["OrderOptionPrice"].ToString()));
                }
                if (row["DiscountName"] != null)
                {
                    history.DiscountName = row["DiscountName"].ToString();
                }
                if ((row["DiscountAmount"] != null) && (row["DiscountAmount"].ToString() != ""))
                {
                    history.DiscountAmount = new decimal?(decimal.Parse(row["DiscountAmount"].ToString()));
                }
                if ((row["DiscountAdjusted"] != null) && (row["DiscountAdjusted"].ToString() != ""))
                {
                    history.DiscountAdjusted = new decimal?(decimal.Parse(row["DiscountAdjusted"].ToString()));
                }
                if ((row["DiscountValue"] != null) && (row["DiscountValue"].ToString() != ""))
                {
                    history.DiscountValue = new decimal?(decimal.Parse(row["DiscountValue"].ToString()));
                }
                if ((row["DiscountValueType"] != null) && (row["DiscountValueType"].ToString() != ""))
                {
                    history.DiscountValueType = new int?(int.Parse(row["DiscountValueType"].ToString()));
                }
                if (row["CouponCode"] != null)
                {
                    history.CouponCode = row["CouponCode"].ToString();
                }
                if (row["CouponName"] != null)
                {
                    history.CouponName = row["CouponName"].ToString();
                }
                if ((row["CouponAmount"] != null) && (row["CouponAmount"].ToString() != ""))
                {
                    history.CouponAmount = new decimal?(decimal.Parse(row["CouponAmount"].ToString()));
                }
                if ((row["CouponValue"] != null) && (row["CouponValue"].ToString() != ""))
                {
                    history.CouponValue = new decimal?(decimal.Parse(row["CouponValue"].ToString()));
                }
                if ((row["CouponValueType"] != null) && (row["CouponValueType"].ToString() != ""))
                {
                    history.CouponValueType = new int?(int.Parse(row["CouponValueType"].ToString()));
                }
                if (row["ActivityName"] != null)
                {
                    history.ActivityName = row["ActivityName"].ToString();
                }
                if ((row["ActivityFreeAmount"] != null) && (row["ActivityFreeAmount"].ToString() != ""))
                {
                    history.ActivityFreeAmount = new decimal?(decimal.Parse(row["ActivityFreeAmount"].ToString()));
                }
                if ((row["ActivityStatus"] != null) && (row["ActivityStatus"].ToString() != ""))
                {
                    history.ActivityStatus = int.Parse(row["ActivityStatus"].ToString());
                }
                if ((row["GroupBuyId"] != null) && (row["GroupBuyId"].ToString() != ""))
                {
                    history.GroupBuyId = new int?(int.Parse(row["GroupBuyId"].ToString()));
                }
                if ((row["GroupBuyPrice"] != null) && (row["GroupBuyPrice"].ToString() != ""))
                {
                    history.GroupBuyPrice = new decimal?(decimal.Parse(row["GroupBuyPrice"].ToString()));
                }
                if ((row["GroupBuyStatus"] != null) && (row["GroupBuyStatus"].ToString() != ""))
                {
                    history.GroupBuyStatus = int.Parse(row["GroupBuyStatus"].ToString());
                }
                if ((row["Amount"] != null) && (row["Amount"].ToString() != ""))
                {
                    history.Amount = decimal.Parse(row["Amount"].ToString());
                }
                if ((row["OrderType"] != null) && (row["OrderType"].ToString() != ""))
                {
                    history.OrderType = int.Parse(row["OrderType"].ToString());
                }
                if ((row["OrderStatus"] != null) && (row["OrderStatus"].ToString() != ""))
                {
                    history.OrderStatus = int.Parse(row["OrderStatus"].ToString());
                }
                if ((row["SellerID"] != null) && (row["SellerID"].ToString() != ""))
                {
                    history.SellerID = new int?(int.Parse(row["SellerID"].ToString()));
                }
                if (row["SellerName"] != null)
                {
                    history.SellerName = row["SellerName"].ToString();
                }
                if (row["SellerEmail"] != null)
                {
                    history.SellerEmail = row["SellerEmail"].ToString();
                }
                if (row["SellerCellPhone"] != null)
                {
                    history.SellerCellPhone = row["SellerCellPhone"].ToString();
                }
                if ((row["SupplierId"] != null) && (row["SupplierId"].ToString() != ""))
                {
                    history.SupplierId = new int?(int.Parse(row["SupplierId"].ToString()));
                }
                if (row["SupplierName"] != null)
                {
                    history.SupplierName = row["SupplierName"].ToString();
                }
                if (row["ReferID"] != null)
                {
                    history.ReferID = row["ReferID"].ToString();
                }
                if (row["ReferURL"] != null)
                {
                    history.ReferURL = row["ReferURL"].ToString();
                }
                if (row["OrderIP"] != null)
                {
                    history.OrderIP = row["OrderIP"].ToString();
                }
                if (row["Remark"] != null)
                {
                    history.Remark = row["Remark"].ToString();
                }
            }
            return history;
        }

        public bool Delete(long OrderId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_OrdersHistory ");
            builder.Append(" where OrderId=@OrderId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@OrderId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = OrderId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string OrderIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_OrdersHistory ");
            builder.Append(" where OrderId in (" + OrderIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(long OrderId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_OrdersHistory");
            builder.Append(" where OrderId=@OrderId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@OrderId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = OrderId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select OrderId,OrderCode,ParentOrderId,CreatedDate,UpdatedDate,BuyerID,BuyerName,BuyerEmail,BuyerCellPhone,RegionId,ShipRegion,ShipAddress,ShipZipCode,ShipName,ShipTelPhone,ShipCellPhone,ShipEmail,ShippingModeId,ShippingModeName,RealShippingModeId,RealShippingModeName,ShipperId,ShipperName,ShipperAddress,ShipperCellPhone,Freight,FreightAdjusted,FreightActual,Weight,ShippingStatus,ShipOrderNumber,ExpressCompanyName,ExpressCompanyAbb,PaymentTypeId,PaymentTypeName,PaymentGateway,PaymentStatus,RefundStatus,PayCurrencyCode,PayCurrencyName,PaymentFee,PaymentFeeAdjusted,GatewayOrderId,OrderTotal,OrderPoint,OrderCostPrice,OrderProfit,OrderOtherCost,OrderOptionPrice,DiscountName,DiscountAmount,DiscountAdjusted,DiscountValue,DiscountValueType,CouponCode,CouponName,CouponAmount,CouponValue,CouponValueType,ActivityName,ActivityFreeAmount,ActivityStatus,GroupBuyId,GroupBuyPrice,GroupBuyStatus,Amount,OrderType,OrderStatus,SellerID,SellerName,SellerEmail,SellerCellPhone,SupplierId,SupplierName,ReferID,ReferURL,OrderIP,Remark ");
            builder.Append(" FROM Shop_OrdersHistory ");
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
            builder.Append(" OrderId,OrderCode,ParentOrderId,CreatedDate,UpdatedDate,BuyerID,BuyerName,BuyerEmail,BuyerCellPhone,RegionId,ShipRegion,ShipAddress,ShipZipCode,ShipName,ShipTelPhone,ShipCellPhone,ShipEmail,ShippingModeId,ShippingModeName,RealShippingModeId,RealShippingModeName,ShipperId,ShipperName,ShipperAddress,ShipperCellPhone,Freight,FreightAdjusted,FreightActual,Weight,ShippingStatus,ShipOrderNumber,ExpressCompanyName,ExpressCompanyAbb,PaymentTypeId,PaymentTypeName,PaymentGateway,PaymentStatus,RefundStatus,PayCurrencyCode,PayCurrencyName,PaymentFee,PaymentFeeAdjusted,GatewayOrderId,OrderTotal,OrderPoint,OrderCostPrice,OrderProfit,OrderOtherCost,OrderOptionPrice,DiscountName,DiscountAmount,DiscountAdjusted,DiscountValue,DiscountValueType,CouponCode,CouponName,CouponAmount,CouponValue,CouponValueType,ActivityName,ActivityFreeAmount,ActivityStatus,GroupBuyId,GroupBuyPrice,GroupBuyStatus,Amount,OrderType,OrderStatus,SellerID,SellerName,SellerEmail,SellerCellPhone,SupplierId,SupplierName,ReferID,ReferURL,OrderIP,Remark ");
            builder.Append(" FROM Shop_OrdersHistory ");
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
            builder.Append(")AS Row, T.*  from Shop_OrdersHistory T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Shop.Order.OrdersHistory GetModel(long OrderId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 OrderId,OrderCode,ParentOrderId,CreatedDate,UpdatedDate,BuyerID,BuyerName,BuyerEmail,BuyerCellPhone,RegionId,ShipRegion,ShipAddress,ShipZipCode,ShipName,ShipTelPhone,ShipCellPhone,ShipEmail,ShippingModeId,ShippingModeName,RealShippingModeId,RealShippingModeName,ShipperId,ShipperName,ShipperAddress,ShipperCellPhone,Freight,FreightAdjusted,FreightActual,Weight,ShippingStatus,ShipOrderNumber,ExpressCompanyName,ExpressCompanyAbb,PaymentTypeId,PaymentTypeName,PaymentGateway,PaymentStatus,RefundStatus,PayCurrencyCode,PayCurrencyName,PaymentFee,PaymentFeeAdjusted,GatewayOrderId,OrderTotal,OrderPoint,OrderCostPrice,OrderProfit,OrderOtherCost,OrderOptionPrice,DiscountName,DiscountAmount,DiscountAdjusted,DiscountValue,DiscountValueType,CouponCode,CouponName,CouponAmount,CouponValue,CouponValueType,ActivityName,ActivityFreeAmount,ActivityStatus,GroupBuyId,GroupBuyPrice,GroupBuyStatus,Amount,OrderType,OrderStatus,SellerID,SellerName,SellerEmail,SellerCellPhone,SupplierId,SupplierName,ReferID,ReferURL,OrderIP,Remark from Shop_OrdersHistory ");
            builder.Append(" where OrderId=@OrderId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@OrderId", SqlDbType.BigInt, 8) };
            cmdParms[0].Value = OrderId;
            new Maticsoft.Model.Shop.Order.OrdersHistory();
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
            builder.Append("select count(1) FROM Shop_OrdersHistory ");
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

        public bool Update(Maticsoft.Model.Shop.Order.OrdersHistory model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_OrdersHistory set ");
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
            builder.Append("Remark=@Remark");
            builder.Append(" where OrderId=@OrderId ");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50), new SqlParameter("@ParentOrderId", SqlDbType.BigInt, 8), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@UpdatedDate", SqlDbType.DateTime), new SqlParameter("@BuyerID", SqlDbType.Int, 4), new SqlParameter("@BuyerName", SqlDbType.NVarChar, 100), new SqlParameter("@BuyerEmail", SqlDbType.NVarChar, 100), new SqlParameter("@BuyerCellPhone", SqlDbType.NVarChar, 50), new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@ShipRegion", SqlDbType.NVarChar, 300), new SqlParameter("@ShipAddress", SqlDbType.NVarChar, 300), new SqlParameter("@ShipZipCode", SqlDbType.NVarChar, 20), new SqlParameter("@ShipName", SqlDbType.NVarChar, 50), new SqlParameter("@ShipTelPhone", SqlDbType.NVarChar, 50), new SqlParameter("@ShipCellPhone", SqlDbType.NVarChar, 50), new SqlParameter("@ShipEmail", SqlDbType.NVarChar, 100), 
                new SqlParameter("@ShippingModeId", SqlDbType.Int, 4), new SqlParameter("@ShippingModeName", SqlDbType.NVarChar, 100), new SqlParameter("@RealShippingModeId", SqlDbType.Int, 4), new SqlParameter("@RealShippingModeName", SqlDbType.NVarChar, 100), new SqlParameter("@ShipperId", SqlDbType.Int, 4), new SqlParameter("@ShipperName", SqlDbType.NVarChar, 100), new SqlParameter("@ShipperAddress", SqlDbType.NVarChar, 300), new SqlParameter("@ShipperCellPhone", SqlDbType.NVarChar, 20), new SqlParameter("@Freight", SqlDbType.Money, 8), new SqlParameter("@FreightAdjusted", SqlDbType.Money, 8), new SqlParameter("@FreightActual", SqlDbType.Money, 8), new SqlParameter("@Weight", SqlDbType.Int, 4), new SqlParameter("@ShippingStatus", SqlDbType.SmallInt, 2), new SqlParameter("@ShipOrderNumber", SqlDbType.NVarChar, 50), new SqlParameter("@ExpressCompanyName", SqlDbType.NVarChar, 500), new SqlParameter("@ExpressCompanyAbb", SqlDbType.NVarChar, 500), 
                new SqlParameter("@PaymentTypeId", SqlDbType.Int, 4), new SqlParameter("@PaymentTypeName", SqlDbType.NVarChar, 100), new SqlParameter("@PaymentGateway", SqlDbType.NVarChar, 50), new SqlParameter("@PaymentStatus", SqlDbType.SmallInt, 2), new SqlParameter("@RefundStatus", SqlDbType.SmallInt, 2), new SqlParameter("@PayCurrencyCode", SqlDbType.NVarChar, 20), new SqlParameter("@PayCurrencyName", SqlDbType.NVarChar, 20), new SqlParameter("@PaymentFee", SqlDbType.Money, 8), new SqlParameter("@PaymentFeeAdjusted", SqlDbType.Money, 8), new SqlParameter("@GatewayOrderId", SqlDbType.NVarChar, 100), new SqlParameter("@OrderTotal", SqlDbType.Money, 8), new SqlParameter("@OrderPoint", SqlDbType.Int, 4), new SqlParameter("@OrderCostPrice", SqlDbType.Money, 8), new SqlParameter("@OrderProfit", SqlDbType.Money, 8), new SqlParameter("@OrderOtherCost", SqlDbType.Money, 8), new SqlParameter("@OrderOptionPrice", SqlDbType.Money, 8), 
                new SqlParameter("@DiscountName", SqlDbType.NVarChar, 200), new SqlParameter("@DiscountAmount", SqlDbType.Money, 8), new SqlParameter("@DiscountAdjusted", SqlDbType.Money, 8), new SqlParameter("@DiscountValue", SqlDbType.Money, 8), new SqlParameter("@DiscountValueType", SqlDbType.SmallInt, 2), new SqlParameter("@CouponCode", SqlDbType.NVarChar, 50), new SqlParameter("@CouponName", SqlDbType.NVarChar, 100), new SqlParameter("@CouponAmount", SqlDbType.Money, 8), new SqlParameter("@CouponValue", SqlDbType.Money, 8), new SqlParameter("@CouponValueType", SqlDbType.SmallInt, 2), new SqlParameter("@ActivityName", SqlDbType.NVarChar, 200), new SqlParameter("@ActivityFreeAmount", SqlDbType.Money, 8), new SqlParameter("@ActivityStatus", SqlDbType.SmallInt, 2), new SqlParameter("@GroupBuyId", SqlDbType.Int, 4), new SqlParameter("@GroupBuyPrice", SqlDbType.Money, 8), new SqlParameter("@GroupBuyStatus", SqlDbType.SmallInt, 2), 
                new SqlParameter("@Amount", SqlDbType.Money, 8), new SqlParameter("@OrderType", SqlDbType.SmallInt, 2), new SqlParameter("@OrderStatus", SqlDbType.SmallInt, 2), new SqlParameter("@SellerID", SqlDbType.Int, 4), new SqlParameter("@SellerName", SqlDbType.NVarChar, 100), new SqlParameter("@SellerEmail", SqlDbType.NVarChar, 100), new SqlParameter("@SellerCellPhone", SqlDbType.NVarChar, 50), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@SupplierName", SqlDbType.NVarChar, 100), new SqlParameter("@ReferID", SqlDbType.NVarChar, 50), new SqlParameter("@ReferURL", SqlDbType.NVarChar, 200), new SqlParameter("@OrderIP", SqlDbType.NVarChar, 50), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@OrderId", SqlDbType.BigInt, 8)
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
            cmdParms[0x47].Value = model.SupplierId;
            cmdParms[0x48].Value = model.SupplierName;
            cmdParms[0x49].Value = model.ReferID;
            cmdParms[0x4a].Value = model.ReferURL;
            cmdParms[0x4b].Value = model.OrderIP;
            cmdParms[0x4c].Value = model.Remark;
            cmdParms[0x4d].Value = model.OrderId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

