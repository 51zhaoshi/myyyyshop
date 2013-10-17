namespace Maticsoft.IDAL.CMS
{
    using Maticsoft.Model.CMS;
    using System;
    using System.Data;

    public interface IPhotoAlbum
    {
        int Add(PhotoAlbum model);
        bool Delete(int AlbumID);
        bool DeleteList(string AlbumIDlist);
        bool Exists(int AlbumID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        int GetMaxSequence();
        PhotoAlbum GetModel(int AlbumID);
        int GetRecordCount(string strWhere);
        bool Update(PhotoAlbum model);
    }
}

