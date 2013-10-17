namespace Maticsoft.IDAL.Ms
{
    using Maticsoft.Model.Ms;
    using System;
    using System.Data;

    public interface IRegionRec
    {
        int Add(RegionRec model);
        RegionRec DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool Delete(int regionId, int type);
        bool DeleteList(string IDlist);
        bool Exists(int ID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        RegionRec GetModel(int ID);
        int GetRecordCount(string strWhere);
        bool Update(RegionRec model);
    }
}

