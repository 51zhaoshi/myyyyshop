namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;

    public interface IProductQA
    {
        int Add(ProductQA model);
        bool Delete(int QAId);
        bool DeleteList(string QAIdlist);
        bool Exists(int QAId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        ProductQA GetModel(int QAId);
        int GetRecordCount(string strWhere);
        bool SetStatus(string ids, int status);
        bool Update(ProductQA model);
    }
}

