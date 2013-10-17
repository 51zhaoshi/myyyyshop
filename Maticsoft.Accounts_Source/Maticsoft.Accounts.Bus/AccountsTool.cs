namespace Maticsoft.Accounts.Bus
{
    using Maticsoft.Accounts;
    using Maticsoft.Accounts.Data;
    using Maticsoft.Accounts.IData;
    using Maticsoft.Accounts.MySqlData;
    using System;
    using System.Data;

    public class AccountsTool
    {
        public static DataSet GetAllCategories()
        {
            IPermissionCategory category = PubConstant.IsSQLServer ? ((IPermissionCategory) new Maticsoft.Accounts.Data.PermissionCategory()) : ((IPermissionCategory) new Maticsoft.Accounts.MySqlData.PermissionCategory());
            return category.GetCategoryList();
        }

        public static DataSet GetAllPermissions()
        {
            IPermission permission = PubConstant.IsSQLServer ? ((IPermission) new Maticsoft.Accounts.Data.Permission()) : ((IPermission) new Maticsoft.Accounts.MySqlData.Permission());
            return permission.GetPermissionList();
        }

        public static DataSet GetPermissionsByCategory(int categoryID)
        {
            IPermissionCategory category = PubConstant.IsSQLServer ? ((IPermissionCategory) new Maticsoft.Accounts.Data.PermissionCategory()) : ((IPermissionCategory) new Maticsoft.Accounts.MySqlData.PermissionCategory());
            return category.GetPermissionsInCategory(categoryID);
        }

        public static DataSet GetRoleList()
        {
            IRole role = PubConstant.IsSQLServer ? ((IRole) new Maticsoft.Accounts.Data.Role()) : ((IRole) new Maticsoft.Accounts.MySqlData.Role());
            return role.GetRoleList();
        }

        public static DataSet GetRoleList(string idlist)
        {
            IRole role = PubConstant.IsSQLServer ? ((IRole) new Maticsoft.Accounts.Data.Role()) : ((IRole) new Maticsoft.Accounts.MySqlData.Role());
            return role.GetRoleList(idlist);
        }
    }
}

