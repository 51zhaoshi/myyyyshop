namespace Maticsoft.IDAL.Ms
{
    using Maticsoft.Model.Ms;
    using System;
    using System.Data;

    public interface IEntryForm
    {
        int Add(EntryForm model);
        bool Delete(int Id);
        bool DeleteList(string Idlist);
        bool Exists(int Id);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        EntryForm GetModel(int Id);
        int GetRecordCount(string strWhere);
        bool Update(EntryForm model);
        bool UpdateList(string IDlist, string strWhere);
    }
}

