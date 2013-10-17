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

    public class Regions
    {
        private readonly IRegions dal = DAMs.CreateRegions();

        public int Add(Maticsoft.Model.Ms.Regions model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Ms.Regions> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Ms.Regions> list = new List<Maticsoft.Model.Ms.Regions>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Ms.Regions item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int RegionId)
        {
            return this.dal.Delete(RegionId);
        }

        public bool DeleteList(string RegionIdlist)
        {
            return this.dal.DeleteList(RegionIdlist);
        }

        public bool Exists(int RegionId)
        {
            return this.dal.Exists(RegionId);
        }

        public DataSet GetAllCityList()
        {
            DataSet cache = DataCache.GetCache("Maticsoft_CityList") as DataSet;
            if ((cache == null) || (cache.Tables[0].Rows.Count == 0))
            {
                cache = this.dal.GetAllCityList();
                DataCache.SetCache("Maticsoft_CityList", cache);
            }
            return cache;
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetCitys(int parentID)
        {
            return this.dal.GetCitys(parentID);
        }

        public DataSet GetDisByParentId(int iParentId)
        {
            return this.dal.GetDistrictByParentId(iParentId);
        }

        public DataTable GetDistrictByParentId(int iParentId)
        {
            return this.dal.GetDistrictByParentId(iParentId).Tables[0];
        }

        public string GetFullNameById4Cache(int id)
        {
            string cacheKey = "GetFullNameById4Cache-" + id;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    string regionNameByRID = this.dal.GetRegionNameByRID(id);
                    if (!string.IsNullOrWhiteSpace(regionNameByRID))
                    {
                        cache = regionNameByRID;
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, regionNameByRID, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (cache as string);
        }

        public DataSet GetList(int RegionId)
        {
            return this.dal.GetList(string.Concat(new object[] { "RegionId=", RegionId, " or Path like '0,", RegionId, "%'" }));
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Ms.Regions GetModel(int RegionId)
        {
            return this.dal.GetModel(RegionId);
        }

        public Maticsoft.Model.Ms.Regions GetModelByCache(int RegionId)
        {
            string cacheKey = "RegionsModel-" + RegionId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(RegionId);
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
            return (Maticsoft.Model.Ms.Regions) cache;
        }

        public List<Maticsoft.Model.Ms.Regions> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public DataTable GetParentId(int id)
        {
            return this.dal.GetParentID(id);
        }

        public DataSet GetParentIDs(int regID, out int Count)
        {
            return this.dal.GetParentIDs(regID, out Count);
        }

        public DataSet GetPrivoceName()
        {
            return this.dal.GetPrivoceName();
        }

        public DataSet GetPrivoces()
        {
            return this.dal.GetPrivoces();
        }

        public List<Maticsoft.Model.Ms.Regions> GetProvinceList()
        {
            string cacheKey = "GetProvinceList-ProvinceList";
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    DataSet privoces = this.dal.GetPrivoces();
                    cache = this.DataTableToList(privoces.Tables[0]);
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
            return (List<Maticsoft.Model.Ms.Regions>) cache;
        }

        public DataSet GetProvinces()
        {
            return this.dal.GetProvinces();
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public string GetRegionIDsByAreaId(int areaid)
        {
            return this.dal.GetRegionIDsByAreaId(areaid);
        }

        public DataSet GetRegionName(string parentID)
        {
            return this.dal.GetRegionName(parentID);
        }

        public string GetRegionNameByRID(int RID)
        {
            return this.dal.GetRegionNameByRID(RID);
        }

        public int GetRegPath(int? regid)
        {
            return this.dal.GetRegPath(regid);
        }

        public bool Update(Maticsoft.Model.Ms.Regions model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateAreaID(string regionlist, int AreaId)
        {
            return this.dal.UpdateAreaID(regionlist, AreaId);
        }
    }
}

