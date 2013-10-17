namespace Maticsoft.BLL.Shop.Order
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Order;
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class OrdersHistory
    {
        private readonly IOrdersHistory dal = DAShopOrder.CreateOrdersHistory();

        public bool Add(Maticsoft.Model.Shop.Order.OrdersHistory model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Order.OrdersHistory> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Order.OrdersHistory> list = new List<Maticsoft.Model.Shop.Order.OrdersHistory>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Order.OrdersHistory item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(long OrderId)
        {
            return this.dal.Delete(OrderId);
        }

        public bool DeleteList(string OrderIdlist)
        {
            return this.dal.DeleteList(OrderIdlist);
        }

        public bool Exists(long OrderId)
        {
            return this.dal.Exists(OrderId);
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

        public Maticsoft.Model.Shop.Order.OrdersHistory GetModel(long OrderId)
        {
            return this.dal.GetModel(OrderId);
        }

        public Maticsoft.Model.Shop.Order.OrdersHistory GetModelByCache(long OrderId)
        {
            string cacheKey = "OrdersHistoryModel-" + OrderId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(OrderId);
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
            return (Maticsoft.Model.Shop.Order.OrdersHistory) cache;
        }

        public List<Maticsoft.Model.Shop.Order.OrdersHistory> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Order.OrdersHistory model)
        {
            return this.dal.Update(model);
        }
    }
}

