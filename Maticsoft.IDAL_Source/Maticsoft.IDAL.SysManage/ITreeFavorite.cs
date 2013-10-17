namespace Maticsoft.IDAL.SysManage
{
    using System;
    using System.Data;

    public interface ITreeFavorite
    {
        int Add(int UserID, int NodeID);
        void Delete(int ID);
        void Delete(int UserID, int NodeID);
        void DeleteByUser(int UserID);
        bool Exists(int UserID, int NodeID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetMenuListByUser(int UserID);
        DataSet GetNodeIDsByUser(int UserID);
        void UpDate(int OrderID, int UserID, int NodeID);
    }
}

