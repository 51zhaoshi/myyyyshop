namespace Maticsoft.IDAL.CMS
{
    using Maticsoft.Model.CMS;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public interface IPhoto
    {
        int Add(Photo model);
        bool Delete(int PhotoID);
        bool DeleteList(string PhotoIDlist, out DataSet imageList);
        bool Exists(int PhotoID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListAroundPhotoId(int Top, int PhotoId, int ClassId);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListToReGen(string strWhere);
        int GetMaxId();
        int GetMaxSequence();
        Photo GetModel(int PhotoID);
        int GetRecordCount(string strWhere);
        bool Update(Photo model);
        bool UpdatePhotoAlbum(int AlbumID, int newAlbumId);
    }
}

