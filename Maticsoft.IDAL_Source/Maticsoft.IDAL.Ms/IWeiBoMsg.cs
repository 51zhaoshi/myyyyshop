namespace Maticsoft.IDAL.Ms
{
    using Maticsoft.Model.Ms;
    using System;
    using System.Data;

    public interface IWeiBoMsg
    {
        int Add(WeiBoMsg model);
        WeiBoMsg DataRowToModel(DataRow row);
        bool Delete(int WeiBoId);
        bool DeleteList(string WeiBoIdlist);
        bool Exists(int WeiBoId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        WeiBoMsg GetModel(int WeiBoId);
        int GetRecordCount(string strWhere);
        bool Update(WeiBoMsg model);
    }
}

