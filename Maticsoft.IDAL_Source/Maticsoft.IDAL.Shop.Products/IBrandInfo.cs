namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public interface IBrandInfo
    {
        int Add(BrandInfo model);
        bool CreateBrandsAndTypes(BrandInfo model, DataProviderAction action);
        bool Delete(int BrandId);
        bool DeleteList(string BrandIdlist);
        bool Exists(int BrandId);
        DataSet GetBrandsByCateId(int cateId, bool IsChild, int Top);
        DataSet GetBrandsListByCateId(int? cateId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListByProductTypeId(int ProductTypeId, int top);
        DataSet GetListByProductTypeId(out int rowCount, out int pageCount, int ProductTypeId, int PageIndex, int PageSize, int action);
        int GetMaxDisplaySequence();
        int GetMaxId();
        BrandInfo GetModel(int BrandId);
        int GetRecordCount(string strWhere);
        BrandInfo GetRelatedProduct(int brandsId);
        BrandInfo GetRelatedProduct(int? brandsId, int? ProductTypeId);
        bool Update(BrandInfo model);
    }
}

