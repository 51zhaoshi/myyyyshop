namespace Maticsoft.DALFactory
{
    using Maticsoft.IDAL.Shop.Order;
    using System;

    public sealed class DAShopOrder : DataAccessBase
    {
        public static IOrderAction CreateOrderAction()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Order.OrderAction";
            return (IOrderAction) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IOrderItems CreateOrderItem()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Order.OrderItems";
            return (IOrderItems) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IOrderLookupItems CreateOrderLookupItems()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Order.OrderLookupItems";
            return (IOrderLookupItems) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IOrderLookupList CreateOrderLookupList()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Order.OrderLookupList";
            return (IOrderLookupList) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IOrderOptions CreateOrderOptions()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Order.OrderOptions";
            return (IOrderOptions) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IOrderRemark CreateOrderRemark()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Order.OrderRemark";
            return (IOrderRemark) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IOrders CreateOrders()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Order.Orders";
            return (IOrders) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IOrderService CreateOrderService()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Order.OrderService";
            return (IOrderService) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IOrdersHistory CreateOrdersHistory()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Order.OrdersHistory";
            return (IOrdersHistory) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }
    }
}

