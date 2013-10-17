namespace Maticsoft.IDAL.Shop.Shipping
{
    using Maticsoft.Model.Shop.Shipping;
    using System;
    using System.Data;

    public interface IShippingRegions
    {
        bool Add(ShippingRegions model);
        ShippingRegions DataRowToModel(DataRow row);
        bool Delete(int ModeId, int RegionId);
        bool Exists(int ModeId, int RegionId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        ShippingRegions GetModel(int ModeId, int RegionId);
        int GetRecordCount(string strWhere);
        bool Update(ShippingRegions model);
    }
}

