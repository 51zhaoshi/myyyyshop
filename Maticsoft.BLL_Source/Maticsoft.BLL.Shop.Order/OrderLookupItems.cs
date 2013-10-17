namespace Maticsoft.BLL.Shop.Order
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Order;
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class OrderLookupItems
    {
        private readonly IOrderLookupItems dal = DAShopOrder.CreateOrderLookupItems();

        public int Add(Maticsoft.Model.Shop.Order.OrderLookupItems model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Order.OrderLookupItems> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Order.OrderLookupItems> list = new List<Maticsoft.Model.Shop.Order.OrderLookupItems>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Order.OrderLookupItems item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int LookupItemId)
        {
            return this.dal.Delete(LookupItemId);
        }

        public bool DeleteList(string LookupItemIdlist)
        {
            return this.dal.DeleteList(LookupItemIdlist);
        }

        public bool Exists(int LookupItemId)
        {
            return this.dal.Exists(LookupItemId);
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

        public Maticsoft.Model.Shop.Order.OrderLookupItems GetModel(int LookupItemId)
        {
            return this.dal.GetModel(LookupItemId);
        }

        public Maticsoft.Model.Shop.Order.OrderLookupItems GetModelByCache(int LookupItemId)
        {
            string cacheKey = "OrderLookupItemsModel-" + LookupItemId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(LookupItemId);
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
            return (Maticsoft.Model.Shop.Order.OrderLookupItems) cache;
        }

        public List<Maticsoft.Model.Shop.Order.OrderLookupItems> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Order.OrderLookupItems model)
        {
            return this.dal.Update(model);
        }
    }
}

