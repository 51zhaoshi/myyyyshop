namespace Maticsoft.IDAL.Shop.Supplier
{
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Data;

    public interface ISupplierCategories
    {
        int Add(SupplierCategories model);
        SupplierCategories DataRowToModel(DataRow row);
        bool Delete(int CategoryId);
        bool DeleteList(string CategoryIdlist);
        bool Exists(int CategoryId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        SupplierCategories GetModel(int CategoryId);
        int GetRecordCount(string strWhere);
        bool Update(SupplierCategories model);
    }
}

