namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IGroups
    {
        int Add(Groups model);
        Groups DataRowToModel(DataRow row);
        bool Delete(int GroupID);
        bool DeleteList(string GroupIDlist);
        bool DeleteListEx(string GroupIDlist);
        bool Exists(string GroupName);
        bool Exists(string GroupName, int groupId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        Groups GetModel(int GroupID);
        int GetRecordCount(string strWhere);
        bool Update(Groups model);
        bool UpdateRecommand(int GroupId, int Recommand);
        bool UpdateStatusList(string IdsStr, EnumHelper.GroupStatus status);
    }
}

