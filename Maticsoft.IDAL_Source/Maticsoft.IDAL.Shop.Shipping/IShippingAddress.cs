namespace Maticsoft.IDAL.Shop.Shipping
{
    using Maticsoft.Model.Shop.Shipping;
    using System;
    using System.Data;

    public interface IShippingAddress
    {
        int Add(ShippingAddress model);
        ShippingAddress DataRowToModel(DataRow row);
        bool Delete(int ShippingId);
        bool DeleteList(string ShippingIdlist);
        bool Exists(int ShippingId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        ShippingAddress GetModel(int ShippingId);
        int GetRecordCount(string strWhere);
        bool Update(ShippingAddress model);
    }
}

