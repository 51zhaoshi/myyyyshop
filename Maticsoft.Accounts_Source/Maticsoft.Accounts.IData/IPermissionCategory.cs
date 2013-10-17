namespace Maticsoft.Accounts.IData
{
    using System;
    using System.Data;

    internal interface IPermissionCategory
    {
        int Create(string description);
        bool Delete(int CategoryID);
        bool ExistsPerm(int CategoryID);
        DataSet GetCategoryList();
        DataSet GetPermissionsInCategory(int categoryId);
        DataRow Retrieve(int categoryId);
    }
}

