namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IGroupTags
    {
        int Add(GroupTags model);
        bool Delete(int TagID);
        bool DeleteList(string TagIDlist);
        bool Exists(int TagID);
        bool Exists(string TagName);
        bool Exists(int TagID, string TagName);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        GroupTags GetModel(int TagID);
        int GetRecordCount(string strWhere);
        bool Update(GroupTags model);
        bool UpdateIsRecommand(int IsRecommand, string IdList);
        bool UpdateStatus(int Status, string IdList);
    }
}

