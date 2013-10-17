namespace Maticsoft.IDAL.SysManage
{
    using Maticsoft.Model.SysManage;
    using System;
    using System.Data;

    public interface IUserLog
    {
        int GetCount(string strWhere);
        DataSet GetList(string strWhere);
        void LogDelete(DateTime dtDateBefore);
        void LogUserAdd(UserLog model);
        void LogUserDelete(DateTime dtDateBefore);
        void LogUserDelete(int ID);
        void LogUserDelete(string IdList);
    }
}

