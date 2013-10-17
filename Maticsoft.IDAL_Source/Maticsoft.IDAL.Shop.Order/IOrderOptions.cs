namespace Maticsoft.IDAL.Shop.Order
{
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Data;

    public interface IOrderOptions
    {
        bool Add(OrderOptions model);
        OrderOptions DataRowToModel(DataRow row);
        bool Delete(int LookupListId, int LookupItemId, long OrderId);
        bool Exists(int LookupListId, int LookupItemId, long OrderId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        OrderOptions GetModel(int LookupListId, int LookupItemId, long OrderId);
        int GetRecordCount(string strWhere);
        bool Update(OrderOptions model);
    }
}

