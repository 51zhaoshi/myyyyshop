namespace Maticsoft.IDAL.Members
{
    using Maticsoft.Model.Members;
    using System;
    using System.Data;

    public interface IGuestbook
    {
        int Add(Guestbook model);
        Guestbook DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        bool Exists(int ID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        Guestbook GetModel(int ID);
        int GetRecordCount(string strWhere);
        bool Update(Guestbook model);
    }
}

