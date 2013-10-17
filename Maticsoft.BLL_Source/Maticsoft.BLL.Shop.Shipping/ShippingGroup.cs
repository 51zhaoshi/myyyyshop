namespace Maticsoft.BLL.Shop.Shipping
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Shipping;
    using Maticsoft.Model.Shop.Shipping;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ShippingGroup
    {
        private readonly IShippingGroup dal = DAShopShipping.CreateShippingGroup();

        public int Add(Maticsoft.Model.Shop.Shipping.ShippingGroup model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Shipping.ShippingGroup> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Shipping.ShippingGroup> list = new List<Maticsoft.Model.Shop.Shipping.ShippingGroup>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Shipping.ShippingGroup item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int GroupId)
        {
            return this.dal.Delete(GroupId);
        }

        public bool DeleteList(string GroupIdlist)
        {
            return this.dal.DeleteList(GroupIdlist);
        }

        public bool Exists(int GroupId)
        {
            return this.dal.Exists(GroupId);
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

        public Maticsoft.Model.Shop.Shipping.ShippingGroup GetModel(int GroupId)
        {
            return this.dal.GetModel(GroupId);
        }

        public Maticsoft.Model.Shop.Shipping.ShippingGroup GetModelByCache(int GroupId)
        {
            string cacheKey = "ShippingGroupModel-" + GroupId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(GroupId);
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
            return (Maticsoft.Model.Shop.Shipping.ShippingGroup) cache;
        }

        public List<Maticsoft.Model.Shop.Shipping.ShippingGroup> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Shipping.ShippingGroup model)
        {
            return this.dal.Update(model);
        }
    }
}

