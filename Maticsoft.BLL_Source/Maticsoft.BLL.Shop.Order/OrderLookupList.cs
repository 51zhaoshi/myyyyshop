namespace Maticsoft.BLL.Shop.Order
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Order;
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class OrderLookupList
    {
        private readonly IOrderLookupList dal = DAShopOrder.CreateOrderLookupList();

        public int Add(Maticsoft.Model.Shop.Order.OrderLookupList model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Order.OrderLookupList> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Order.OrderLookupList> list = new List<Maticsoft.Model.Shop.Order.OrderLookupList>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Order.OrderLookupList item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int LookupListId)
        {
            return this.dal.Delete(LookupListId);
        }

        public bool DeleteList(string LookupListIdlist)
        {
            return this.dal.DeleteList(LookupListIdlist);
        }

        public bool Exists(int LookupListId)
        {
            return this.dal.Exists(LookupListId);
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

        public Maticsoft.Model.Shop.Order.OrderLookupList GetModel(int LookupListId)
        {
            return this.dal.GetModel(LookupListId);
        }

        public Maticsoft.Model.Shop.Order.OrderLookupList GetModelByCache(int LookupListId)
        {
            string cacheKey = "OrderLookupListModel-" + LookupListId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(LookupListId);
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
            return (Maticsoft.Model.Shop.Order.OrderLookupList) cache;
        }

        public List<Maticsoft.Model.Shop.Order.OrderLookupList> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Order.OrderLookupList model)
        {
            return this.dal.Update(model);
        }
    }
}

