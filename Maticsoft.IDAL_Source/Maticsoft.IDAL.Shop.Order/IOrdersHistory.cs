namespace Maticsoft.IDAL.Shop.Order
{
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Data;

    public interface IOrdersHistory
    {
        bool Add(OrdersHistory model);
        OrdersHistory DataRowToModel(DataRow row);
        bool Delete(long OrderId);
        bool DeleteList(string OrderIdlist);
        bool Exists(long OrderId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        OrdersHistory GetModel(long OrderId);
        int GetRecordCount(string strWhere);
        bool Update(OrdersHistory model);
    }
}

