namespace Maticsoft.IDAL.Shop.Supplier
{
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Data;

    public interface ISupplierConfig
    {
        int Add(SupplierConfig model);
        SupplierConfig DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        bool Exists(int ID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        SupplierConfig GetModel(int ID);
        int GetRecordCount(string strWhere);
        bool Update(SupplierConfig model);
    }
}

