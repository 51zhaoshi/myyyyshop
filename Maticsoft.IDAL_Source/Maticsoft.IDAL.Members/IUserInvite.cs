namespace Maticsoft.IDAL.Members
{
    using Maticsoft.Model.Members;
    using System;
    using System.Data;

    public interface IUserInvite
    {
        int Add(UserInvite model);
        UserInvite DataRowToModel(DataRow row);
        bool Delete(int InviteId);
        bool DeleteList(string InviteIdlist);
        bool Exists(int InviteId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        UserInvite GetModel(int InviteId);
        int GetRecordCount(string strWhere);
        bool Update(UserInvite model);
    }
}

