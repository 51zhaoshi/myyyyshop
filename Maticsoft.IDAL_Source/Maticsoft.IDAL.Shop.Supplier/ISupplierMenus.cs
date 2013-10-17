namespace Maticsoft.IDAL.Shop.Supplier
{
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Data;

    public interface ISupplierMenus
    {
        int Add(SupplierMenus model);
        SupplierMenus DataRowToModel(DataRow row);
        bool Delete(int MenuId);
        bool DeleteList(string MenuIdlist);
        bool Exists(int MenuId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        SupplierMenus GetModel(int MenuId);
        int GetRecordCount(string strWhere);
        bool Update(SupplierMenus model);
    }
}

