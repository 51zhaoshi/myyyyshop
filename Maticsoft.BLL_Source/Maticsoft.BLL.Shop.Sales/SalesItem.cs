namespace Maticsoft.BLL.Shop.Sales
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Sales;
    using Maticsoft.Model.Shop.Sales;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class SalesItem
    {
        private readonly ISalesItem dal = DAShopSales.CreateSalesItem();

        public int Add(Maticsoft.Model.Shop.Sales.SalesItem model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Sales.SalesItem> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Sales.SalesItem> list = new List<Maticsoft.Model.Shop.Sales.SalesItem>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Sales.SalesItem item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int ItemId)
        {
            return this.dal.Delete(ItemId);
        }

        public bool DeleteByRuleId(int ruleId)
        {
            return this.dal.DeleteByRuleId(ruleId);
        }

        public bool DeleteList(string ItemIdlist)
        {
            return this.dal.DeleteList(ItemIdlist);
        }

        public bool Exists(int ItemId)
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

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Shop.Sales.SalesItem GetModel(int ItemId)
        {
            return this.dal.GetModel(ItemId);
        }

        public Maticsoft.Model.Shop.Sales.SalesItem GetModelByCache(int ItemId)
        {
            string cacheKey = "SalesItemModel-" + ItemId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ItemId);
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
            return (Maticsoft.Model.Shop.Sales.SalesItem) cache;
        }

        public List<Maticsoft.Model.Shop.Sales.SalesItem> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Sales.SalesItem model)
        {
            return this.dal.Update(model);
        }
    }
}

