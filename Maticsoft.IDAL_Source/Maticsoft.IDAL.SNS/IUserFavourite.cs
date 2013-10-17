namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IUserFavourite
    {
        int Add(UserFavourite model);
        bool AddEx(UserFavourite FavModell, int TopicId, int ReplyId);
        UserFavourite DataRowToModel(DataRow row);
        bool Delete(int FavouriteID);
        bool DeleteEx(int UserId, int TargetId, int Type);
        bool DeleteList(string FavouriteIDlist);
        bool Exists(int CreatedUserID, int Type, int TargetID);
        DataSet GetFavListByPage(int UserId, string orderby, int startIndex, int endIndex);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        UserFavourite GetModel(int FavouriteID);
        int GetRecordCount(string strWhere);
        bool Update(UserFavourite model);
    }
}

