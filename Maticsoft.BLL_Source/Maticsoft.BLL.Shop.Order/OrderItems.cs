namespace Maticsoft.BLL.Shop.Order
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Order;
    using Maticsoft.Model.Shop.Order;
    using Maticsoft.ViewModel.Shop;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class OrderItems
    {
        private readonly IOrderItems dal = DAShopOrder.CreateOrderItem();

        public long Add(Maticsoft.Model.Shop.Order.OrderItems model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Order.OrderItems> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Order.OrderItems> list = new List<Maticsoft.Model.Shop.Order.OrderItems>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Order.OrderItems item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(long ItemId)
        {
            return this.dal.Delete(ItemId);
        }

        public bool DeleteList(string ItemIdlist)
        {
            return this.dal.DeleteList(ItemIdlist);
        }

        public bool Exists(long ItemId)
        {
            return this.dal.Exists(ItemId);
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

        public DataSet GetListByCache(string strWhere)
        {
            string cacheKey = "GetListByCache-" + strWhere;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetList(strWhere);
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
            return (DataSet) cache;
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public Maticsoft.Model.Shop.Order.OrderItems GetModel(long ItemId)
        {
            return this.dal.GetModel(ItemId);
        }

        public Maticsoft.Model.Shop.Order.OrderItems GetModelByCache(long ItemId)
        {
            string cacheKey = "OrderItemModel-" + ItemId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ItemId);
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
            return (Maticsoft.Model.Shop.Order.OrderItems) cache;
        }

        public List<Maticsoft.Model.Shop.Order.OrderItems> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetOrderItemCountByOrderId(long orderId)
        {
            return this.dal.GetRecordCount(" OrderId=" + orderId);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public List<SaleRecord> GetSaleRecordByPage(long productId, string orderby, int startIndex, int endIndex)
        {
            DataSet set = this.dal.GetSaleRecordByPage(productId, orderby, startIndex, endIndex);
            return this.SaleRecordToList(set.Tables[0]);
        }

        public int GetSaleRecordCount(long productId)
        {
            return this.dal.GetSaleRecordCount(productId);
        }

        public List<SaleRecord> SaleRecordToList(DataTable dt)
        {
            List<SaleRecord> list = new List<SaleRecord>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    SaleRecord item = new SaleRecord();
                    if ((dt.Rows[i]["BuyerName"] != null) && (dt.Rows[i]["BuyerName"].ToString() != ""))
                    {
                        item.BuyName = dt.Rows[i]["BuyerName"].ToString();
                    }
                    if ((dt.Rows[i]["ShipmentQuantity"] != null) && (dt.Rows[i]["ShipmentQuantity"].ToString() != ""))
                    {
                        item.BuyCount = Globals.SafeInt(dt.Rows[i]["ShipmentQuantity"].ToString(), 0);
                    }
                    if ((dt.Rows[i]["CreatedDate"] != null) && (dt.Rows[i]["CreatedDate"].ToString() != ""))
                    {
                        item.BuyDate = Globals.SafeDateTime(dt.Rows[i]["CreatedDate"].ToString(), DateTime.Now);
                    }
                    if ((dt.Rows[i]["SellPrice"] != null) && (dt.Rows[i]["SellPrice"].ToString() != ""))
                    {
                        item.BuyPrice = Globals.SafeDecimal(dt.Rows[i]["SellPrice"].ToString(), (decimal) 0M);
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Update(Maticsoft.Model.Shop.Order.OrderItems model)
        {
            return this.dal.Update(model);
        }
    }
}

