namespace Maticsoft.IDAL.Shop.Supplier
{
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Data;

    public interface ISupplierRank
    {
        int Add(SupplierRank model);
        SupplierRank DataRowToModel(DataRow row);
        bool Delete(int RankId);
        bool DeleteList(string RankIdlist);
        bool Exists(int RankId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        SupplierRank GetModel(int RankId);
        int GetRecordCount(string strWhere);
        bool Update(SupplierRank model);
    }
}

