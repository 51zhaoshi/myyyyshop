namespace Maticsoft.IDAL.Poll
{
    using Maticsoft.Model.Poll;
    using System;
    using System.Data;

    public interface ITopics
    {
        int Add(Topics model);
        void Delete(int ID);
        bool DeleteList(string IDlist);
        bool Exists(int FormID, string Title);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        Topics GetModel(int ID);
        void Update(Topics model);
    }
}

