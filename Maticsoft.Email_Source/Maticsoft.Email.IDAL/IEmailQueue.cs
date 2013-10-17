namespace Maticsoft.Email.IDAL
{
    using Maticsoft.Email.Model;
    using System;
    using System.Data;

    public interface IEmailQueue
    {
        bool Add(EmailQueue model);
        bool Delete();
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        EmailQueue GetModel();
        int GetRecordCount(string strWhere);
        bool PushEmailQueur(string uType, string uName, string EmailSubject, string EmailBody, string EmailFrom);
        bool Update(EmailQueue model);
    }
}

