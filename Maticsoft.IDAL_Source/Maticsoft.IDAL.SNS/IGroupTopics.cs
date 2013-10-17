namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public interface IGroupTopics
    {
        int Add(GroupTopics model);
        int AddEx(GroupTopics Tmodel, Products PModel);
        GroupTopics DataRowToModel(DataRow row);
        bool Delete(int TopicID);
        bool DeleteEx(int TopicID);
        bool DeleteEx(int TopicID, out string ImageUrl);
        bool DeleteList(string TopicIDlist);
        bool Exists(int TopicID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListEx(int Top, string strWhere, string filedOrder);
        GroupTopics GetModel(int TopicID);
        int GetRecordCount(string strWhere);
        bool Update(GroupTopics model);
        bool UpdateAdminRecommand(int TopicId, bool IsAdmin);
        bool UpdatePVCount(int TopicId);
        bool UpdateRecommand(int TopicId, int Recommand);
        bool UpdateStatusList(string IdsStr, EnumHelper.TopicStatus status);
    }
}

