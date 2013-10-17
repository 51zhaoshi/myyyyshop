namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;

    public interface IProductCategories
    {
        bool Add(ProductCategories model);
        bool Delete(long produtId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        ProductCategories GetModel(long produtId);
        int GetRecordCount(string strWhere);
    }
}

