namespace Maticsoft.IDAL.Settings
{
    using Maticsoft.Model.Settings;
    using System;
    using System.Data;

    public interface IFriendlyLink
    {
        int Add(FriendlyLink model);
        FriendlyLink DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        bool Exists(int ID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        int GetMaxId();
        FriendlyLink GetModel(int ID);
        bool Update(FriendlyLink model);
        bool UpdateList(string IDlist, string strWhere);
    }
}

