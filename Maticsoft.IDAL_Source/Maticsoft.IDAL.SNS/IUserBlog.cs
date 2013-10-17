namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IUserBlog
    {
        int Add(UserBlog model);
        UserBlog DataRowToModel(DataRow row);
        bool Delete(int BlogID);
        bool DeleteEx(int BlogID);
        bool DeleteList(string BlogIDlist);
        bool Exists(int BlogID);
        DataSet GetActiveUser(int top);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        UserBlog GetModel(int BlogID);
        int GetPvCount(int id);
        int GetRecordCount(string strWhere);
        bool Update(UserBlog model);
        bool UpdateCommentCount(int id);
        bool UpdateFavCount(int blogId);
        bool UpdatePvCount(int id);
        bool UpdateRec(int id, int Rec);
        bool UpdateRecList(string ids, int Rec);
        bool UpdateStatusList(string ids, int Status);
    }
}

