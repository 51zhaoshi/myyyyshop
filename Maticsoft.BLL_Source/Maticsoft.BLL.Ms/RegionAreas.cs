namespace Maticsoft.BLL.Ms
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Ms;
    using Maticsoft.Model.Ms;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class RegionAreas
    {
        private readonly IRegionAreas dal = DAMs.CreateRegionAreas();

        public int Add(Maticsoft.Model.Ms.RegionAreas model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Ms.RegionAreas> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Ms.RegionAreas> list = new List<Maticsoft.Model.Ms.RegionAreas>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Ms.RegionAreas item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int AreaId)
        {
            return this.dal.Delete(AreaId);
        }

        public bool DeleteList(string AreaIdlist)
        {
            return this.dal.DeleteList(AreaIdlist);
        }

        public bool Exists(int AreaId)
        {
            return this.dal.Exists(AreaId);
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

        public Maticsoft.Model.Ms.RegionAreas GetModel(int AreaId)
        {
            return this.dal.GetModel(AreaId);
        }

        public Maticsoft.Model.Ms.RegionAreas GetModelByCache(int AreaId)
        {
            string cacheKey = "AreaModel-" + AreaId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(AreaId);
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
            return (Maticsoft.Model.Ms.RegionAreas) cache;
        }

        public List<Maticsoft.Model.Ms.RegionAreas> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Ms.RegionAreas model)
        {
            return this.dal.Update(model);
        }
    }
}

