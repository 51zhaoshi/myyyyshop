namespace Maticsoft.IDAL.Shop.Order
{
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Data;

    public interface IOrderLookupItems
    {
        int Add(OrderLookupItems model);
        OrderLookupItems DataRowToModel(DataRow row);
        bool Delete(int LookupItemId);
        bool DeleteList(string LookupItemIdlist);
        bool Exists(int LookupItemId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        OrderLookupItems GetModel(int LookupItemId);
        int GetRecordCount(string strWhere);
        bool Update(OrderLookupItems model);
    }
}

