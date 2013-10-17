namespace Maticsoft.IDAL.Ms
{
    using Maticsoft.Model.Ms;
    using System;
    using System.Data;

    public interface IEmailTemplet
    {
        int Add(EmailTemplet model);
        EmailTemplet DataRowToModel(DataRow row);
        bool Delete(int TempletId);
        bool DeleteList(string TempletIdlist);
        bool Exists(int TempletId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        EmailTemplet GetModel(int TempletId);
        int GetRecordCount(string strWhere);
        bool Update(EmailTemplet model);
    }
}

