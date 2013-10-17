namespace Maticsoft.IDAL.Members
{
    using Maticsoft.Model.Members;
    using System;
    using System.Data;

    public interface IUserBind
    {
        int Add(UserBind model);
        UserBind DataRowToModel(DataRow row);
        bool Delete(int BindId);
        bool DeleteList(string BindIdlist);
        bool Exists(int BindId);
        bool Exists(int userId, string MediaUserID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        UserBind GetModel(int BindId);
        UserBind GetModel(int userId, int type);
        int GetRecordCount(string strWhere);
        bool Update(UserBind model);
        bool UpdateEx(UserBind model);
    }
}

