namespace Maticsoft.IDAL.Members
{
    using Maticsoft.Model.Members;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public interface IUsers
    {
        int Add(Users model);
        bool Delete(int UserID);
        bool DeleteByDepartmentID(int DepartmentID);
        bool DeleteEx(int userId);
        bool DeleteList(string UserIDlist);
        bool DeleteListByDepartmentID(string DepartmentIDlist);
        bool ExistByPhone(string Phone);
        bool Exists(int UserID);
        bool ExistsByEmail(string Email);
        bool ExistsNickName(string nickname);
        bool ExistsNickName(int userid, string nickname);
        int GetDefaultUserId();
        DataSet GetList(string strWhere);
        DataSet GetList(string type, string keyWord);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListEX(string keyWord);
        DataSet GetListEXByType(string type, string keyWord = "");
        int GetMaxId();
        Users GetModel(int UserID);
        string GetNickName(int userId);
        int GetRecordCount(string strWhere);
        DataSet GetSearchList(string type, string StrWhere = "");
        Users GetUserIdByDepartmentID(string DepartmentID);
        int GetUserIdByNickName(string NickName);
        string GetUserName(int userId);
        bool Update(Users model);
        bool UpdateActiveStatus(string Ids, int ActiveType);
        bool UpdateFansAndFellowCount();
    }
}

