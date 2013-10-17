namespace Maticsoft.BLL.Ms
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Ms;
    using Maticsoft.Model.Ms;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class RegionRec
    {
        private readonly IRegionRec dal = DAMs.CreateRegionRec();

        public int Add(Maticsoft.Model.Ms.RegionRec model)
        {
            return this.dal.Add(model);
        }

        public int AddEx(int regionId, int type)
        {
            Maticsoft.Model.Ms.Regions model = new Maticsoft.BLL.Ms.Regions().GetModel(regionId);
            Maticsoft.Model.Ms.RegionRec rec = new Maticsoft.Model.Ms.RegionRec();
            this.Delete(regionId, type);
            if (model != null)
            {
                rec.RegionId = model.RegionId;
                rec.RegionName = model.RegionName;
                rec.Type = type;
                rec.DisplaySequence = 0;
                return this.dal.Add(rec);
            }
            return -1;
        }

        public List<Maticsoft.Model.Ms.RegionRec> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Ms.RegionRec> list = new List<Maticsoft.Model.Ms.RegionRec>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Ms.RegionRec item = this.dal.DataRowToModel(dt.Rows[i]);
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

        public bool Delete(int regionId, int type)
        {
            return this.dal.Delete(regionId, type);
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

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Ms.RegionRec GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.Ms.RegionRec GetModelByCache(int ID)
        {
            string cacheKey = "RegionRecModel-" + ID;
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
            return (Maticsoft.Model.Ms.RegionRec) cache;
        }

        public List<Maticsoft.Model.Ms.RegionRec> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.Ms.RegionRec> GetRecCityList(int type)
        {
            string cacheKey = "GetRecCityList-" + type;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetModelList(" type=" + type);
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
            return (List<Maticsoft.Model.Ms.RegionRec>) cache;
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Ms.RegionRec model)
        {
            return this.dal.Update(model);
        }
    }
}

