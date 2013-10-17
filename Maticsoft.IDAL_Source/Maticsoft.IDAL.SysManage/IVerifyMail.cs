namespace Maticsoft.IDAL.SysManage
{
    using Maticsoft.Model.SysManage;
    using System;
    using System.Data;

    public interface IVerifyMail
    {
        bool Add(VerifyMail model);
        bool Delete(string KeyValue);
        bool DeleteList(string KeyValuelist);
        bool Exists(string KeyValue);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        VerifyMail GetModel(string KeyValue);
        int GetRecordCount(string strWhere);
        bool Update(VerifyMail model);
    }
}

