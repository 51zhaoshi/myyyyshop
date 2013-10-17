namespace Maticsoft.IDAL.Shop.Supplier
{
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Data;

    public interface ISupplierThemes
    {
        int Add(SupplierThemes model);
        SupplierThemes DataRowToModel(DataRow row);
        bool Delete(int ThemeId);
        bool DeleteList(string ThemeIdlist);
        bool Exists(int ThemeId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        SupplierThemes GetModel(int ThemeId);
        int GetRecordCount(string strWhere);
        bool Update(SupplierThemes model);
    }
}

