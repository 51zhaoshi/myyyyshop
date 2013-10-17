namespace Maticsoft.IDAL.CMS
{
    using Maticsoft.Model.CMS;
    using System;
    using System.Data;

    public interface IVideoAlbum
    {
        int Add(VideoAlbum model);
        bool Delete(int AlbumID);
        bool DeleteList(string AlbumIDlist);
        bool Exists(int AlbumID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListEx(string strWhere, string orderby);
        int GetMaxId();
        int GetMaxSequence();
        VideoAlbum GetModel(int AlbumID);
        VideoAlbum GetModelEx(int AlbumID);
        int GetRecordCount(string strWhere);
        bool Update(VideoAlbum model);
        bool UpdateList(string IDlist, string strWhere);
    }
}

