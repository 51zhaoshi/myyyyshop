namespace Maticsoft.IDAL.Members
{
    using Maticsoft.Model.Members;
    using System;
    using System.Data;

    public interface IUsersExp
    {
        bool Add(UsersExpModel model);
        bool Add(int userId);
        bool AddEx(UsersExpModel model, int inviteuid, string inviteNick, int pointScore);
        bool Delete(int UserID);
        bool DeleteList(string UserIDlist);
        DataSet GetAllEmpByUserId(int userId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        UsersExpModel GetModel(int UserID);
        int GetRecordCount(string strWhere);
        decimal GetUserBalance(int UserId);
        int GetUserCountByKeyWord(string NickName);
        DataSet GetUserList(int Top, string strWhere, string filedOrder);
        DataSet GetUserListByKeyWord(string NickName, string orderby, int startIndex, int endIndex);
        int GetUserRankId(int UserId);
        bool Update(UsersExpModel model);
        bool UpdateAblumsCount();
        bool UpdateFavouritesCount();
        bool UpdateIsDPI(string userIds, int status);
        bool UpdatePhoneAndPay(int userid, string account, string phone);
        bool UpdateProductCount();
        bool UpdateShareCount();
    }
}

