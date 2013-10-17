namespace Maticsoft.IDAL.Poll
{
    using Maticsoft.Model.Poll;
    using System;
    using System.Data;

    public interface IReply
    {
        int Add(Reply model);
        void Delete(int ID);
        bool Exists(int ID);
        DataSet GetList(string strWhere);
        int GetMaxId();
        Reply GetModel(int ID);
        void Update(Reply model);
    }
}

