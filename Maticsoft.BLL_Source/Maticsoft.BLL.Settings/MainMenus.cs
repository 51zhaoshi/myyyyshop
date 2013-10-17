namespace Maticsoft.BLL.Settings
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Settings;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.Settings;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class MainMenus
    {
        private readonly IMainMenus dal = DASettings.CreateMainMenus();

        public int Add(Maticsoft.Model.Settings.MainMenus model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Settings.MainMenus> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Settings.MainMenus> list = new List<Maticsoft.Model.Settings.MainMenus>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Settings.MainMenus item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int MenuID)
        {
            return this.dal.Delete(MenuID);
        }

        public bool DeleteList(string MenuIDlist)
        {
            return this.dal.DeleteList(MenuIDlist);
        }

        public bool Exists(int MenuID)
        {
            return this.dal.Exists(MenuID);
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

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public List<Maticsoft.Model.Settings.MainMenus> GetMenusByArea(Maticsoft.Model.Ms.EnumHelper.AreaType area, string theme = "")
        {
            string str = " IsUsed=1 and NavArea=" + ((int) area);
            if (!string.IsNullOrWhiteSpace(theme))
            {
                str = str + " and (NavTheme='" + theme + "' or NavTheme='')";
            }
            return this.GetModelList(str + " order by Sequence");
        }

        public List<Maticsoft.Model.Settings.MainMenus> GetMenusByAreaByCacle(Maticsoft.Model.Ms.EnumHelper.AreaType area, string theme = "")
        {
            string cacheKey = "GetMenusByAreaByCacle-" + area + theme;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetMenusByArea(area, theme);
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
            return (List<Maticsoft.Model.Settings.MainMenus>) cache;
        }

        public Maticsoft.Model.Settings.MainMenus GetModel(int MenuID)
        {
            return this.dal.GetModel(MenuID);
        }

        public Maticsoft.Model.Settings.MainMenus GetModelByCache(int MenuID)
        {
            string cacheKey = "WebMenuConfigModel-" + MenuID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(MenuID);
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
            return (Maticsoft.Model.Settings.MainMenus) cache;
        }

        public List<Maticsoft.Model.Settings.MainMenus> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Settings.MainMenus model)
        {
            return this.dal.Update(model);
        }
    }
}

