namespace Maticsoft.Accounts.IData
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    internal interface IUser
    {
        void AddAssignRole(int UserID, int RoleID);
        bool AddRole(int userId, int roleId);
        bool AssignRoleExists(int UserID, int RoleID);
        int Create(string userName, byte[] password, string nickName, string trueName, string sex, string phone, string email, int employeeID, string departmentID, bool activity, string userType, int style, int User_iCreator, DateTime User_dateValid, string User_cLang);
        int Create(string userName, byte[] password, string nickName, string trueName, string sex, string phone, string email, int employeeID, string departmentID, bool activity, string userType, int style, int User_iCreator, DateTime User_dateCreate, DateTime User_dateValid, string User_cLang);
        bool Delete(int userID);
        void DeleteAssignRole(int UserID, int RoleID);
        DataSet GetAssignRolesByUser(int UserID);
        ArrayList GetEffectivePermissionList(int userID);
        ArrayList GetEffectivePermissionListID(int userID);
        DataSet GetEffectivePermissionLists(int userID);
        DataSet GetNoAssignRolesByUser(int UserID);
        DataSet GetUserList(string key);
        DataSet GetUserList(string UserType, string DepartmentID, string Key);
        [Obsolete]
        ArrayList GetUserRoles(int userID);
        Dictionary<int, string> GetUserRoles4KeyValues(int userID);
        DataSet GetUsersByDepart(string DepartmentID, string Key);
        DataSet GetUsersByEmp(int EmployeeID);
        DataSet GetUsersByRole(int RoleID);
        DataSet GetUsersByType(string UserType, string Key);
        [Obsolete]
        bool HasUser(string userName);
        bool HasUserByEmail(string email);
        bool HasUserByNickName(string nickName);
        bool HasUserByPhone(string phone);
        bool HasUserByPhone(string phone, string userType);
        bool HasUserByUserName(string userName);
        bool RemoveRole(int userId, int roleId);
        DataRow Retrieve(int userID);
        DataRow Retrieve(string userName);
        DataRow RetrieveByNickName(string nickName);
        bool SetPassword(string UserName, byte[] encPassword);
        int TestPassword(int userID, byte[] encPassword);
        bool Update(int UserID, int User_iApprover, int User_iApproveState);
        bool Update(int UserID, int EmployeeID, string DepartmentID);
        bool Update(int userID, string userName, byte[] password, string nickName, string trueName, string sex, string phone, string email, int employeeID, string departmentID, bool activity, string userType, int style);
        bool UpdateActivity(int userId, bool activity);
        int ValidateLogin(string userName, byte[] encPassword);
        int ValidateLogin4Email(string email, byte[] encPassword);
    }
}

