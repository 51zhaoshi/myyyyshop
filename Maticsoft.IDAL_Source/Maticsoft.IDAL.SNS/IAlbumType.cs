namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IAlbumType
    {
        int Add(AlbumType model);
        AlbumType DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        bool Exists(string TypeName);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        AlbumType GetModel(int ID);
        int GetRecordCount(string strWhere);
        DataSet GetTypeList(int albumId);
        bool Update(AlbumType model);
        bool UpdateIsMenu(int IsMenu, string IdList);
        bool UpdateMenuIsShow(int MenuIsShow, string IdList);
        bool UpdateStatus(int Status, string IdList);
    }
}

