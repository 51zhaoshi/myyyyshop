namespace Maticsoft.Accounts.IData
{
    using System;
    using System.Data;

    internal interface IPermission
    {
        int Create(int categoryID, string description);
        bool Delete(int id);
        DataSet GetNoPermissionList(int roleId);
        DataSet GetPermissionList();
        DataSet GetPermissionList(int roleId);
        DataSet Retrieve(int permissionId);
        bool Update(int PermissionID, string description);
        void UpdateCategory(string PermissionIDlist, int CategoryID);
    }
}

