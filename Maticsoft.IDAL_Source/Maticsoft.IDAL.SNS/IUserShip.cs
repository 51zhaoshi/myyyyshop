namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IUserShip
    {
        bool Add(UserShip model);
        bool AddAttention(int ActiveUserID, int PassiveUserID);
        bool CancelAttention(int ActiveUserID, int PassiveUserID);
        UserShip DataRowToModel(DataRow row);
        List<UserShip> DataTableToList(DataTable dt);
        List<UserShip> DataTableToListEx(DataTable dt);
        bool Delete(int ActiveUserID, int PassiveUserID);
        bool Exists(int ActiveUserID, int PassiveUserID);
        bool FellowUser(UserShip model);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByFansPage(int userid, string orderby, int startIndex, int endIndex);
        DataSet GetListByFellowsPage(int userid, string orderby, int startIndex, int endIndex);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        UserShip GetModel(int ActiveUserID, int PassiveUserID);
        int GetRecordCount(string strWhere);
        bool UnFellowUser(int Userid, int FellowUserId);
        bool Update(UserShip model);
    }
}

