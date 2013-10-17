namespace Maticsoft.IDAL.Ms
{
    using Maticsoft.Model.Ms;
    using System;
    using System.Data;

    public interface IWeiBoTaskMsg
    {
        int Add(WeiBoTaskMsg model);
        int AddEx(WeiBoMsg model);
        WeiBoTaskMsg DataRowToModel(DataRow row);
        bool Delete(int WeiBoTaskId);
        bool DeleteList(string WeiBoTaskIdlist);
        bool Exists(int WeiBoTaskId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        WeiBoTaskMsg GetModel(int WeiBoTaskId);
        int GetRecordCount(string strWhere);
        bool RunTask(WeiBoTaskMsg model);
        bool Update(WeiBoTaskMsg model);
    }
}

