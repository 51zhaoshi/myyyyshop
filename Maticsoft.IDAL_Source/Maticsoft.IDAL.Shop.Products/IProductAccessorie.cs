namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;

    public interface IProductAccessorie
    {
        bool Add(ProductAccessorie model);
        bool Delete(long ProductId, int AccessoriesValueId);
        bool Exists(long ProductId, int AccessoriesValueId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        ProductAccessorie GetModel(long ProductId, int AccessoriesValueId);
        int GetRecordCount(string strWhere);
        bool Update(ProductAccessorie model);
    }
}

