namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IComments
    {
        DataSet AblumComment(int ablumId, string strWhere);
        int Add(Comments model);
        int AddEx(Comments ComModel);
        Comments DataRowToModel(DataRow row);
        bool Delete(int CommentID);
        bool DeleteComment(int ablumId, int commentId);
        bool DeleteList(string CommentIDlist);
        bool DeleteListEx(string CommentIDlist);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        Comments GetModel(int CommentID);
        int GetRecordCount(string strWhere);
        bool Update(Comments model);
    }
}

