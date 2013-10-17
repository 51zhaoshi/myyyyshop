namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface ISearchWordTop
    {
        int Add(SearchWordTop model);
        SearchWordTop DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        bool Exists(string HotWord);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListEx(string strWhere);
        SearchWordTop GetModel(int ID);
        int GetRecordCount(string strWhere);
        bool Update(SearchWordTop model);
    }
}

