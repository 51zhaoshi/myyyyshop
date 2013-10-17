namespace Maticsoft.IDAL.CMS
{
    using Maticsoft.Model.CMS;
    using System;
    using System.Data;

    public interface IComment
    {
        int Add(Comment model);
        int AddEx(Comment model);
        int AddTran(Comment model);
        Comment DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        bool Exists(int ID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListEx(int Top, string strWhere, string filedOrder);
        int GetMaxId();
        Comment GetModel(int ID);
        int GetRecordCount(string strWhere);
        bool Update(Comment model);
        bool UpdateList(string IDlist, int state);
    }
}

