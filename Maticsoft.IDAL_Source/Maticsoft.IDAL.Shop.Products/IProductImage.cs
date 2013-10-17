namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;

    public interface IProductImage
    {
        int Add(ProductImage model);
        bool Delete(int ProductImageId);
        bool Delete(long ProductId, int ProductImageId);
        bool DeleteList(string ProductImageIdlist);
        bool Exists(long ProductId, int ProductImageId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        ProductImage GetModel(int ProductImageId);
        int GetRecordCount(string strWhere);
        DataSet ProductImagesList(long productId);
        bool Update(ProductImage model);
    }
}

