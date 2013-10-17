namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface ISearchWordLog
    {
        int Add(SearchWordLog model);
        SearchWordLog DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        bool GetHotHotWordssList(int Top);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        SearchWordLog GetModel(int ID);
        int GetRecordCount(string strWhere);
        bool Update(SearchWordLog model);
    }
}

