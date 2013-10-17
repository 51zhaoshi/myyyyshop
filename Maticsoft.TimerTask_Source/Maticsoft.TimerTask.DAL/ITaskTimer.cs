namespace Maticsoft.TimerTask.DAL
{
    using Maticsoft.TimerTask.Model;
    using System;
    using System.Data;

    public interface ITaskTimer
    {
        int Add(TaskTimer model);
        TaskTimer DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        bool Exists(int ID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        TaskTimer GetModel(int ID);
        int GetRecordCount(string strWhere);
        bool Update(TaskTimer model);
    }
}

