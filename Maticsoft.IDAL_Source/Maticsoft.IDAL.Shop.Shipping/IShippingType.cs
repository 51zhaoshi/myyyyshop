namespace Maticsoft.IDAL.Shop.Shipping
{
    using Maticsoft.Model.Shop.Shipping;
    using System;
    using System.Data;

    public interface IShippingType
    {
        int Add(ShippingType model);
        ShippingType DataRowToModel(DataRow row);
        bool Delete(int ModeId);
        bool DeleteList(string ModeIdlist);
        bool Exists(int ModeId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        ShippingType GetModel(int ModeId);
        int GetRecordCount(string strWhere);
        bool Update(ShippingType model);
    }
}

