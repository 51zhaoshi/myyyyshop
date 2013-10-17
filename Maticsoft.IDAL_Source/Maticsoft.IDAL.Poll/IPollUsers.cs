namespace Maticsoft.IDAL.Poll
{
    using Maticsoft.Model.Poll;
    using System;
    using System.Data;

    public interface IPollUsers
    {
        int Add(PollUsers model);
        void Delete(int UserID);
        bool DeleteList(string UserIDlist);
        bool Exists(int UserID);
        DataSet GetList(string strWhere);
        int GetMaxId();
        PollUsers GetModel(int UserID);
        int GetUserCount();
        bool Update(PollUsers model);
    }
}

