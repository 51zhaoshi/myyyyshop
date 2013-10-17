namespace Maticsoft.IDAL.Ms
{
    using Maticsoft.Model.Ms;
    using System;
    using System.Data;

    public interface IRegionAreas
    {
        int Add(RegionAreas model);
        RegionAreas DataRowToModel(DataRow row);
        bool Delete(int AreaId);
        bool DeleteList(string AreaIdlist);
        bool Exists(int AreaId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        RegionAreas GetModel(int AreaId);
        int GetRecordCount(string strWhere);
        bool Update(RegionAreas model);
    }
}

