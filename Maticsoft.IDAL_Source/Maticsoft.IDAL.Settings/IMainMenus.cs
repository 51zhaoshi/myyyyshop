namespace Maticsoft.IDAL.Settings
{
    using Maticsoft.Model.Settings;
    using System;
    using System.Data;

    public interface IMainMenus
    {
        int Add(MainMenus model);
        MainMenus DataRowToModel(DataRow row);
        bool Delete(int MenuID);
        bool DeleteList(string MenuIDlist);
        bool Exists(int MenuID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        MainMenus GetModel(int MenuID);
        int GetRecordCount(string strWhere);
        bool Update(MainMenus model);
    }
}

