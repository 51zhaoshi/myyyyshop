namespace Maticsoft.Accounts.Bus
{
    using Maticsoft.Accounts.IData;
    using System;

    public class PermissionCategories
    {
        private IPermissionCategory dalpc = (PubConstant.IsSQLServer ? ((IPermissionCategory) new PermissionCategory()) : ((IPermissionCategory) new PermissionCategory()));

        public int Create(string description)
        {
            return this.dalpc.Create(description);
        }

        public bool Delete(int pID)
        {
            return this.dalpc.Delete(pID);
        }

        public bool ExistsPerm(int CategoryID)
        {
            return this.dalpc.ExistsPerm(CategoryID);
        }
    }
}

