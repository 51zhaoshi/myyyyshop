namespace Maticsoft.IDAL.Shop.Order
{
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Data;

    public interface IOrderLookupList
    {
        int Add(OrderLookupList model);
        OrderLookupList DataRowToModel(DataRow row);
        bool Delete(int LookupListId);
        bool DeleteList(string LookupListIdlist);
        bool Exists(int LookupListId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        OrderLookupList GetModel(int LookupListId);
        int GetRecordCount(string strWhere);
        bool Update(OrderLookupList model);
    }
}

