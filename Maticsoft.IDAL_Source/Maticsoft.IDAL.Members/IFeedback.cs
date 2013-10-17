namespace Maticsoft.IDAL.Members
{
    using Maticsoft.Model.Members;
    using System;
    using System.Data;

    public interface IFeedback
    {
        int Add(Feedback model);
        Feedback DataRowToModel(DataRow row);
        bool Delete(int FeedbackId);
        bool DeleteList(string FeedbackIdlist);
        bool Exists(int FeedbackId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        Feedback GetModel(int FeedbackId);
        int GetRecordCount(string strWhere);
        bool Update(Feedback model);
    }
}

