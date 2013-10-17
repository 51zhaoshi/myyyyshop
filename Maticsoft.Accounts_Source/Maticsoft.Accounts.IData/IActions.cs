namespace Maticsoft.Accounts.IData
{
    using System;
    using System.Data;

    internal interface IActions
    {
        int Add(string Description);
        int Add(string Description, int PermissionID);
        void AddPermission(int ActionID, int PermissionID);
        void AddPermission(string ActionIDs, int PermissionID);
        void ClearPermissions(int ActionID);
        void Delete(int ActionID);
        bool Exists(string Description);
        string GetDescription(int ActionID);
        DataSet GetList(string strWhere);
        void Update(int ActionID, string Description);
        void Update(int ActionID, string Description, int PermissionID);
    }
}

