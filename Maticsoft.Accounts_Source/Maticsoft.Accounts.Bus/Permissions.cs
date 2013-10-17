namespace Maticsoft.Accounts.Bus
{
    using Maticsoft.Accounts.IData;
    using System;
    using System.Data;

    [Serializable]
    public class Permissions
    {
        private int _categoryID;
        private string _description;
        private int _permissionID;
        private IPermission dalPermission = (PubConstant.IsSQLServer ? ((IPermission) new Permission()) : ((IPermission) new Permission()));

        public int Create(int pcID, string description)
        {
            return this.dalPermission.Create(pcID, description);
        }

        public bool Delete(int pID)
        {
            return this.dalPermission.Delete(pID);
        }

        public void GetPermissionDetails(int pID)
        {
            DataSet set = this.dalPermission.Retrieve(pID);
            if (set.Tables[0].Rows.Count > 0)
            {
                if (set.Tables[0].Rows[0]["PermissionID"] != null)
                {
                    this._permissionID = Convert.ToInt32(set.Tables[0].Rows[0]["PermissionID"]);
                }
                this._description = set.Tables[0].Rows[0]["Description"].ToString();
                if (set.Tables[0].Rows[0]["CategoryID"] != null)
                {
                    this._categoryID = Convert.ToInt32(set.Tables[0].Rows[0]["CategoryID"]);
                }
            }
        }

        public string GetPermissionName(int permissionId)
        {
            DataSet set = this.dalPermission.Retrieve(permissionId);
            if (set.Tables[0].Rows.Count == 0)
            {
                throw new Exception("找不到权限 （" + permissionId + "）");
            }
            return set.Tables[0].Rows[0]["Description"].ToString();
        }

        public bool Update(int pcID, string description)
        {
            return this.dalPermission.Update(pcID, description);
        }

        public void UpdateCategory(string PermissionIDlist, int CategoryID)
        {
            this.dalPermission.UpdateCategory(PermissionIDlist, CategoryID);
        }

        public int CategoryID
        {
            get
            {
                return this._categoryID;
            }
            set
            {
                this._categoryID = value;
            }
        }

        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        public int PermissionID
        {
            get
            {
                return this._permissionID;
            }
            set
            {
                this._permissionID = value;
            }
        }
    }
}

