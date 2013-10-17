namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public interface ISKUInfo
    {
        long Add(SKUInfo model);
        bool Delete(long SkuId);
        bool DeleteList(string SkuIdlist);
        bool Exists(long SkuId);
        bool Exists(string SkuCode);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        SKUInfo GetModel(long SkuId);
        SKUInfo GetModelBySKU(string sku);
        int GetRecordCount(string strWhere);
        DataSet GetSKUListByPage(string strWhere, string orderby, int startIndex, int endIndex, out int dataCount, long productId);
        int GetStockById(long productId);
        int GetStockBySKU(string SKU);
        DataSet PrductsSkuInfo(long prductId);
        bool Update(SKUInfo model);
    }
}

