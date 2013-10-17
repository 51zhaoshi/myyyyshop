namespace Maticsoft.Accounts.IData
{
    using System;
    using System.Data;

    internal interface IUserType
    {
        void Add(string UserType, string Description);
        void Delete(string UserType);
        bool Exists(string UserType, string Description);
        string GetDescription(string UserType);
        DataSet GetList(string strWhere);
        void Update(string UserType, string Description);
    }
}

