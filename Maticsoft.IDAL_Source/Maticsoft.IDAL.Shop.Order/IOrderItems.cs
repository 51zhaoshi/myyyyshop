namespace Maticsoft.IDAL.Shop.Order
{
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Data;

    public interface IOrderItems
    {
        long Add(OrderItems model);
        OrderItems DataRowToModel(DataRow row);
        bool Delete(long ItemId);
        bool DeleteList(string ItemIdlist);
        bool Exists(long ItemId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        OrderItems GetModel(long ItemId);
        int GetRecordCount(string strWhere);
        DataSet GetSaleRecordByPage(long productId, string orderby, int startIndex, int endIndex);
        int GetSaleRecordCount(long productId);
        bool Update(OrderItems model);
    }
}

