namespace Maticsoft.IDAL.Members
{
    using Maticsoft.Model.Members;
    using System;
    using System.Data;

    public interface IUsersApprove
    {
        int Add(UsersApprove model);
        bool BatchUpdate(string ids, string status);
        bool Delete(int ApproveID);
        bool DeleteByUserId(int userId);
        bool DeleteList(string ApproveIDlist);
        bool Exists(int ApproveID);
        DataSet GetApproveList(string strWhere);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        UsersApprove GetModel(int ApproveID);
        UsersApprove GetModelByUserID(int UserID);
        int GetRecordCount(string strWhere);
        bool Update(UsersApprove model);
    }
}

