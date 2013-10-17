namespace Maticsoft.IDAL.Poll
{
    using Maticsoft.Model.Poll;
    using System;
    using System.Data;

    public interface IUserPoll
    {
        void Add(UserPoll model);
        bool Add2(UserPoll model);
        DataSet GetList(string strWhere);
        DataSet GetListInnerJoin(int userid);
        int GetUserByForm(int FormID);
        void Update(UserPoll model);
    }
}

