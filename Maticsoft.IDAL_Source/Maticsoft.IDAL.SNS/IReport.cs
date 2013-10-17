namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IReport
    {
        int Add(Report model);
        Report DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        bool Exists(int ID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListEx(int Top, string strWhere, string filedOrder);
        Report GetModel(int ID);
        int GetRecordCount(string strWhere);
        bool Update(Report model);
        bool UpdateReportStatus(int status, int reportId);
        bool UpdateReportStatus(int status, string reportIds);
    }
}

