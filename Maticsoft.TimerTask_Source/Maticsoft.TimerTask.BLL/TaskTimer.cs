namespace Maticsoft.TimerTask.BLL
{
    using Maticsoft.Common;
    using Maticsoft.Components;
    using Maticsoft.TimerTask.DAL;
    using Maticsoft.TimerTask.Model;
    using Maticsoft.TimerTask.SQLServerDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public static class TaskTimer
    {
        private static ITaskTimer taskTimerDal;

        public static int Add(Maticsoft.TimerTask.Model.TaskTimer model)
        {
            return dal.Add(model);
        }

        public static List<Maticsoft.TimerTask.Model.TaskTimer> DataTableToList(DataTable dt)
        {
            List<Maticsoft.TimerTask.Model.TaskTimer> list = new List<Maticsoft.TimerTask.Model.TaskTimer>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.TimerTask.Model.TaskTimer item = dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public static bool Delete(int ID)
        {
            return dal.Delete(ID);
        }

        public static bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        public static bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        public static DataSet GetAllList()
        {
            return GetList("");
        }

        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public static DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        public static DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public static int GetMaxId()
        {
            return dal.GetMaxId();
        }

        public static Maticsoft.TimerTask.Model.TaskTimer GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        public static Maticsoft.TimerTask.Model.TaskTimer GetModelByCache(int ID)
        {
            string cacheKey = "TaskTimersModel-" + ID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = dal.GetModel(ID);
                    if (cache != null)
                    {
                        int configInt = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) configInt), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.TimerTask.Model.TaskTimer) cache;
        }

        public static List<Maticsoft.TimerTask.Model.TaskTimer> GetModelList(string strWhere)
        {
            return DataTableToList(dal.GetList(strWhere).Tables[0]);
        }

        public static int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }

        public static bool Update(Maticsoft.TimerTask.Model.TaskTimer model)
        {
            return dal.Update(model);
        }

        private static ITaskTimer dal
        {
            get
            {
                if (!MvcApplication.IsInstall)
                {
                    return null;
                }
                if (taskTimerDal == null)
                {
                    taskTimerDal = new Maticsoft.TimerTask.SQLServerDAL.TaskTimer();
                }
                return taskTimerDal;
            }
        }
    }
}

