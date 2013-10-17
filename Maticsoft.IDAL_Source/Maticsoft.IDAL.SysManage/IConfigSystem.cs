namespace Maticsoft.IDAL.SysManage
{
    using Maticsoft.Model.SysManage;
    using System;
    using System.Data;

    public interface IConfigSystem
    {
        int Add(string Keyname, string Value, string Description);
        int Add(string Keyname, string Value, string Description, ApplicationKeyType KeyType);
        void Delete(int ID);
        bool Exists(string Keyname);
        DataSet GetList(string strWhere);
        string GetValue(int ID);
        string GetValue(string Keyname);
        bool Update(string Keyname, string Value);
        bool Update(string Keyname, string Value, ApplicationKeyType KeyType);
        bool Update(string Keyname, string Value, string Description);
        void Update(int ID, string Keyname, string Value, string Description);
        void UpdateConnectionString(string connectionString);
    }
}

