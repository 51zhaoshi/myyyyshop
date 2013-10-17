namespace Maticsoft.IDAL.Members
{
    using Maticsoft.Model.Members;
    using System;
    using System.Data;

    public interface IFeedbackType
    {
        int Add(FeedbackType model);
        FeedbackType DataRowToModel(DataRow row);
        bool Delete(int TypeId);
        bool DeleteList(string TypeIdlist);
        bool Exists(int TypeId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        FeedbackType GetModel(int TypeId);
        int GetRecordCount(string strWhere);
        bool Update(FeedbackType model);
    }
}

