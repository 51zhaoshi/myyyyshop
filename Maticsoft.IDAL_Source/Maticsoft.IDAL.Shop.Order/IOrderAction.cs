namespace Maticsoft.IDAL.Shop.Order
{
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Data;

    public interface IOrderAction
    {
        long Add(OrderAction model);
        OrderAction DataRowToModel(DataRow row);
        bool Delete(long ActionId);
        bool DeleteList(string ActionIdlist);
        bool Exists(long ActionId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        OrderAction GetModel(long ActionId);
        int GetRecordCount(string strWhere);
        bool Update(OrderAction model);
    }
}

