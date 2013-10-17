namespace Maticsoft.IDAL.Members
{
    using Maticsoft.Model.Members;
    using System;
    using System.Data;

    public interface IPointsLimit
    {
        int Add(PointsLimit model);
        bool Delete(int PointsLimitID);
        bool DeleteEX(int PointsLimitID);
        bool DeleteList(string PointsLimitIDlist);
        bool Exists(int PointsLimitID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        PointsLimit GetModel(int PointsLimitID);
        int GetRecordCount(string strWhere);
        bool Update(PointsLimit model);
    }
}

