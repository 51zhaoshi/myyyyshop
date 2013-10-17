namespace Maticsoft.Accounts.Bus
{
    using Maticsoft.Accounts;
    using Maticsoft.Accounts.Data;
    using Maticsoft.Accounts.IData;
    using Maticsoft.Accounts.MySqlData;
    using System;
    using System.Data;

    [Serializable]
    public class Role
    {
        private IRole dataRole;
        private string description;
        private DataSet nopermissions;
        private DataSet permissions;
        private int roleId;
        private DataSet users;

        public Role()
        {
            this.dataRole = PubConstant.IsSQLServer ? ((IRole) new Maticsoft.Accounts.Data.Role()) : ((IRole) new Maticsoft.Accounts.MySqlData.Role());
        }

        public Role(int currentRoleId)
        {
            this.dataRole = PubConstant.IsSQLServer ? ((IRole) new Maticsoft.Accounts.Data.Role()) : ((IRole) new Maticsoft.Accounts.MySqlData.Role());
            DataRow row = this.dataRole.Retrieve(currentRoleId);
            this.roleId = currentRoleId;
            if (row["Description"] != null)
            {
                this.description = (string) row["Description"];
            }
            IPermission permission = PubConstant.IsSQLServer ? ((IPermission) new Maticsoft.Accounts.Data.Permission()) : ((IPermission) new Maticsoft.Accounts.MySqlData.Permission());
            this.permissions = permission.GetPermissionList(currentRoleId);
            this.nopermissions = permission.GetNoPermissionList(currentRoleId);
            this.users = (PubConstant.IsSQLServer ? ((IUser) new Maticsoft.Accounts.Data.User()) : ((IUser) new Maticsoft.Accounts.MySqlData.User())).GetUsersByRole(currentRoleId);
        }

        public void AddPermission(int permissionId)
        {
            this.dataRole.AddPermission(this.roleId, permissionId);
        }

        public void ClearPermissions()
        {
            this.dataRole.ClearPermissions(this.roleId);
        }

        public int Create()
        {
            this.roleId = this.dataRole.Create(this.description);
            return this.roleId;
        }

        public bool Delete()
        {
            return this.dataRole.Delete(this.roleId);
        }

        public DataSet GetRoleList()
        {
            return this.dataRole.GetRoleList();
        }

        public void RemovePermission(int permissionId)
        {
            this.dataRole.RemovePermission(this.roleId, permissionId);
        }

        public bool RoleExists(string Description)
        {
            return this.dataRole.RoleExists(Description);
        }

        public bool Update()
        {
            return this.dataRole.Update(this.roleId, this.description);
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        public DataSet NoPermissions
        {
            get
            {
                return this.nopermissions;
            }
        }

        public DataSet Permissions
        {
            get
            {
                return this.permissions;
            }
        }

        public int RoleID
        {
            get
            {
                return this.roleId;
            }
            set
            {
                this.roleId = value;
            }
        }

        public DataSet Users
        {
            get
            {
                return this.users;
            }
        }
    }
}

