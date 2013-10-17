namespace Maticsoft.SQLServerDAL.Shop.Order
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Common;
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Order;
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public class OrderService : IOrderService
    {
        public bool CancelOrder(OrderInfo orderInfo, User currentUser = new User())
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            if ((orderInfo.OrderItems != null) && (orderInfo.OrderItems.Count > 0))
            {
                foreach (Maticsoft.Model.Shop.Order.OrderItems items in orderInfo.OrderItems)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("update Shop_SKUs  set Stock=Stock+@Stock");
                    builder.Append(" where SKU=@SKU");
                    SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@SKU", SqlDbType.NVarChar, 50), new SqlParameter("@Stock", SqlDbType.Int, 4) };
                    parameterArray[0].Value = items.SKU;
                    parameterArray[1].Value = items.Quantity;
                    cmdList.Add(new CommandInfo(builder.ToString(), parameterArray));
                }
            }
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("UPDATE  Shop_Orders SET OrderStatus=-1, UpdatedDate=@UpdatedDate");
            builder2.Append(" where OrderId=@OrderId OR ParentOrderId=@OrderId");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@OrderId", SqlDbType.BigInt, 8), new SqlParameter("@UpdatedDate", SqlDbType.DateTime) };
            para[0].Value = orderInfo.OrderId;
            para[1].Value = DateTime.Now;
            CommandInfo item = new CommandInfo(builder2.ToString(), para, EffentNextType.ExcuteEffectRows);
            cmdList.Add(item);
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("insert into Shop_OrderAction(");
            builder3.Append("OrderId,OrderCode,UserId,Username,ActionCode,ActionDate,Remark)");
            builder3.Append(" values (");
            builder3.Append("@OrderId,@OrderCode,@UserId,@Username,@ActionCode,@ActionDate,@Remark)");
            SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@OrderId", SqlDbType.BigInt, 8), new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@Username", SqlDbType.NVarChar, 200), new SqlParameter("@ActionCode", SqlDbType.NVarChar, 100), new SqlParameter("@ActionDate", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x3e8) };
            parameterArray3[0].Value = orderInfo.OrderId;
            parameterArray3[1].Value = orderInfo.OrderCode;
            parameterArray3[2].Value = (currentUser != null) ? currentUser.UserID : orderInfo.BuyerID;
            parameterArray3[3].Value = (currentUser != null) ? currentUser.NickName : orderInfo.BuyerName;
            parameterArray3[4].Value = 0x65;
            parameterArray3[5].Value = DateTime.Now;
            if ((currentUser != null) && (currentUser.UserType == "AA"))
            {
                parameterArray3[6].Value = "管理员取消订单";
            }
            else
            {
                parameterArray3[6].Value = "取消订单";
            }
            item = new CommandInfo(builder3.ToString(), parameterArray3, EffentNextType.ExcuteEffectRows);
            cmdList.Add(item);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }

        public long CreateOrder(OrderInfo orderInfo)
        {
            using (SqlConnection connection = DbHelperSQL.GetConnection)
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        orderInfo.OrderId = Globals.SafeLong(DbHelperSQL.GetSingle4Trans(this.GenerateOrderInfo(orderInfo), transaction).ToString(), (long) (-1L));
                        DbHelperSQL.ExecuteSqlTran4Indentity(this.GenerateOrderItems(orderInfo), transaction);
                        DbHelperSQL.ExecuteSqlTran4Indentity(this.GenerateOrderAction(orderInfo), transaction);
                        DbHelperSQL.ExecuteSqlTran4Indentity(this.CutSKUStock(orderInfo), transaction);
                        if ((orderInfo.SubOrders != null) && (orderInfo.SubOrders.Count > 0))
                        {
                            foreach (OrderInfo info in orderInfo.SubOrders)
                            {
                                info.ParentOrderId = orderInfo.OrderId;
                                this.CreateSubOrder(info, transaction);
                            }
                        }
                        transaction.Commit();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return orderInfo.OrderId;
        }

        public long CreateSubOrder(OrderInfo subInfo, SqlTransaction transaction)
        {
            subInfo.OrderId = Globals.SafeLong(DbHelperSQL.GetSingle4Trans(this.GenerateOrderInfo(subInfo), transaction).ToString(), (long) (-1L));
            DbHelperSQL.ExecuteSqlTran4Indentity(this.GenerateOrderItems(subInfo), transaction);
            DbHelperSQL.ExecuteSqlTran4Indentity(this.GenerateOrderAction(subInfo), transaction);
            return subInfo.OrderId;
        }

        private List<CommandInfo> CutSKUStock(OrderInfo orderInfo)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            foreach (Maticsoft.Model.Shop.Order.OrderItems items in orderInfo.OrderItems)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("update Shop_SKUs  set Stock=Stock-@Stock");
                builder.Append(" where SKU=@SKU");
                SqlParameter[] para = new SqlParameter[] { new SqlParameter("@SKU", SqlDbType.NVarChar, 50), new SqlParameter("@Stock", SqlDbType.Int, 4) };
                para[0].Value = items.SKU;
                para[1].Value = items.Quantity;
                list.Add(new CommandInfo(builder.ToString(), para));
            }
            return list;
        }

        private List<CommandInfo> GenerateOrderAction(OrderInfo orderInfo)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_OrderAction(");
            builder.Append("OrderId,OrderCode,UserId,Username,ActionCode,ActionDate,Remark)");
            builder.Append(" values (");
            builder.Append("@OrderId,@OrderCode,@UserId,@Username,@ActionCode,@ActionDate,@Remark)");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@OrderId", SqlDbType.BigInt, 8), new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@Username", SqlDbType.NVarChar, 200), new SqlParameter("@ActionCode", SqlDbType.NVarChar, 100), new SqlParameter("@ActionDate", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x3e8) };
            para[0].Value = orderInfo.OrderId;
            para[1].Value = orderInfo.OrderCode;
            para[4].Value = 100;
            para[5].Value = DateTime.Now;
            para[2].Value = orderInfo.BuyerID;
            para[3].Value = orderInfo.BuyerName;
            para[6].Value = "创建订单";
            return new List<CommandInfo> { new CommandInfo(builder.ToString(), para, 3) };
        }

        private CommandInfo GenerateOrderInfo(OrderInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_Orders(");
            builder.Append("OrderCode,ParentOrderId,CreatedDate,UpdatedDate,BuyerID,BuyerName,BuyerEmail,BuyerCellPhone,RegionId,ShipRegion,ShipAddress,ShipZipCode,ShipName,ShipTelPhone,ShipCellPhone,ShipEmail,ShippingModeId,ShippingModeName,RealShippingModeId,RealShippingModeName,ShipperId,ShipperName,ShipperAddress,ShipperCellPhone,Freight,FreightAdjusted,FreightActual,Weight,ShippingStatus,ShipOrderNumber,ExpressCompanyName,ExpressCompanyAbb,PaymentTypeId,PaymentTypeName,PaymentGateway,PaymentStatus,RefundStatus,PayCurrencyCode,PayCurrencyName,PaymentFee,PaymentFeeAdjusted,GatewayOrderId,OrderTotal,OrderPoint,OrderCostPrice,OrderProfit,OrderOtherCost,OrderOptionPrice,DiscountName,DiscountAmount,DiscountAdjusted,DiscountValue,DiscountValueType,CouponCode,CouponName,CouponAmount,CouponValue,CouponValueType,ActivityName,ActivityFreeAmount,ActivityStatus,GroupBuyId,GroupBuyPrice,GroupBuyStatus,Amount,OrderType,OrderStatus,SellerID,SellerName,SellerEmail,SellerCellPhone,CommentStatus,SupplierId,SupplierName,ReferID,ReferURL,OrderIP,Remark,ProductTotal,HasChildren,IsReviews)");
            builder.Append(" values (");
            builder.Append("@OrderCode,@ParentOrderId,@CreatedDate,@UpdatedDate,@BuyerID,@BuyerName,@BuyerEmail,@BuyerCellPhone,@RegionId,@ShipRegion,@ShipAddress,@ShipZipCode,@ShipName,@ShipTelPhone,@ShipCellPhone,@ShipEmail,@ShippingModeId,@ShippingModeName,@RealShippingModeId,@RealShippingModeName,@ShipperId,@ShipperName,@ShipperAddress,@ShipperCellPhone,@Freight,@FreightAdjusted,@FreightActual,@Weight,@ShippingStatus,@ShipOrderNumber,@ExpressCompanyName,@ExpressCompanyAbb,@PaymentTypeId,@PaymentTypeName,@PaymentGateway,@PaymentStatus,@RefundStatus,@PayCurrencyCode,@PayCurrencyName,@PaymentFee,@PaymentFeeAdjusted,@GatewayOrderId,@OrderTotal,@OrderPoint,@OrderCostPrice,@OrderProfit,@OrderOtherCost,@OrderOptionPrice,@DiscountName,@DiscountAmount,@DiscountAdjusted,@DiscountValue,@DiscountValueType,@CouponCode,@CouponName,@CouponAmount,@CouponValue,@CouponValueType,@ActivityName,@ActivityFreeAmount,@ActivityStatus,@GroupBuyId,@GroupBuyPrice,@GroupBuyStatus,@Amount,@OrderType,@OrderStatus,@SellerID,@SellerName,@SellerEmail,@SellerCellPhone,@CommentStatus,@SupplierId,@SupplierName,@ReferID,@ReferURL,@OrderIP,@Remark,@ProductTotal,@HasChildren,@IsReviews)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50), new SqlParameter("@ParentOrderId", SqlDbType.BigInt, 8), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@UpdatedDate", SqlDbType.DateTime), new SqlParameter("@BuyerID", SqlDbType.Int, 4), new SqlParameter("@BuyerName", SqlDbType.NVarChar, 100), new SqlParameter("@BuyerEmail", SqlDbType.NVarChar, 100), new SqlParameter("@BuyerCellPhone", SqlDbType.NVarChar, 50), new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@ShipRegion", SqlDbType.NVarChar, 300), new SqlParameter("@ShipAddress", SqlDbType.NVarChar, 300), new SqlParameter("@ShipZipCode", SqlDbType.NVarChar, 20), new SqlParameter("@ShipName", SqlDbType.NVarChar, 50), new SqlParameter("@ShipTelPhone", SqlDbType.NVarChar, 50), new SqlParameter("@ShipCellPhone", SqlDbType.NVarChar, 50), new SqlParameter("@ShipEmail", SqlDbType.NVarChar, 100), 
                new SqlParameter("@ShippingModeId", SqlDbType.Int, 4), new SqlParameter("@ShippingModeName", SqlDbType.NVarChar, 100), new SqlParameter("@RealShippingModeId", SqlDbType.Int, 4), new SqlParameter("@RealShippingModeName", SqlDbType.NVarChar, 100), new SqlParameter("@ShipperId", SqlDbType.Int, 4), new SqlParameter("@ShipperName", SqlDbType.NVarChar, 100), new SqlParameter("@ShipperAddress", SqlDbType.NVarChar, 300), new SqlParameter("@ShipperCellPhone", SqlDbType.NVarChar, 20), new SqlParameter("@Freight", SqlDbType.Money, 8), new SqlParameter("@FreightAdjusted", SqlDbType.Money, 8), new SqlParameter("@FreightActual", SqlDbType.Money, 8), new SqlParameter("@Weight", SqlDbType.Int, 4), new SqlParameter("@ShippingStatus", SqlDbType.SmallInt, 2), new SqlParameter("@ShipOrderNumber", SqlDbType.NVarChar, 50), new SqlParameter("@ExpressCompanyName", SqlDbType.NVarChar, 500), new SqlParameter("@ExpressCompanyAbb", SqlDbType.NVarChar, 500), 
                new SqlParameter("@PaymentTypeId", SqlDbType.Int, 4), new SqlParameter("@PaymentTypeName", SqlDbType.NVarChar, 100), new SqlParameter("@PaymentGateway", SqlDbType.NVarChar, 50), new SqlParameter("@PaymentStatus", SqlDbType.SmallInt, 2), new SqlParameter("@RefundStatus", SqlDbType.SmallInt, 2), new SqlParameter("@PayCurrencyCode", SqlDbType.NVarChar, 20), new SqlParameter("@PayCurrencyName", SqlDbType.NVarChar, 20), new SqlParameter("@PaymentFee", SqlDbType.Money, 8), new SqlParameter("@PaymentFeeAdjusted", SqlDbType.Money, 8), new SqlParameter("@GatewayOrderId", SqlDbType.NVarChar, 100), new SqlParameter("@OrderTotal", SqlDbType.Money, 8), new SqlParameter("@OrderPoint", SqlDbType.Int, 4), new SqlParameter("@OrderCostPrice", SqlDbType.Money, 8), new SqlParameter("@OrderProfit", SqlDbType.Money, 8), new SqlParameter("@OrderOtherCost", SqlDbType.Money, 8), new SqlParameter("@OrderOptionPrice", SqlDbType.Money, 8), 
                new SqlParameter("@DiscountName", SqlDbType.NVarChar, 200), new SqlParameter("@DiscountAmount", SqlDbType.Money, 8), new SqlParameter("@DiscountAdjusted", SqlDbType.Money, 8), new SqlParameter("@DiscountValue", SqlDbType.Money, 8), new SqlParameter("@DiscountValueType", SqlDbType.SmallInt, 2), new SqlParameter("@CouponCode", SqlDbType.NVarChar, 50), new SqlParameter("@CouponName", SqlDbType.NVarChar, 100), new SqlParameter("@CouponAmount", SqlDbType.Money, 8), new SqlParameter("@CouponValue", SqlDbType.Money, 8), new SqlParameter("@CouponValueType", SqlDbType.SmallInt, 2), new SqlParameter("@ActivityName", SqlDbType.NVarChar, 200), new SqlParameter("@ActivityFreeAmount", SqlDbType.Money, 8), new SqlParameter("@ActivityStatus", SqlDbType.SmallInt, 2), new SqlParameter("@GroupBuyId", SqlDbType.Int, 4), new SqlParameter("@GroupBuyPrice", SqlDbType.Money, 8), new SqlParameter("@GroupBuyStatus", SqlDbType.SmallInt, 2), 
                new SqlParameter("@Amount", SqlDbType.Money, 8), new SqlParameter("@OrderType", SqlDbType.SmallInt, 2), new SqlParameter("@OrderStatus", SqlDbType.SmallInt, 2), new SqlParameter("@SellerID", SqlDbType.Int, 4), new SqlParameter("@SellerName", SqlDbType.NVarChar, 100), new SqlParameter("@SellerEmail", SqlDbType.NVarChar, 100), new SqlParameter("@SellerCellPhone", SqlDbType.NVarChar, 50), new SqlParameter("@CommentStatus", SqlDbType.SmallInt, 2), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@SupplierName", SqlDbType.NVarChar, 100), new SqlParameter("@ReferID", SqlDbType.NVarChar, 50), new SqlParameter("@ReferURL", SqlDbType.NVarChar, 200), new SqlParameter("@OrderIP", SqlDbType.NVarChar, 50), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@ProductTotal", SqlDbType.Money, 8), new SqlParameter("@HasChildren", SqlDbType.Bit, 1), 
                new SqlParameter("@IsReviews", SqlDbType.Bit, 1)
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
            para[0x47].Value = model.CommentStatus;
            para[0x48].Value = model.SupplierId;
            para[0x49].Value = model.SupplierName;
            para[0x4a].Value = model.ReferID;
            para[0x4b].Value = model.ReferURL;
            para[0x4c].Value = model.OrderIP;
            para[0x4d].Value = model.Remark;
            para[0x4e].Value = model.ProductTotal;
            para[0x4f].Value = model.HasChildren;
            para[80].Value = model.IsReviews;
            return new CommandInfo(builder.ToString(), para);
        }

        private List<CommandInfo> GenerateOrderItems(OrderInfo orderInfo)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            foreach (Maticsoft.Model.Shop.Order.OrderItems items in orderInfo.OrderItems)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("insert into Shop_OrderItems(");
                builder.Append("OrderId,OrderCode,ProductId,ProductCode,SKU,Name,ThumbnailsUrl,Description,Quantity,ShipmentQuantity,CostPrice,SellPrice,AdjustedPrice,Attribute,Remark,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName)");
                builder.Append(" values (");
                builder.Append("@OrderId,@OrderCode,@ProductId,@ProductCode,@SKU,@Name,@ThumbnailsUrl,@Description,@Quantity,@ShipmentQuantity,@CostPrice,@SellPrice,@AdjustedPrice,@Attribute,@Remark,@Weight,@Deduct,@Points,@ProductLineId,@SupplierId,@SupplierName)");
                builder.Append(";select @@IDENTITY");
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@OrderId", SqlDbType.BigInt, 8), new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50), new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@ProductCode", SqlDbType.NVarChar, 50), new SqlParameter("@SKU", SqlDbType.NVarChar, 200), new SqlParameter("@Name", SqlDbType.NVarChar, 200), new SqlParameter("@ThumbnailsUrl", SqlDbType.NVarChar, 300), new SqlParameter("@Description", SqlDbType.NVarChar, 500), new SqlParameter("@Quantity", SqlDbType.Int, 4), new SqlParameter("@ShipmentQuantity", SqlDbType.Int, 4), new SqlParameter("@CostPrice", SqlDbType.Money, 8), new SqlParameter("@SellPrice", SqlDbType.Money, 8), new SqlParameter("@AdjustedPrice", SqlDbType.Money, 8), new SqlParameter("@Attribute", SqlDbType.Text), new SqlParameter("@Remark", SqlDbType.Text), new SqlParameter("@Weight", SqlDbType.Int, 4), 
                    new SqlParameter("@Deduct", SqlDbType.Money, 8), new SqlParameter("@Points", SqlDbType.Int, 4), new SqlParameter("@ProductLineId", SqlDbType.Int, 4), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@SupplierName", SqlDbType.NVarChar, 100)
                 };
                para[0].Value = orderInfo.OrderId;
                para[1].Value = orderInfo.OrderCode;
                para[2].Value = items.ProductId;
                para[3].Value = items.ProductCode;
                para[4].Value = items.SKU;
                para[5].Value = items.Name;
                para[6].Value = items.ThumbnailsUrl;
                para[7].Value = items.Description;
                para[8].Value = items.Quantity;
                para[9].Value = items.ShipmentQuantity;
                para[10].Value = items.CostPrice;
                para[11].Value = items.SellPrice;
                para[12].Value = items.AdjustedPrice;
                para[13].Value = items.Attribute;
                para[14].Value = items.Remark;
                para[15].Value = items.Weight;
                para[0x10].Value = items.Deduct;
                para[0x11].Value = items.Points;
                para[0x12].Value = items.ProductLineId;
                para[0x13].Value = items.SupplierId;
                para[20].Value = items.SupplierName;
                list.Add(new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows));
            }
            return list;
        }

        public bool PayForOrder(OrderInfo orderInfo, User currentUser = new User())
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE  Shop_Orders SET OrderStatus=1, PaymentStatus=2, UpdatedDate=@UpdatedDate");
            builder.Append(" WHERE OrderId=@OrderId OR ParentOrderId=@OrderId");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@OrderId", SqlDbType.BigInt, 8), new SqlParameter("@UpdatedDate", SqlDbType.DateTime) };
            para[0].Value = orderInfo.OrderId;
            para[1].Value = DateTime.Now;
            cmdList.Add(new CommandInfo(builder.ToString(), para, EffentNextType.ExcuteEffectRows));
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("insert into Shop_OrderAction(");
            builder2.Append("OrderId,OrderCode,UserId,Username,ActionCode,ActionDate,Remark)");
            builder2.Append(" values (");
            builder2.Append("@OrderId,@OrderCode,@UserId,@Username,@ActionCode,@ActionDate,@Remark)");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@OrderId", SqlDbType.BigInt, 8), new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@Username", SqlDbType.NVarChar, 200), new SqlParameter("@ActionCode", SqlDbType.NVarChar, 100), new SqlParameter("@ActionDate", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x3e8) };
            parameterArray2[0].Value = orderInfo.OrderId;
            parameterArray2[1].Value = orderInfo.OrderCode;
            parameterArray2[2].Value = (currentUser != null) ? currentUser.UserID : orderInfo.BuyerID;
            parameterArray2[3].Value = "系统";
            parameterArray2[4].Value = 0x66;
            parameterArray2[5].Value = DateTime.Now;
            parameterArray2[6].Value = "支付订单";
            cmdList.Add(new CommandInfo(builder2.ToString(), parameterArray2, EffentNextType.ExcuteEffectRows));
            if (orderInfo.HasChildren && (orderInfo.SubOrders.Count > 0))
            {
                foreach (OrderInfo info in orderInfo.SubOrders)
                {
                    parameterArray2 = new SqlParameter[] { new SqlParameter("@OrderId", SqlDbType.BigInt, 8), new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@Username", SqlDbType.NVarChar, 200), new SqlParameter("@ActionCode", SqlDbType.NVarChar, 100), new SqlParameter("@ActionDate", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x3e8) };
                    parameterArray2[0].Value = info.OrderId;
                    parameterArray2[1].Value = info.OrderCode;
                    parameterArray2[2].Value = (currentUser != null) ? currentUser.UserID : orderInfo.BuyerID;
                    parameterArray2[3].Value = "系统";
                    parameterArray2[4].Value = 0x66;
                    parameterArray2[5].Value = DateTime.Now;
                    parameterArray2[6].Value = "支付订单";
                    cmdList.Add(new CommandInfo(builder2.ToString(), parameterArray2, EffentNextType.ExcuteEffectRows));
                }
            }
            if ((orderInfo.OrderItems != null) && (orderInfo.OrderItems.Count > 0))
            {
                foreach (Maticsoft.Model.Shop.Order.OrderItems items in orderInfo.OrderItems)
                {
                    StringBuilder builder3 = new StringBuilder();
                    builder3.Append("update Shop_Products SET SaleCounts=SaleCounts+@Stock");
                    builder3.Append(" where ProductId=@ProductId");
                    SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt), new SqlParameter("@Stock", SqlDbType.Int, 4) };
                    parameterArray3[0].Value = items.ProductId;
                    parameterArray3[1].Value = items.Quantity;
                    cmdList.Add(new CommandInfo(builder3.ToString(), parameterArray3));
                }
            }
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }
    }
}

