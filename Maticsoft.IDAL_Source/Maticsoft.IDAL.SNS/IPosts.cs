namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IPosts
    {
        int Add(Posts model);
        Posts AddBlogPost(Posts model, UserBlog blogModel, bool CreatePost);
        int AddForwardPost(Posts model);
        Posts AddPost(Posts model, int AlbumId, long Pid, int PhotoCateId, Products PModel, int RecommandStateInt, string photoAdress, string mapLng, string mapLat, bool CreatePost);
        Posts DataRowToModel(DataRow row);
        bool Delete(int PostID);
        bool DeleteEx(int PostID);
        bool DeleteList(string PostIDlist);
        bool DeleteListByNormalPost(string PostIDs);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        Posts GetModel(int PostID);
        DataSet GetPostUserIds(string ids);
        int GetRecordCount(string strWhere);
        bool Update(Posts model);
        bool UpdateCommentCount(int postId);
        bool UpdateFavCount(int postId);
        int UpdateForwardCount(string StrWhere);
        bool UpdateStatusList(string PostIds, int Status);
        bool UpdateToDel(int PostID);
    }
}

