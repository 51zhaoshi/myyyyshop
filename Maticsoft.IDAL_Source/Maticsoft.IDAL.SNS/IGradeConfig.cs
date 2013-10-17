namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IGradeConfig
    {
        int Add(GradeConfig model);
        bool Delete(int GradeID);
        bool DeleteList(string GradeIDlist);
        bool Exists(int GradeID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        GradeConfig GetModel(int GradeID);
        int GetRecordCount(string strWhere);
        GradeConfig GetUserLevel(int? grades);
        bool Update(GradeConfig model);
    }
}

