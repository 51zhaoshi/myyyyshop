namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public interface IProductInfo
    {
        long Add(ProductInfo model);
        bool ChangeProductsCategory(string productIds, int categoryId);
        ProductInfo DataRowToModel(DataRow row);
        bool Delete(long ProductId);
        bool DeleteList(string ProductIdlist);
        DataSet DeleteProducts(string Ids, out int Result);
        bool Exists(long ProductId);
        bool Exists(string productCode);
        bool ExistsBrands(int BrandId);
        DataSet GetList(string strWhere);
        DataSet GetList(string strWhere, string DataField);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByCategoryIdSaleStatus(string strWhere);
        DataSet GetListByExport(int SaleStatus, string ProductName, int CategoryId, string SKU, int BrandId);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        ProductInfo GetModel(long ProductId);
        DataSet GetProductCommendListInfo(string strWhere, string orderBy, int startIndex, int endIndex, out int dataCount, long productId, int modeType);
        DataSet GetProductInfo(string strWhere);
        DataSet GetProductListByCategoryId(int? categoryId, string strWhere, string orderBy, int startIndex, int endIndex, out int dataCount);
        DataSet GetProductListByCategoryIdEx(int? categoryId, string strWhere, string orderBy, int startIndex, int endIndex, out int dataCount);
        DataSet GetProductListInfo(string strProductIds);
        DataSet GetProductListInfo(string strWhere, string orderBy, int startIndex, int endIndex, out int dataCount, long productId);
        string GetProductName(long productId);
        int GetProductNoRecCount(int categoryId, string pName, int modeType);
        DataSet GetProductNoRecList(int categoryId, string pName, int modeType, int startIdex, int endIndex);
        DataSet GetProductRanList(int top);
        DataSet GetProductRecList(ProductRecType type, int categoryId, int top);
        DataSet GetProductsByCid(int cid);
        int GetProductsCountEx(int Cid, int BrandId, string attrValues, string priceRange);
        DataSet GetProductsListEx(int Cid, int BrandId, string attrValues, string priceRange, string mod, int startIndex, int endIndex);
        DataSet GetProSaleModel(int id);
        int GetProSalesCount();
        DataSet GetProSalesList(int startIndex, int endIndex);
        int GetRecordCount(string strWhere);
        DataSet GetRecycleList(string strWhere);
        int GetSearchCountEx(int Cid, int BrandId, string keyWord, string priceRange);
        DataSet GetSearchListEx(int Cid, int BrandId, string keyWord, string priceRange, string mod, int startIndex, int endIndex);
        DataSet GetTableSchema();
        DataSet GetTableSchemaEx();
        int MaxSequence();
        int MaxSequence(string CategoryPath);
        DataSet RelatedProductSource(long productId, int top);
        bool RevertAll();
        DataSet SearchProducts(int cateId, ProductSearch model);
        int StockNum(long productId);
        bool Update(ProductInfo model);
        bool UpdateList(string IDlist, string strSetValue);
        bool UpdateLowestSalePrice(long productId, decimal price);
        bool UpdateMarketPrice(long productId, decimal price);
        bool UpdateProductName(long productId, string strSetValue);
        bool UpdateStatus(long productId, int SaleStatus);
    }
}

