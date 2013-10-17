namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IUserAlbumsType
    {
        bool Add(UserAlbumsType model);
        UserAlbumsType DataRowToModel(DataRow row);
        bool Delete(int AlbumsID, int TypeID);
        bool Exists(int AlbumsID, int TypeID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        UserAlbumsType GetModel(int AlbumsID, int TypeID);
        UserAlbumsType GetModelByUserId(int AlbumsID, int UserId);
        int GetRecordCount(string strWhere);
        bool Update(UserAlbumsType model);
        bool UpdateType(UserAlbumsType model);
    }
}

