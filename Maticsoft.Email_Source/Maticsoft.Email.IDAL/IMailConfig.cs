namespace Maticsoft.Email.IDAL
{
    using Maticsoft.Email.Model;
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
        void Update(MailConfig model);
    }
}

