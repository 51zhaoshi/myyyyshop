namespace Maticsoft.IDAL.SysManage
{
    using Maticsoft.Model.SysManage;
    using System;
    using System.Data;

    public interface ITaskQueue
    {
        bool Add(TaskQueue model);
        TaskQueue DataRowToModel(DataRow row);
        bool Delete(int ID, int Type);
        bool DeleteArticle();
        bool DeleteTask(int Type);
        bool Exists(int ID, int Type);
        DataSet GetContinueTask(int type);
        TaskQueue GetLastModel(int type);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        TaskQueue GetModel(int ID, int Type);
        int GetRecordCount(string strWhere);
        bool Update(TaskQueue model);
    }
}

