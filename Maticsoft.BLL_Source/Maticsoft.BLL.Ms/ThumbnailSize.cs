namespace Maticsoft.BLL.Ms
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Ms;
    using Maticsoft.Model.Ms;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class ThumbnailSize
    {
        private readonly IThumbnailSize dal = DAMs.CreateThumbnailSize();

        public bool Add(Maticsoft.Model.Ms.ThumbnailSize model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Ms.ThumbnailSize> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Ms.ThumbnailSize> list = new List<Maticsoft.Model.Ms.ThumbnailSize>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Ms.ThumbnailSize item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string ThumName)
        {
            return this.dal.Delete(ThumName);
        }

        public bool DeleteList(string ThumNamelist)
        {
            return this.dal.DeleteList(ThumNamelist);
        }

        public bool Exists(string ThumName)
        {
            return this.dal.Exists(ThumName);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public static string GetCloudName(string ThumName)
        {
            Maticsoft.Model.Ms.ThumbnailSize modelByCache = new Maticsoft.BLL.Ms.ThumbnailSize().GetModelByCache(ThumName);
            if (modelByCache != null)
            {
                return modelByCache.CloudSizeName;
            }
            return "";
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

        public Maticsoft.Model.Ms.ThumbnailSize GetModel(string ThumName)
        {
            return this.dal.GetModel(ThumName);
        }

        public Maticsoft.Model.Ms.ThumbnailSize GetModelByCache(string ThumName)
        {
            string cacheKey = "ThumbnailSizeModel-" + ThumName;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ThumName);
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
            return (Maticsoft.Model.Ms.ThumbnailSize) cache;
        }

        public List<Maticsoft.Model.Ms.ThumbnailSize> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public static List<Maticsoft.Model.Ms.ThumbnailSize> GetThumSizeList(EnumHelper.AreaType type, string Theme = "")
        {
            string cacheKey = "GetThumSizeList-" + ((int) type) + Theme;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                Maticsoft.BLL.Ms.ThumbnailSize size = new Maticsoft.BLL.Ms.ThumbnailSize();
                try
                {
                    string strWhere = " Type=" + ((int) type);
                    if (!string.IsNullOrWhiteSpace(Theme))
                    {
                        strWhere = strWhere + " and (Theme='" + Theme + "' or Theme='')";
                    }
                    cache = size.GetModelList(strWhere);
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
            return (List<Maticsoft.Model.Ms.ThumbnailSize>) cache;
        }

        public bool Update(Maticsoft.Model.Ms.ThumbnailSize model)
        {
            return this.dal.Update(model);
        }
    }
}

