namespace Maticsoft.BLL
{
    using Maticsoft.Common;
    using System;
    using System.Reflection;

    public class CreateManage
    {
        public const string ASSEMBLY_PATH = "Maticsoft.BLL";

        public static object CreateObject(string classNamespace)
        {
            object cache = DataCache.GetCache(classNamespace);
            if (cache == null)
            {
                try
                {
                    cache = Assembly.Load("Maticsoft.BLL").CreateInstance(classNamespace);
                    DataCache.SetCache(classNamespace, cache);
                }
                catch
                {
                }
            }
            return cache;
        }

        public static object CreateObjectNoCache(string classNamespace)
        {
            try
            {
                return Assembly.Load("Maticsoft.BLL").CreateInstance(classNamespace);
            }
            catch
            {
                return null;
            }
        }
    }
}

