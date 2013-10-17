namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IUserFavAlbum
    {
        int Add(UserFavAlbum model);
        UserFavAlbum DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool Delete(int AlbumID, int UserID);
        bool DeleteList(string IDlist);
        bool Exists(int AlbumID, int UserID);
        int FavAlbum(int AlbumId, int UserId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        UserFavAlbum GetModel(int ID);
        int GetRecordCount(string strWhere);
        int UnFavAlbum(int AlbumId, int UserId);
        bool Update(UserFavAlbum model);
    }
}

