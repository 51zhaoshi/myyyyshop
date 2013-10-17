namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IGroupTopicFav
    {
        int Add(GroupTopicFav model);
        bool AddEx(GroupTopicFav model);
        GroupTopicFav DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        bool Exists(int TopicID, int CreatedUserID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        GroupTopicFav GetModel(int ID);
        int GetRecordCount(string strWhere);
        bool Update(GroupTopicFav model);
    }
}

