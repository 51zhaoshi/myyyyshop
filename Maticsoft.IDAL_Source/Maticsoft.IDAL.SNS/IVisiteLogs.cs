namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IVisiteLogs
    {
        int Add(VisiteLogs model);
        VisiteLogs DataRowToModel(DataRow row);
        bool Delete(int VisitID);
        bool DeleteList(string VisitIDlist);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        VisiteLogs GetModel(int VisitID);
        int GetRecordCount(string strWhere);
        bool Update(VisiteLogs model);
    }
}

