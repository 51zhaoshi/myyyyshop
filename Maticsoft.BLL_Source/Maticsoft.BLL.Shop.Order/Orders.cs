namespace Maticsoft.BLL.Shop.Order
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Order;
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Orders
    {
        private readonly IOrders dal = DAShopOrder.CreateOrders();

        public long Add(OrderInfo model)
        {
            return this.dal.Add(model);
        }

        public List<OrderInfo> DataTableToList(DataTable dt)
        {
            List<OrderInfo> list = new List<OrderInfo>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    OrderInfo item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(long OrderId)
        {
            return this.dal.Delete(OrderId);
        }

        public bool DeleteList(string OrderIdlist)
        {
            return this.dal.DeleteList(OrderIdlist);
        }

        public bool Exists(long OrderId)
        {
            return this.dal.Exists(OrderId);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public List<OrderInfo> GetListByPageEX(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DataSet set = this.GetListByPage(strWhere, orderby, startIndex, endIndex);
            return this.DataTableToList(set.Tables[0]);
        }

        public List<OrderInfo> GetListByStatus(EnumHelper.OrderMainStatus orderType)
        {
            string strWhere = this.GetWhereByStatus(orderType) + " order by CreatedDate desc ";
            return this.GetModelList(strWhere);
        }

        public OrderInfo GetModel(long OrderId)
        {
            return this.dal.GetModel(OrderId);
        }

        public OrderInfo GetModelByCache(long OrderId)
        {
            string cacheKey = "OrdersModel-" + OrderId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(OrderId);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (OrderInfo) cache;
        }

        public OrderInfo GetModelInfo(long OrderId)
        {
            OrderInfo model = this.GetModel(OrderId);
            if (model != null)
            {
                model.OrderItems = new Maticsoft.BLL.Shop.Order.OrderItems().GetModelList(" OrderId=" + OrderId);
            }
            return model;
        }

        public OrderInfo GetModelInfoByCache(long OrderId)
        {
            string cacheKey = "GetModelInfoByCache-" + OrderId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetModelInfo(OrderId);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (OrderInfo) cache;
        }

        public List<OrderInfo> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public EnumHelper.OrderMainStatus GetOrderType(EnumHelper.PaymentGateway paymentGateway, EnumHelper.OrderStatus orderStatus, EnumHelper.PaymentStatus paymentStatus, EnumHelper.ShippingStatus shippingStatus)
        {
            EnumHelper.OrderMainStatus preHandle = EnumHelper.OrderMainStatus.PreHandle;
            switch (paymentGateway)
            {
                case EnumHelper.PaymentGateway.cod:
                    if (((orderStatus == EnumHelper.OrderStatus.UnHandle) && (paymentStatus == EnumHelper.PaymentStatus.Unpaid)) && (shippingStatus == EnumHelper.ShippingStatus.UnShipped))
                    {
                        preHandle = EnumHelper.OrderMainStatus.PreHandle;
                    }
                    if (((orderStatus == EnumHelper.OrderStatus.Cancel) && (paymentStatus == EnumHelper.PaymentStatus.Unpaid)) && (shippingStatus == EnumHelper.ShippingStatus.UnShipped))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Cancel;
                    }
                    if (((orderStatus == EnumHelper.OrderStatus.UserLock) || (orderStatus == EnumHelper.OrderStatus.AdminLock)) && ((paymentStatus == EnumHelper.PaymentStatus.Unpaid) && (shippingStatus == EnumHelper.ShippingStatus.UnShipped)))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Locking;
                    }
                    if (((orderStatus == EnumHelper.OrderStatus.Handling) && (paymentStatus == EnumHelper.PaymentStatus.Unpaid)) && (shippingStatus == EnumHelper.ShippingStatus.UnShipped))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Handling;
                    }
                    if (((orderStatus == EnumHelper.OrderStatus.Handling) && (paymentStatus == EnumHelper.PaymentStatus.Unpaid)) && (shippingStatus == EnumHelper.ShippingStatus.Packing))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Shipping;
                    }
                    if (((orderStatus == EnumHelper.OrderStatus.Handling) && (paymentStatus == EnumHelper.PaymentStatus.Unpaid)) && (shippingStatus == EnumHelper.ShippingStatus.Shipped))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Shiped;
                    }
                    if (((orderStatus == EnumHelper.OrderStatus.Complete) && (paymentStatus == EnumHelper.PaymentStatus.Paid)) && (shippingStatus == EnumHelper.ShippingStatus.ConfirmShip))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Complete;
                    }
                    return preHandle;

                case EnumHelper.PaymentGateway.bank:
                    if (((orderStatus == EnumHelper.OrderStatus.UnHandle) && (paymentStatus == EnumHelper.PaymentStatus.Unpaid)) && (shippingStatus == EnumHelper.ShippingStatus.UnShipped))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Paying;
                    }
                    if (((orderStatus == EnumHelper.OrderStatus.Cancel) && (paymentStatus == EnumHelper.PaymentStatus.Unpaid)) && (shippingStatus == EnumHelper.ShippingStatus.UnShipped))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Cancel;
                    }
                    if (((orderStatus == EnumHelper.OrderStatus.UserLock) || (orderStatus == EnumHelper.OrderStatus.AdminLock)) && ((paymentStatus == EnumHelper.PaymentStatus.Unpaid) && (shippingStatus == EnumHelper.ShippingStatus.UnShipped)))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Locking;
                    }
                    if (((orderStatus == EnumHelper.OrderStatus.UnHandle) && (paymentStatus == EnumHelper.PaymentStatus.Paid)) && (shippingStatus == EnumHelper.ShippingStatus.UnShipped))
                    {
                        preHandle = EnumHelper.OrderMainStatus.PreConfirm;
                    }
                    if (((orderStatus == EnumHelper.OrderStatus.Handling) && (paymentStatus == EnumHelper.PaymentStatus.Paid)) && (shippingStatus == EnumHelper.ShippingStatus.UnShipped))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Handling;
                    }
                    if (((orderStatus == EnumHelper.OrderStatus.Handling) && (paymentStatus == EnumHelper.PaymentStatus.Paid)) && (shippingStatus == EnumHelper.ShippingStatus.Packing))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Shipping;
                    }
                    if (((orderStatus == EnumHelper.OrderStatus.Handling) && (paymentStatus == EnumHelper.PaymentStatus.Paid)) && (shippingStatus == EnumHelper.ShippingStatus.Shipped))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Shiped;
                    }
                    if (((orderStatus == EnumHelper.OrderStatus.Complete) && (paymentStatus == EnumHelper.PaymentStatus.Paid)) && (shippingStatus == EnumHelper.ShippingStatus.ConfirmShip))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Complete;
                    }
                    return preHandle;
            }
            if (((orderStatus == EnumHelper.OrderStatus.UnHandle) && (paymentStatus == EnumHelper.PaymentStatus.Unpaid)) && (shippingStatus == EnumHelper.ShippingStatus.UnShipped))
            {
                preHandle = EnumHelper.OrderMainStatus.Paying;
            }
            if (((orderStatus == EnumHelper.OrderStatus.Cancel) && (paymentStatus == EnumHelper.PaymentStatus.Unpaid)) && (shippingStatus == EnumHelper.ShippingStatus.UnShipped))
            {
                preHandle = EnumHelper.OrderMainStatus.Cancel;
            }
            if (((orderStatus == EnumHelper.OrderStatus.UserLock) || (orderStatus == EnumHelper.OrderStatus.AdminLock)) && ((paymentStatus == EnumHelper.PaymentStatus.Unpaid) && (shippingStatus == EnumHelper.ShippingStatus.UnShipped)))
            {
                preHandle = EnumHelper.OrderMainStatus.Locking;
            }
            if (((orderStatus == EnumHelper.OrderStatus.Handling) && (paymentStatus == EnumHelper.PaymentStatus.Paid)) && (shippingStatus == EnumHelper.ShippingStatus.UnShipped))
            {
                preHandle = EnumHelper.OrderMainStatus.Handling;
            }
            if (((orderStatus == EnumHelper.OrderStatus.Handling) && (paymentStatus == EnumHelper.PaymentStatus.Paid)) && (shippingStatus == EnumHelper.ShippingStatus.Packing))
            {
                preHandle = EnumHelper.OrderMainStatus.Shipping;
            }
            if (((orderStatus == EnumHelper.OrderStatus.Handling) && (paymentStatus == EnumHelper.PaymentStatus.Paid)) && (shippingStatus == EnumHelper.ShippingStatus.Shipped))
            {
                preHandle = EnumHelper.OrderMainStatus.Shiped;
            }
            if (((orderStatus == EnumHelper.OrderStatus.Complete) && (paymentStatus == EnumHelper.PaymentStatus.Paid)) && (shippingStatus == EnumHelper.ShippingStatus.ConfirmShip))
            {
                preHandle = EnumHelper.OrderMainStatus.Complete;
            }
            return preHandle;
        }

        public EnumHelper.OrderMainStatus GetOrderType(string paymentGateway, int orderStatus, int paymentStatus, int shippingStatus)
        {
            EnumHelper.OrderMainStatus preHandle = EnumHelper.OrderMainStatus.PreHandle;
            switch (paymentGateway)
            {
                case "cod":
                    if (((orderStatus == 0) && (paymentStatus == 0)) && (shippingStatus == 0))
                    {
                        preHandle = EnumHelper.OrderMainStatus.PreHandle;
                    }
                    if (((orderStatus == -1) && (paymentStatus == 0)) && (shippingStatus == 0))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Cancel;
                    }
                    if (((orderStatus == -2) || (orderStatus == -3)) && ((paymentStatus == 0) && (shippingStatus == 0)))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Locking;
                    }
                    if (((orderStatus == 1) && (paymentStatus == 0)) && (shippingStatus == 0))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Handling;
                    }
                    if (((orderStatus == 1) && (paymentStatus == 0)) && (shippingStatus == 1))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Shipping;
                    }
                    if (((orderStatus == 1) && (paymentStatus == 0)) && (shippingStatus == 2))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Shiped;
                    }
                    if (((orderStatus == 2) && (paymentStatus == 2)) && (shippingStatus == 3))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Complete;
                    }
                    return preHandle;

                case "bank":
                    if (((orderStatus == 0) && (paymentStatus == 0)) && (shippingStatus == 0))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Paying;
                    }
                    if (((orderStatus == -1) && (paymentStatus == 0)) && (shippingStatus == 0))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Cancel;
                    }
                    if (((orderStatus == -2) || (orderStatus == -3)) && ((paymentStatus == 0) && (shippingStatus == 0)))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Locking;
                    }
                    if (((orderStatus == 0) && (paymentStatus == 2)) && (shippingStatus == 0))
                    {
                        preHandle = EnumHelper.OrderMainStatus.PreConfirm;
                    }
                    if (((orderStatus == 1) && (paymentStatus == 2)) && (shippingStatus == 0))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Handling;
                    }
                    if (((orderStatus == 1) && (paymentStatus == 2)) && (shippingStatus == 1))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Shipping;
                    }
                    if (((orderStatus == 1) && (paymentStatus == 2)) && (shippingStatus == 2))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Shiped;
                    }
                    if (((orderStatus == 2) && (paymentStatus == 2)) && (shippingStatus == 3))
                    {
                        preHandle = EnumHelper.OrderMainStatus.Complete;
                    }
                    return preHandle;
            }
            if (orderStatus == -1)
            {
                return EnumHelper.OrderMainStatus.Cancel;
            }
            if (((orderStatus == 0) && (paymentStatus == 0)) && (shippingStatus == 0))
            {
                preHandle = EnumHelper.OrderMainStatus.Paying;
            }
            if (((orderStatus == -1) && (paymentStatus == 0)) && (shippingStatus == 0))
            {
                preHandle = EnumHelper.OrderMainStatus.Cancel;
            }
            if (((orderStatus == -2) || (orderStatus == -3)) && ((paymentStatus == 0) && (shippingStatus == 0)))
            {
                preHandle = EnumHelper.OrderMainStatus.Locking;
            }
            if (((orderStatus == 1) && (paymentStatus == 2)) && (shippingStatus == 0))
            {
                preHandle = EnumHelper.OrderMainStatus.Handling;
            }
            if (((orderStatus == 1) && (paymentStatus == 2)) && (shippingStatus == 1))
            {
                preHandle = EnumHelper.OrderMainStatus.Shipping;
            }
            if (((orderStatus == 1) && (paymentStatus == 2)) && (shippingStatus == 2))
            {
                preHandle = EnumHelper.OrderMainStatus.Shiped;
            }
            if (((orderStatus == 2) && (paymentStatus == 2)) && (shippingStatus == 3))
            {
                preHandle = EnumHelper.OrderMainStatus.Complete;
            }
            return preHandle;
        }

        public int GetPaymentStatusCounts(int userid, int PaymentStatus)
        {
            return this.dal.GetPaymentStatusCounts(userid, PaymentStatus, -1);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public string GetWhereByStatus(EnumHelper.OrderMainStatus orderType)
        {
            switch (orderType)
            {
                case EnumHelper.OrderMainStatus.Paying:
                    return string.Format(" OrderStatus={0}  and PaymentStatus={1} and ShippingStatus={2} and PaymentGateway!='{3}'", new object[] { 0, 0, 0, EnumHelper.PaymentGateway.cod.ToString() });

                case EnumHelper.OrderMainStatus.PreHandle:
                    return string.Format(" OrderStatus={0}  and PaymentStatus={1} and ShippingStatus={2} and PaymentGateway='{3}'", new object[] { 0, 0, 0, EnumHelper.PaymentGateway.cod.ToString() });

                case EnumHelper.OrderMainStatus.Cancel:
                    return string.Format(" OrderStatus={0}  and PaymentStatus={1} and ShippingStatus={2} ", -1, 0, 0);

                case EnumHelper.OrderMainStatus.Locking:
                    return string.Format("  PaymentStatus={1} and ShippingStatus={2}  and (OrderStatus={0} or OrderStatus={3})  ", new object[] { -3, 0, 0, -2 });

                case EnumHelper.OrderMainStatus.PreConfirm:
                    return string.Format(" OrderStatus={0}  and PaymentStatus={1} and ShippingStatus={2} and PaymentGateway!='{3}'", new object[] { 0, 2, 0, EnumHelper.PaymentGateway.bank.ToString() });

                case EnumHelper.OrderMainStatus.Handling:
                    return string.Format(" OrderStatus={0}  and ShippingStatus={1} ", 1, 0);

                case EnumHelper.OrderMainStatus.Shipping:
                    return string.Format(" OrderStatus={0}  and  ShippingStatus={1}", 1, 1);

                case EnumHelper.OrderMainStatus.Shiped:
                    return string.Format(" OrderStatus={0}  and  ShippingStatus={1}", 1, 2);

                case EnumHelper.OrderMainStatus.Complete:
                    return string.Format(" OrderStatus={0}  and  ShippingStatus={1}", 2, 3);
            }
            return "";
        }

        public string GetWhereByStatus(int orderType)
        {
            switch (orderType)
            {
                case 1:
                    return string.Format(" OrderStatus={0}  and PaymentStatus={1} and ShippingStatus={2} and PaymentGateway!='{3}'", new object[] { 0, 0, 0, EnumHelper.PaymentGateway.cod.ToString() });

                case 2:
                    return string.Format(" OrderStatus={0}  and PaymentStatus={1} and ShippingStatus={2} and PaymentGateway='{3}'", new object[] { 0, 0, 0, EnumHelper.PaymentGateway.cod.ToString() });

                case 3:
                    return string.Format(" OrderStatus={0}  and PaymentStatus={1} and ShippingStatus={2} ", -1, 0, 0);

                case 4:
                    return string.Format("  PaymentStatus={1} and ShippingStatus={2}  and (OrderStatus={0} or OrderStatus={3})  ", new object[] { -3, 0, 0, -2 });

                case 5:
                    return string.Format(" OrderStatus={0}  and PaymentStatus={1} and ShippingStatus={2} and PaymentGateway!='{3}'", new object[] { 0, 2, 0, EnumHelper.PaymentGateway.bank.ToString() });

                case 6:
                    return string.Format(" OrderStatus={0}  and ShippingStatus={1} ", 1, 0);

                case 7:
                    return string.Format(" OrderStatus={0}  and  ShippingStatus={1}", 1, 1);

                case 8:
                    return string.Format(" OrderStatus={0}  and  ShippingStatus={1}", 1, 2);

                case 9:
                    return string.Format(" OrderStatus={0}  and  ShippingStatus={1}", 2, 3);
            }
            return "";
        }

        public void RemoveModelInfoCache(long OrderId)
        {
            DataCache.DeleteCache("GetModelInfoByCache-" + OrderId);
        }

        public bool ReturnStatus(long orderId)
        {
            return this.dal.ReturnStatus(orderId);
        }

        public bool SetOrderSuccess(long orderId)
        {
            return this.dal.SetOrderSuccess(orderId);
        }

        public bool Update(OrderInfo model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateOrderStatus(long orderId, int status)
        {
            return this.dal.UpdateOrderStatus(orderId, status);
        }

        public bool UpdateShipped(OrderInfo orderModel)
        {
            return this.dal.UpdateShipped(orderModel);
        }
    }
}

