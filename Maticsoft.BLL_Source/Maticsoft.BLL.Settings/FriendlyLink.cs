namespace Maticsoft.BLL.Settings
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Settings;
    using Maticsoft.Model.Settings;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class FriendlyLink
    {
        private readonly IFriendlyLink dal = DASettings.CreateFriendlyLink();

        public int Add(Maticsoft.Model.Settings.FriendlyLink model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Settings.FriendlyLink> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Settings.FriendlyLink> list = new List<Maticsoft.Model.Settings.FriendlyLink>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Settings.FriendlyLink item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int ID)
        {
            return this.dal.Delete(ID);
        }

        public bool DeleteList(string IDlist)
        {
            return this.dal.DeleteList(IDlist);
        }

        public bool Exists(int ID)
        {
            return this.dal.Exists(ID);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Settings.FriendlyLink GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.Settings.FriendlyLink GetModelByCache(int ID)
        {
            string cacheKey = "FLinksModel-" + ID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ID);
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
            return (Maticsoft.Model.Settings.FriendlyLink) cache;
        }

        public List<Maticsoft.Model.Settings.FriendlyLink> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            if (DataSetTools.DataSetIsNull(list))
            {
                return null;
            }
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.Settings.FriendlyLink> GetModelList(int Top, int Type)
        {
            string cacheKey = "GetModelList-" + Top + Type;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    string strWhere = string.Format(" IsDisplay=1 AND TypeID={0} ", Type);
                    string filedOrder = " OrderID ";
                    cache = this.GetModelList(Top, strWhere, filedOrder);
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
            return (List<Maticsoft.Model.Settings.FriendlyLink>) cache;
        }

        public List<Maticsoft.Model.Settings.FriendlyLink> GetModelList(int Top, string strWhere, string filedOrder)
        {
            DataSet ds = this.GetList(Top, strWhere, filedOrder);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            return this.DataTableToList(ds.Tables[0]);
        }

        public bool Update(Maticsoft.Model.Settings.FriendlyLink model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateList(string IDlist, string strWhere)
        {
            return this.dal.UpdateList(IDlist, strWhere);
        }
    }
}

