namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IFellowTopics
    {
        int Add(FellowTopics model);
        FellowTopics DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool Delete(string TopicTitle, int CreatedUserId);
        bool DeleteList(string IDlist);
        bool Exists(string TopicTitle, int CreatedUserId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        FellowTopics GetModel(int ID);
        int GetRecordCount(string strWhere);
        bool Update(FellowTopics model);
    }
}

