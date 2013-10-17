namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;

    public interface IAccessoriesValue
    {
        DataSet AccessoriesByProductId(long productId);
        int Add(AccessoriesValue model);
        bool Delete(int AccessoriesValueId);
        bool DeleteList(string AccessoriesValueIdlist);
        bool Exists(int AccessoriesValueId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        AccessoriesValue GetModel(int AccessoriesValueId);
        int GetRecordCount(string strWhere);
        bool Update(AccessoriesValue model);
    }
}

