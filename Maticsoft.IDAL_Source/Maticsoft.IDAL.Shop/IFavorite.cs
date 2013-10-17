namespace Maticsoft.IDAL.Shop
{
    using Maticsoft.Model.Shop;
    using System;
    using System.Data;

    public interface IFavorite
    {
        int Add(Favorite model);
        Favorite DataRowToModel(DataRow row);
        bool Delete(int FavoriteId);
        bool DeleteList(string FavoriteIdlist);
        bool Exists(int FavoriteId);
        bool Exists(long targetId, int userId, int type);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        Favorite GetModel(int FavoriteId);
        DataSet GetProductListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetRecordCount(string strWhere);
        bool Update(Favorite model);
    }
}

