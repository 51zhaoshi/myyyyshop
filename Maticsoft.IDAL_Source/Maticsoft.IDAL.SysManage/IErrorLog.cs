namespace Maticsoft.IDAL.SysManage
{
    using Maticsoft.Model.SysManage;
    using System;
    using System.Data;

    public interface IErrorLog
    {
        int Add(ErrorLog model);
        void Delete(int ID);
        void Delete(string IDList);
        void DeleteByDate(DateTime dtDateBefore);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        ErrorLog GetModel(int ID);
        void Update(ErrorLog model);
    }
}

