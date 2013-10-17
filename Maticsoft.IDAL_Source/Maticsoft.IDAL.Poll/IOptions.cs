namespace Maticsoft.IDAL.Poll
{
    using Maticsoft.Model.Poll;
    using System;
    using System.Data;

    public interface IOptions
    {
        int Add(Options model);
        void Delete(int ID);
        bool DeleteList(string ClassIDlist);
        bool Exists(int TopicID, string Name);
        DataSet GetCountList(int FormID);
        DataSet GetCountList(string strwhere);
        DataSet GetList(string strWhere);
        Options GetModel(int ID);
        void Update(Options model);
    }
}

