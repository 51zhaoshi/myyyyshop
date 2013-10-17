namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IGuestBook
    {
        int Add(GuestBook model);
        GuestBook DataRowToModel(DataRow row);
        bool Delete(int GuestBookID);
        bool DeleteList(string GuestBookIDlist);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        GuestBook GetModel(int GuestBookID);
        int GetRecordCount(string strWhere);
        bool Update(GuestBook model);
    }
}

