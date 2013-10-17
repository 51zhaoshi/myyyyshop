namespace Maticsoft.IDAL.Shop.Supplier
{
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Data;

    public interface ISupplierRankThemes
    {
        bool Add(SupplierRankThemes model);
        SupplierRankThemes DataRowToModel(DataRow row);
        bool Delete(int RankId, int ThemeId);
        bool Exists(int RankId, int ThemeId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        SupplierRankThemes GetModel(int RankId, int ThemeId);
        int GetRecordCount(string strWhere);
        bool Update(SupplierRankThemes model);
    }
}

