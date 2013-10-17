namespace Maticsoft.IDAL.Shop.Order
{
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Data;

    public interface IOrderRemark
    {
        long Add(OrderRemark model);
        OrderRemark DataRowToModel(DataRow row);
        bool Delete(long RemarkId);
        bool DeleteList(string RemarkIdlist);
        bool Exists(long RemarkId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        OrderRemark GetModel(long RemarkId);
        int GetRecordCount(string strWhere);
        bool Update(OrderRemark model);
    }
}

