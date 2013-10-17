namespace Maticsoft.Accounts.Bus
{
    using Maticsoft.Accounts;
    using Maticsoft.Accounts.IData;
    using System;
    using System.Data;

    public class UserType
    {
        private IUserType dal = (PubConstant.IsSQLServer ? ((IUserType) new UserType()) : ((IUserType) new UserType()));

        public void Add(string UserType, string Description)
        {
            this.dal.Add(UserType, Description);
        }

        public void Delete(string UserType)
        {
            this.dal.Delete(UserType);
        }

        public bool Exists(string UserType, string Description)
        {
            return this.dal.Exists(UserType, Description);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public string GetDescription(string UserType)
        {
            return this.dal.GetDescription(UserType);
        }

        public string GetDescriptionByCache(string UserType)
        {
            string cacheKey = "Accounts_UserTypeModel-" + UserType;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetDescription(UserType);
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
            return cache.ToString();
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public void Update(string UserType, string Description)
        {
            this.dal.Update(UserType, Description);
        }
    }
}

