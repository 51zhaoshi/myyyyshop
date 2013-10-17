namespace Maticsoft.IDAL.Ms
{
    using Maticsoft.Model.Ms;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public interface IRegions
    {
        int Add(Regions model);
        Regions DataRowToModel(DataRow row);
        bool Delete(int RegionId);
        bool DeleteList(string RegionIdlist);
        bool Exists(int RegionId);
        DataSet GetAllCityList();
        DataSet GetCitys(int parentID);
        DataSet GetDistrictByParentId(int iParentId);
        DataSet GetList(string strWhere);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        Regions GetModel(int RegionId);
        DataTable GetParentID(int regionID);
        DataSet GetParentIDs(int regID, out int Count);
        string GetPath(int regid);
        DataSet GetPrivoceName();
        DataSet GetPrivoces();
        DataSet GetProvinces();
        int GetRecordCount(string strWhere);
        string GetRegionIDsByAreaId(int areaid);
        DataSet GetRegionName(string parentID);
        string GetRegionNameByRID(int RID);
        int GetRegPath(int? regid);
        bool Update(Regions model);
        bool UpdateAreaID(string regionlist, int AreaId);
    }
}

