namespace Maticsoft.IDAL.Members
{
    using Maticsoft.Model.Members;
    using System;
    using System.Data;

    public interface IPointsDetail
    {
        int Add(PointsDetail model);
        bool AddDetail(PointsDetail model);
        bool Delete(int PointsDetailID);
        bool DeleteList(string PointsDetailIDlist);
        bool Exists(int PointsDetailID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        PointsDetail GetModel(int PointsDetailID);
        int GetRecordCount(string strWhere);
        bool Update(PointsDetail model);
    }
}

