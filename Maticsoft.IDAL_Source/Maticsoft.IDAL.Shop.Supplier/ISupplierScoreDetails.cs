namespace Maticsoft.IDAL.Shop.Supplier
{
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Data;

    public interface ISupplierScoreDetails
    {
        int Add(SupplierScoreDetails model);
        SupplierScoreDetails DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        bool Exists(int ID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        SupplierScoreDetails GetModel(int ID);
        int GetRecordCount(string strWhere);
        bool Update(SupplierScoreDetails model);
    }
}

