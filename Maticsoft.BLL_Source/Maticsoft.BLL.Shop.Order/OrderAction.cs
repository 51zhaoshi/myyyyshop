namespace Maticsoft.BLL.Shop.Order
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Order;
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class OrderAction
    {
        private readonly IOrderAction dal = DAShopOrder.CreateOrderAction();

        public long Add(Maticsoft.Model.Shop.Order.OrderAction model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Order.OrderAction> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Order.OrderAction> list = new List<Maticsoft.Model.Shop.Order.OrderAction>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Order.OrderAction item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(long ActionId)
        {
            return this.dal.Delete(ActionId);
        }

        public bool DeleteList(string ActionIdlist)
        {
            return this.dal.DeleteList(ActionIdlist);
        }

        public bool Exists(long ActionId)
        {
            return this.dal.Exists(ActionId);
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

        public Maticsoft.Model.Shop.Order.OrderAction GetModel(long ActionId)
        {
            return this.dal.GetModel(ActionId);
        }

        public Maticsoft.Model.Shop.Order.OrderAction GetModelByCache(long ActionId)
        {
            string cacheKey = "OrderActionModel-" + ActionId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ActionId);
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
            return (Maticsoft.Model.Shop.Order.OrderAction) cache;
        }

        public List<Maticsoft.Model.Shop.Order.OrderAction> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Order.OrderAction model)
        {
            return this.dal.Update(model);
        }
    }
}

