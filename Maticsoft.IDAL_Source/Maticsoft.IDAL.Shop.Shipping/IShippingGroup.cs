namespace Maticsoft.IDAL.Shop.Shipping
{
    using Maticsoft.Model.Shop.Shipping;
    using System;
    using System.Data;

    public interface IShippingGroup
    {
        int Add(ShippingGroup model);
        ShippingGroup DataRowToModel(DataRow row);
        bool Delete(int GroupId);
        bool DeleteList(string GroupIdlist);
        bool Exists(int GroupId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        ShippingGroup GetModel(int GroupId);
        int GetRecordCount(string strWhere);
        bool Update(ShippingGroup model);
    }
}

