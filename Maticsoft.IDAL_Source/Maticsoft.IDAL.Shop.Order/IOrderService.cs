namespace Maticsoft.IDAL.Shop.Order
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Runtime.InteropServices;

    public interface IOrderService
    {
        bool CancelOrder(OrderInfo orderInfo, User currentUser = new User());
        long CreateOrder(OrderInfo orderInfo);
        bool PayForOrder(OrderInfo orderInfo, User currentUser = new User());
    }
}

