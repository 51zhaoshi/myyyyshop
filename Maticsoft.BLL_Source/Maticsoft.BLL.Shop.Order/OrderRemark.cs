namespace Maticsoft.BLL.Shop.Order
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Order;
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class OrderRemark
    {
        private readonly IOrderRemark dal = DAShopOrder.CreateOrderRemark();

        public long Add(Maticsoft.Model.Shop.Order.OrderRemark model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Order.OrderRemark> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Order.OrderRemark> list = new List<Maticsoft.Model.Shop.Order.OrderRemark>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Order.OrderRemark item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(long RemarkId)
        {
            return this.dal.Delete(RemarkId);
        }

        public bool DeleteList(string RemarkIdlist)
        {
            return this.dal.DeleteList(RemarkIdlist);
        }

        public bool Exists(long RemarkId)
        {
            return this.dal.Exists(RemarkId);
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

        public Maticsoft.Model.Shop.Order.OrderRemark GetModel(long RemarkId)
        {
            return this.dal.GetModel(RemarkId);
        }

        public Maticsoft.Model.Shop.Order.OrderRemark GetModelByCache(long RemarkId)
        {
            string cacheKey = "OrderRemarkModel-" + RemarkId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(RemarkId);
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
            return (Maticsoft.Model.Shop.Order.OrderRemark) cache;
        }

        public List<Maticsoft.Model.Shop.Order.OrderRemark> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Order.OrderRemark model)
        {
            return this.dal.Update(model);
        }
    }
}

