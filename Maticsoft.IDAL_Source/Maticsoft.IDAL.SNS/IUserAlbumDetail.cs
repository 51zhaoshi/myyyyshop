namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IUserAlbumDetail
    {
        int Add(UserAlbumDetail model);
        bool AddEx(UserAlbumDetail model);
        UserAlbumDetail DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool Delete(int AlbumID, int TargetID, int Type);
        bool DeleteEx(int AlbumID, int TargetId, int Type);
        bool DeleteList(string IDlist);
        bool Exists(int AlbumID, int TargetID, int Type);
        DataSet GetAlbumImgListByPage(int albumID, string orderby, int startIndex, int endIndex, int type);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        UserAlbumDetail GetModel(int ID);
        int GetRecordCount(string strWhere);
        int GetRecordCount4AlbumImgByAlbumID(int albumID, int type);
        List<string> GetThumbImageByAlbum(int AlbumID, int type);
        bool Update(UserAlbumDetail model);
    }
}

