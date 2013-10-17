namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IPostsTopics
    {
        bool Add(PostsTopics model);
        PostsTopics DataRowToModel(DataRow row);
        bool Delete(string Title);
        bool DeleteList(string Titlelist);
        bool Exists(string Title);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        PostsTopics GetModel(string Title);
        int GetRecordCount(string strWhere);
        bool Update(PostsTopics model);
    }
}

