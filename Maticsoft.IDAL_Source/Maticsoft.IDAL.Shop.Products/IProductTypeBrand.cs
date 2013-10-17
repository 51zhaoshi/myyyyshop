namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;

    public interface IProductTypeBrand
    {
        bool Add(ProductTypeBrand model);
        bool Delete(int? ProductTypeId, int? BrandId);
        bool Exists(int ProductTypeId, int BrandId);
        bool ExistsBrands(int BrandId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        ProductTypeBrand GetModel(int ProductTypeId, int BrandId);
        int GetRecordCount(string strWhere);
        bool Update(ProductTypeBrand model);
    }
}

