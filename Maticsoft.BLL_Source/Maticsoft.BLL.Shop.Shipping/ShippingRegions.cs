namespace Maticsoft.BLL.Shop.Shipping
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Shipping;
    using Maticsoft.Model.Shop.Shipping;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ShippingRegions
    {
        private readonly IShippingRegions dal = DAShopShipping.CreateShippingRegions();

        public bool Add(Maticsoft.Model.Shop.Shipping.ShippingRegions model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Shipping.ShippingRegions> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Shipping.ShippingRegions> list = new List<Maticsoft.Model.Shop.Shipping.ShippingRegions>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Shipping.ShippingRegions item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int ModeId, int RegionId)
        {
            return this.dal.Delete(ModeId, RegionId);
        }

        public bool Exists(int ModeId, int RegionId)
        {
            return this.dal.Exists(ModeId, RegionId);
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

        public Maticsoft.Model.Shop.Shipping.ShippingRegions GetModel(int ModeId, int RegionId)
        {
            return this.dal.GetModel(ModeId, RegionId);
        }

        public Maticsoft.Model.Shop.Shipping.ShippingRegions GetModelByCache(int ModeId, int RegionId)
        {
            string cacheKey = "ShippingRegionsModel-" + ModeId + RegionId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ModeId, RegionId);
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
            return (Maticsoft.Model.Shop.Shipping.ShippingRegions) cache;
        }

        public List<Maticsoft.Model.Shop.Shipping.ShippingRegions> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Shipping.ShippingRegions model)
        {
            return this.dal.Update(model);
        }
    }
}

