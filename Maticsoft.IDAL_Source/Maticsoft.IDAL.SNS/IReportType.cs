namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IReportType
    {
        int Add(ReportType model);
        ReportType DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        bool Exists(string TypeName);
        bool Exists(int ID, string TypeName);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        ReportType GetModel(int ID);
        int GetRecordCount(string strWhere);
        bool Update(ReportType model);
        bool UpdateList(int Status, string IdList);
    }
}

