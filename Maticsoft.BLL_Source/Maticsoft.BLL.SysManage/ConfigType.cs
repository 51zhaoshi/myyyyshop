namespace Maticsoft.BLL.SysManage
{
    using Maticsoft.IDAL.SysManage;
    using System;
    using System.Data;

    public class ConfigType
    {
        private readonly IConfigType dal = DASysManage.CreateConfigType();

        public int Add(string TypeName)
        {
            return this.dal.Add(TypeName);
        }

        public bool Delete(int KeyType)
        {
            return this.dal.Delete(KeyType);
        }

        public bool DeleteList(string KeyTypelist)
        {
            return this.dal.DeleteList(KeyTypelist);
        }

        public bool Exists(string TypeName)
        {
            return this.dal.Exists(TypeName);
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public string GetTypeName(int KeyType)
        {
            return this.dal.GetTypeName(KeyType);
        }

        public bool Update(int KeyType, string TypeName)
        {
            return this.dal.Update(KeyType, TypeName);
        }
    }
}

