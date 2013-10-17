namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;

    public interface IProductStationMode
    {
        int Add(ProductStationMode model);
        bool Delete(int StationId);
        bool Delete(int productId, int type);
        bool DeleteByType(int type, int categoryId);
        bool DeleteList(string StationIdlist);
        bool Exists(int StationId);
        bool Exists(int productId, int type);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListByType(string strType);
        int GetMaxId();
        ProductStationMode GetModel(int StationId);
        int GetRecordCount(string strWhere);
        DataSet GetStationMode(int modeType, int categoryId, string pName);
        bool Update(ProductStationMode model);
    }
}

