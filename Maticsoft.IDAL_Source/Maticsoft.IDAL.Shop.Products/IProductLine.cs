namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;

    public interface IProductLine
    {
        int Add(ProductLine model);
        bool Delete(int LineId);
        bool DeleteList(string LineIdlist);
        bool Exists(int LineId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        ProductLine GetModel(int LineId);
        int GetRecordCount(string strWhere);
        bool Update(ProductLine model);
    }
}

