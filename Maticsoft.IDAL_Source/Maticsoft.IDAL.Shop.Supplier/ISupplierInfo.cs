namespace Maticsoft.IDAL.Shop.Supplier
{
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Data;

    public interface ISupplierInfo
    {
        int Add(SupplierInfo model);
        SupplierInfo DataRowToModel(DataRow row);
        bool Delete(int SupplierId);
        bool DeleteList(string SupplierIdlist);
        bool Exists(int SupplierId);
        bool Exists(string Name);
        bool Exists(string Name, int SupplierID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        SupplierInfo GetModel(int SupplierId);
        int GetRecordCount(string strWhere);
        DataSet GetStatisticsSales(int supplierId, int year);
        DataSet GetStatisticsSupply(int supplierId);
        bool Update(SupplierInfo model);
        bool UpdateList(string IDlist, string strWhere);
    }
}

