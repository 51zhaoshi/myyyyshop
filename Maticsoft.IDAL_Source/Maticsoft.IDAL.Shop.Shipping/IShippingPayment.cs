namespace Maticsoft.IDAL.Shop.Shipping
{
    using Maticsoft.Model.Shop.Shipping;
    using System;
    using System.Data;

    public interface IShippingPayment
    {
        bool Add(ShippingPayment model);
        ShippingPayment DataRowToModel(DataRow row);
        bool Delete(int modeId);
        bool Delete(int ShippingModeId, int PaymentModeId);
        bool Exists(int ShippingModeId, int PaymentModeId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        ShippingPayment GetModel(int ShippingModeId, int PaymentModeId);
        int GetRecordCount(string strWhere);
        bool Update(ShippingPayment model);
    }
}

