namespace Maticsoft.BLL.Shop.Shipping
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Shipping;
    using Maticsoft.Model.Shop.Shipping;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ShippingAddress
    {
        private readonly IShippingAddress dal = DAShopShipping.CreateShippingAddress();

        public int Add(Maticsoft.Model.Shop.Shipping.ShippingAddress model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Shipping.ShippingAddress> DataTableToList(DataTable dt)
        {
            Regions regions = new Regions();
            List<Maticsoft.Model.Shop.Shipping.ShippingAddress> list = new List<Maticsoft.Model.Shop.Shipping.ShippingAddress>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Shipping.ShippingAddress item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        item.RegionFullName = regions.GetFullNameById4Cache(item.RegionId);
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int ShippingId)
        {
            return this.dal.Delete(ShippingId);
        }

        public bool DeleteList(string ShippingIdlist)
        {
            return this.dal.DeleteList(ShippingIdlist);
        }

        public bool Exists(int ShippingId)
        {
            return this.dal.Exists(ShippingId);
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

        public Maticsoft.Model.Shop.Shipping.ShippingAddress GetModel(int ShippingId)
        {
            Regions regions = new Regions();
            Maticsoft.Model.Shop.Shipping.ShippingAddress model = this.dal.GetModel(ShippingId);
            if (model != null)
            {
                model.RegionFullName = regions.GetFullNameById4Cache(model.RegionId);
            }
            return model;
        }

        public Maticsoft.Model.Shop.Shipping.ShippingAddress GetModelByCache(int ShippingId)
        {
            string cacheKey = "ShippingAddressModel-" + ShippingId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ShippingId);
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
            return (Maticsoft.Model.Shop.Shipping.ShippingAddress) cache;
        }

        public List<Maticsoft.Model.Shop.Shipping.ShippingAddress> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Shipping.ShippingAddress model)
        {
            return this.dal.Update(model);
        }
    }
}

