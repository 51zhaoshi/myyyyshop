namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public interface IUserAlbums
    {
        int Add(UserAlbums model);
        int AddEx(UserAlbums model, int TypeId);
        UserAlbums DataRowToModel(DataRow row);
        bool Delete(int AlbumID);
        bool DeleteAblumAction(int albumId);
        bool DeleteEx(int AlbumID, int TypeId, int UserId);
        bool DeleteList(string AlbumIDlist);
        bool Exists(int CreatedUserID, string AlbumName);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListForIndex(int TypeID, int Top, string orderby, int RecommandType = -1);
        DataSet GetListForIndexEx(int TypeID, int Top, string orderby);
        DataSet GetListForPage(int TypeID, string orderby, int startIndex, int endIndex);
        DataSet GetListForPageEx(int TypeID, string orderby, int startIndex, int endIndex);
        UserAlbums GetModel(int AlbumID);
        int GetRecordCount(int TypeID);
        int GetRecordCount(string strWhere);
        UserAlbums GetUserAlbum(int type, int pid, int UserId);
        DataSet GetUserFavAlbum(int UserId);
        bool Update(UserAlbums model);
        bool UpdateCommentCount(int ablumId);
        bool UpdateEx(UserAlbums model);
        bool UpdateIsRecommand(int IsRecommand, string IdList);
        bool UpdatePhotoCount();
        bool UpdatePvCount(int AlbumId);
        bool UpdateRecommand(int ablumId, EnumHelper.RecommendType recommendType);
    }
}

