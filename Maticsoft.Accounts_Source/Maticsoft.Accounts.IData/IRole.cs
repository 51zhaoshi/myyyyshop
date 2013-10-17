namespace Maticsoft.Accounts.IData
{
    using System;
    using System.Data;

    internal interface IRole
    {
        void AddPermission(int roleId, int permissionId);
        void ClearPermissions(int roleId);
        int Create(string description);
        bool Delete(int roleId);
        DataSet GetRoleList();
        DataSet GetRoleList(string idlist);
        void RemovePermission(int roleId, int permissionId);
        DataRow Retrieve(int roleId);
        bool RoleExists(string Description);
        bool Update(int roleId, string description);
    }
}

