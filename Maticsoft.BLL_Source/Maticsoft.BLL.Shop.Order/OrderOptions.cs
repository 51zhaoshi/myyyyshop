namespace Maticsoft.BLL.Shop.Order
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Order;
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class OrderOptions
    {
        private readonly IOrderOptions dal = DAShopOrder.CreateOrderOptions();

        public bool Add(Maticsoft.Model.Shop.Order.OrderOptions model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Order.OrderOptions> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Order.OrderOptions> list = new List<Maticsoft.Model.Shop.Order.OrderOptions>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Order.OrderOptions item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int LookupListId, int LookupItemId, long OrderId)
        {
            return this.dal.Delete(LookupListId, LookupItemId, OrderId);
        }

        public bool Exists(int LookupListId, int LookupItemId, long OrderId)
        {
            return this.dal.Exists(LookupListId, LookupItemId, OrderId);
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

        public Maticsoft.Model.Shop.Order.OrderOptions GetModel(int LookupListId, int LookupItemId, long OrderId)
        {
            return this.dal.GetModel(LookupListId, LookupItemId, OrderId);
        }

        public Maticsoft.Model.Shop.Order.OrderOptions GetModelByCache(int LookupListId, int LookupItemId, long OrderId)
        {
            string cacheKey = string.Concat(new object[] { "OrderOptionsModel-", LookupListId, LookupItemId, OrderId });
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(LookupListId, LookupItemId, OrderId);
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
            return (Maticsoft.Model.Shop.Order.OrderOptions) cache;
        }

        public List<Maticsoft.Model.Shop.Order.OrderOptions> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Order.OrderOptions model)
        {
            return this.dal.Update(model);
        }
    }
}

