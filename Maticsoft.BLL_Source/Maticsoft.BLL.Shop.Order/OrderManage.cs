namespace Maticsoft.BLL.Shop.Order
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.IDAL.Shop.Order;
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Runtime.InteropServices;

    public static class OrderManage
    {
        private static readonly Orders orderManage = new Orders();
        private static readonly IOrderService service = DAShopOrder.CreateOrderService();

        public static bool CancelOrder(OrderInfo orderInfo, User currentUser)
        {
            return service.CancelOrder(orderInfo, currentUser);
        }

        public static long CreateOrder(OrderInfo orderInfo)
        {
            return service.CreateOrder(orderInfo);
        }

        public static bool PayForOrder(OrderInfo orderInfo, User currentUser = new User())
        {
            if ((orderInfo.OrderItems == null) || (orderInfo.OrderItems.Count < 1))
            {
                orderInfo = orderManage.GetModelInfoByCache(orderInfo.OrderId);
            }
            if (orderInfo.HasChildren && (orderInfo.SubOrders.Count < 1))
            {
                orderInfo.SubOrders = orderManage.GetModelList(" ParentOrderId=" + orderInfo.OrderId);
            }
            return service.PayForOrder(orderInfo, currentUser);
        }
    }
}

