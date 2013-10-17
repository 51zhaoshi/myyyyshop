namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public interface IProductService
    {
        bool AddProduct(ProductInfo productInfo, out long ProductId);
        DataSet GetCompareProudctBasicInfo(string ids);
        DataSet GetCompareProudctInfo(string ids);
        bool ModifyProduct(ProductInfo productInfo);
    }
}

