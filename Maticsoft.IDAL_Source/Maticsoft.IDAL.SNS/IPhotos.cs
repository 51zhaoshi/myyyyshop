namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public interface IPhotos
    {
        int Add(Photos model);
        Photos DataRowToModel(DataRow row);
        bool Delete(int PhotoID);
        bool DeleteEX(int PhotoID);
        bool DeleteList(string PhotoIDlist);
        DataSet DeleteListEx(string Ids, out int Result);
        bool DeleteListEX(string PhotoIds);
        bool Exists(int PhotoID);
        int GetCountEx(int type, int categoryId, string address);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListByPageEx(string strWhere, int CateId, string orderby, int startIndex, int endIndex);
        DataSet GetListByPageEx(int type, int categoryId, string address, string orderby, int startIndex, int endIndex);
        DataSet GetListEx(string strWhere, int CateId);
        DataSet GetListToReGen(string strWhere);
        Photos GetModel(int PhotoID);
        int GetNextID(int photoId, int albumId);
        DataSet GetPhotoUserIds(string ids);
        int GetPrevID(int photoId, int albumId);
        int GetRecordCount(string strWhere);
        int GetRecordCountEx(string strWhere, int CateId);
        DataSet GetZuiInList(int Type, int Top);
        bool Update(Photos model);
        bool UpdateCateList(string PhotoIds, int CateId);
        bool UpdatePvCount(int ProductID);
        bool UpdateRecomend(int PhotoID, int Recomend);
        bool UpdateRecomendList(string PhotoIds, int Recomend);
        bool UpdateRecommandState(int id, int State);
        bool UpdateStaticUrl(int photoId, string staticUrl);
        bool UpdateStatus(int PhotoID, int Status);
        DataSet UserUploadPhoto(int ablumId);
    }
}

