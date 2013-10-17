namespace Maticsoft.DALFactory
{
    using Maticsoft.IDAL.SysManage;
    using System;

    public sealed class DASysManage : DataAccessBase
    {
        public static IConfigSystem CreateConfigSystem()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SysManage.ConfigSystem";
            return (IConfigSystem) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IConfigType CreateConfigType()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SysManage.ConfigType";
            return (IConfigType) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IErrorLog CreateErrorLog()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SysManage.ErrorLog";
            return (IErrorLog) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IMultiLanguage CreateMultiLanguage()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SysManage.MultiLanguage";
            return (IMultiLanguage) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ISysTree CreateSysTree()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SysManage.SysTree";
            return (ISysTree) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ITaskQueue CreateTaskQueue()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SysManage.TaskQueue";
            return (ITaskQueue) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ITreeFavorite CreateTreeFavorite()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SysManage.TreeFavorite";
            return (ITreeFavorite) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IUserLog CreateUserLog()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SysManage.UserLog";
            return (IUserLog) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IVerifyMail CreateVerifyMail()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".SysManage.VerifyMail";
            return (IVerifyMail) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }
    }
}

