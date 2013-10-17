namespace Maticsoft.IDAL.Shop.Supplier
{
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Data;

    public interface ISuppProductCategories
    {
        bool Add(SuppProductCategories model);
        SuppProductCategories DataRowToModel(DataRow row);
        bool Delete(int CategoryId, long ProductId);
        bool Exists(int CategoryId, long ProductId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        SuppProductCategories GetModel(int CategoryId, long ProductId);
        int GetRecordCount(string strWhere);
        bool Update(SuppProductCategories model);
    }
}

