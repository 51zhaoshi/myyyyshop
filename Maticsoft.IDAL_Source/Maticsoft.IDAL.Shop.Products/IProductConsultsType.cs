namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;

    public interface IProductConsultsType
    {
        int Add(ProductConsultsType model);
        bool Delete(int TypeId);
        bool DeleteList(string TypeIdlist);
        bool Exists(int TypeId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        ProductConsultsType GetModel(int TypeId);
        int GetRecordCount(string strWhere);
        bool Update(ProductConsultsType model);
    }
}

