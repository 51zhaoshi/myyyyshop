namespace Maticsoft.DALFactory
{
    using Maticsoft.Common;
    using System;
    using System.Reflection;

    public class DataAccess<t>
    {
        protected static readonly string AssemblyPath;

        static DataAccess()
        {
            DataAccess<t>.AssemblyPath = ConfigHelper.GetConfigString("DAL");
        }

        public static t Create(string ClassName)
        {
            string classNamespace = DataAccess<t>.AssemblyPath + "." + ClassName;
            return (t) DataAccess<t>.CreateObject(DataAccess<t>.AssemblyPath, classNamespace);
        }

        protected static object CreateObject(string AssemblyPath, string classNamespace)
        {
            object cache = DataCache.GetCache(classNamespace);
            if (cache == null)
            {
                try
                {
                    cache = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
                    DataCache.SetCache(classNamespace, cache);
                }
                catch
                {
                }
            }
            return cache;
        }

        protected static object CreateObjectNoCache(string AssemblyPath, string classNamespace)
        {
            try
            {
                return Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
            }
            catch
            {
                return null;
            }
        }
    }
}

