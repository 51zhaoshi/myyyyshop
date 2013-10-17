namespace Maticsoft.Accounts.Bus
{
    using Maticsoft.Accounts;
    using Maticsoft.Accounts.IData;
    using System;
    using System.Collections;
    using System.Data;

    public class Actions
    {
        private IActions dal = (PubConstant.IsSQLServer ? ((IActions) new Actions()) : ((IActions) new Actions()));

        public int Add(string Description)
        {
            return this.dal.Add(Description);
        }

        public int Add(string Description, int PermissionID)
        {
            return this.dal.Add(Description, PermissionID);
        }

        public void AddPermission(int ActionID, int PermissionID)
        {
            this.dal.AddPermission(ActionID, PermissionID);
        }

        public void AddPermission(string ActionIDs, int PermissionID)
        {
            this.dal.AddPermission(ActionIDs, PermissionID);
        }

        public void ClearPermissions(int ActionID)
        {
            this.dal.ClearPermissions(ActionID);
        }

        public void Delete(int ActionID)
        {
            this.dal.Delete(ActionID);
        }

        public bool Exists(string Description)
        {
            return this.dal.Exists(Description);
        }

        public string GetDescription(int ActionID)
        {
            return this.dal.GetDescription(ActionID);
        }

        public Hashtable GetHashList()
        {
            DataSet list = this.dal.GetList("");
            Hashtable hashtable = new Hashtable();
            if ((list.Tables.Count > 0) && (list.Tables[0] != null))
            {
                foreach (DataRow row in list.Tables[0].Rows)
                {
                    string key = row["ActionID"].ToString();
                    string str2 = row["PermissionID"].ToString();
                    hashtable.Add(key, str2);
                }
            }
            return hashtable;
        }

        public Hashtable GetHashListByCache()
        {
            string cacheKey = "ActionsPermissionHashList";
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetHashList();
                    if (cache != null)
                    {
                        int configInt = ConfigHelper.GetConfigInt("CacheTime");
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) configInt), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Hashtable) cache;
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public void Update(int ActionID, string Description)
        {
            this.dal.Update(ActionID, Description);
        }

        public void Update(int ActionID, string Description, int PermissionID)
        {
            this.dal.Update(ActionID, Description, PermissionID);
        }
    }
}

