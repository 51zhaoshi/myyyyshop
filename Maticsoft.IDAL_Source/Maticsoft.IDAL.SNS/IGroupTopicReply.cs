namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IGroupTopicReply
    {
        int Add(GroupTopicReply model);
        int AddEx(GroupTopicReply TModel, Products PModel);
        GroupTopicReply DataRowToModel(DataRow row);
        bool Delete(int ReplyID);
        bool DeleteEx(int ReplyID);
        bool DeleteList(string ReplyIDlist);
        bool DeleteListEx(string TopicIDlist);
        int ForwardReply(GroupTopicReply TModel);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListEx(string strWhere);
        GroupTopicReply GetModel(int ReplyID);
        int GetRecordCount(string strWhere);
        bool Update(GroupTopicReply model);
        bool UpdateStatusList(string IdsStr, EnumHelper.TopicStatus status);
    }
}

