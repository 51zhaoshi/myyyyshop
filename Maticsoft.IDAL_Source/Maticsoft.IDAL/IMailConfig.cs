namespace Maticsoft.IDAL
{
    using Maticsoft.Model;
    using System;
    using System.Data;

    public interface IMailConfig
    {
        int Add(MailConfig model);
        void Delete(int ID);
        bool Exists(int UserID, string Mailaddress);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        MailConfig GetModel();
        MailConfig GetModel(int ID);
        MailConfig GetModel(int? userId);
        void Update(MailConfig model);
    }
}

