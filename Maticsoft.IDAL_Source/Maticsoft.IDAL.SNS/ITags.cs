namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface ITags
    {
        int Add(Tags model);
        Tags DataRowToModel(DataRow row);
        bool Delete(int TagID);
        bool DeleteList(string TagIDlist);
        bool Exists(int TypeId, string TagName);
        DataSet GetHotTags(int top);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListEx(int Top, string strWhere, string filedOrder);
        Tags GetModel(int TagID);
        int GetRecordCount(string strWhere);
        bool Update(Tags model);
        bool UpdateIsRecommand(int IsRecommand, string IdList);
        bool UpdateStatus(int Status, string IdList);
    }
}

