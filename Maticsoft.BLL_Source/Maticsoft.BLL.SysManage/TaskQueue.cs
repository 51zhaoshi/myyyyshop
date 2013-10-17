namespace Maticsoft.BLL.SysManage
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.SysManage;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class TaskQueue
    {
        private readonly ITaskQueue dal = DASysManage.CreateTaskQueue();

        public bool Add(Maticsoft.Model.SysManage.TaskQueue model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SysManage.TaskQueue> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SysManage.TaskQueue> list = new List<Maticsoft.Model.SysManage.TaskQueue>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SysManage.TaskQueue item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int ID, int Type)
        {
            return this.dal.Delete(ID, Type);
        }

        public bool DeleteArticle()
        {
            return this.dal.DeleteArticle();
        }

        public bool DeleteTask(int Type)
        {
            return this.dal.DeleteTask(Type);
        }

        public bool Exists(int ID, int Type)
        {
            return this.dal.Exists(ID, Type);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<Maticsoft.Model.SysManage.TaskQueue> GetContinueTask(int type)
        {
            DataSet continueTask = this.dal.GetContinueTask(type);
            return this.DataTableToList(continueTask.Tables[0]);
        }

        public Maticsoft.Model.SysManage.TaskQueue GetLastModel(int type)
        {
            return this.dal.GetLastModel(type);
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.SysManage.TaskQueue GetModel(int ID, int Type)
        {
            return this.dal.GetModel(ID, Type);
        }

        public Maticsoft.Model.SysManage.TaskQueue GetModelByCache(int ID, int Type)
        {
            string cacheKey = "TaskQueueModel-" + ID + Type;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ID, Type);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.SysManage.TaskQueue) cache;
        }

        public List<Maticsoft.Model.SysManage.TaskQueue> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.SysManage.TaskQueue model)
        {
            return this.dal.Update(model);
        }
    }
}

