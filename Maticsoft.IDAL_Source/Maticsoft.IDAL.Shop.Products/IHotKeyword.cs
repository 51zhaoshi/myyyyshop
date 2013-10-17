namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;

    public interface IHotKeyword
    {
        int Add(HotKeyword model);
        bool Delete(int Id);
        bool DeleteList(string Idlist);
        bool Exists(int Id);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListLeftjoinCategories(string strWhere);
        int GetMaxId();
        HotKeyword GetModel(int Id);
        int GetRecordCount(string strWhere);
        bool Update(HotKeyword model);
    }
}

