namespace Maticsoft.IDAL.Members
{
    using Maticsoft.Model.Members;
    using System;
    using System.Data;

    public interface IUserRank
    {
        int Add(UserRank model);
        UserRank DataRowToModel(DataRow row);
        bool Delete(int RankId);
        bool DeleteList(string RankIdlist);
        bool Exists(int RankId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        UserRank GetModel(int RankId);
        int GetRecordCount(string strWhere);
        string GetUserLevel(int grades);
        bool Update(UserRank model);
    }
}

