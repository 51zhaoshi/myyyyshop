namespace Maticsoft.IDAL.Shop.Order
{
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Data;

    public interface IOrders
    {
        long Add(OrderInfo model);
        OrderInfo DataRowToModel(DataRow row);
        bool Delete(long OrderId);
        bool DeleteList(string OrderIdlist);
        bool Exists(long OrderId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        OrderInfo GetModel(long OrderId);
        int GetPaymentStatusCounts(int userid, int PaymentStatus, int OrderStatusCancel);
        int GetRecordCount(string strWhere);
        bool ReturnStatus(long orderId);
        bool SetOrderSuccess(long orderId);
        bool Update(OrderInfo model);
        bool UpdateOrderStatus(long orderId, int status);
        bool UpdateShipped(OrderInfo orderModel);
    }
}

