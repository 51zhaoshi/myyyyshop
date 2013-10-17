namespace Maticsoft.IDAL.Settings
{
    using Maticsoft.Model.Settings;
    using System;
    using System.Data;

    public interface IFilterWords
    {
        int Add(FilterWords model);
        FilterWords DataRowToModel(DataRow row);
        bool Delete(int FilterId);
        bool DeleteList(string FilterIdlist);
        bool Exists(int FilterId);
        FilterWords GetByWordPattern(string wordPattern);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        FilterWords GetModel(int FilterId);
        int GetRecordCount(string strWhere);
        bool Update(FilterWords model);
        bool UpdateActionType(string ids, int type, string replace);
    }
}

