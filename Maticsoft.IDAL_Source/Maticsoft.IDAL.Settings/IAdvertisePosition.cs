namespace Maticsoft.IDAL.Settings
{
    using Maticsoft.Model.Settings;
    using System;
    using System.Data;

    public interface IAdvertisePosition
    {
        int Add(AdvertisePosition model);
        bool Delete(int AdvPositionId);
        bool DeleteList(string AdvPositionIdlist);
        bool Exists(int AdvPositionId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        AdvertisePosition GetModel(int AdvPositionId);
        int GetRecordCount(string strWhere);
        bool Update(AdvertisePosition model);
    }
}

