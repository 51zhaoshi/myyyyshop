namespace Maticsoft.BLL.SysManage
{
    using Maticsoft.IDAL.SysManage;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Data;

    public class UserLog
    {
        private static IUserLog dal = DASysManage.CreateUserLog();

        public static void Delete(DateTime dtDateBefore)
        {
            dal.LogUserDelete(dtDateBefore);
        }

        public static void Delete(int iID)
        {
            dal.LogUserDelete(iID);
        }

        public static void Delete(string IdList)
        {
            dal.LogUserDelete(IdList);
        }

        public static DataSet GetAllList()
        {
            return dal.GetList("");
        }

        public static int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }

        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public static void LogUserAdd(Maticsoft.Model.SysManage.UserLog model)
        {
            dal.LogUserAdd(model);
        }
    }
}

