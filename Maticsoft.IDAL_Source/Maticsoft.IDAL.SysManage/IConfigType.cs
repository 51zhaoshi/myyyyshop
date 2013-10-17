namespace Maticsoft.IDAL.SysManage
{
    using System;
    using System.Data;

    public interface IConfigType
    {
        int Add(string TypeName);
        bool Delete(int KeyType);
        bool DeleteList(string KeyTypelist);
        bool Exists(string TypeName);
        DataSet GetList(string strWhere);
        string GetTypeName(int KeyType);
        bool Update(int KeyType, string TypeName);
    }
}

