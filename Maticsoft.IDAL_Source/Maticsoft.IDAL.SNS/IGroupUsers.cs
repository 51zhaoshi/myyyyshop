namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IGroupUsers
    {
        bool Add(GroupUsers model);
        bool AddEx(GroupUsers model);
        GroupUsers DataRowToModel(DataRow row);
        bool Delete(int GroupID, int UserID);
        bool DeleteEx(int GroupId, int UserID);
        bool DeleteEx(int GroupId, string UserID);
        bool Exists(int GroupID, int UserID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        GroupUsers GetModel(int GroupID, int UserID);
        int GetRecordCount(string strWhere);
        bool Update(GroupUsers model);
        bool UpdateRecommand(int GroupID, int UserID, int Recommand);
        bool UpdateRole(int GroupID, int UserID, int Role);
        bool UpdateStatus(int GroupID, int UserID, int Status);
        bool UpdateStatusByTopicIds(string Ids, int Status);
        bool UpdateStatusByTopicReplyIds(string Ids, int Status);
    }
}

