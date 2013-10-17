namespace Maticsoft.BLL.Shop.Sales
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Sales;
    using Maticsoft.Model.Shop.Sales;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class SalesRule
    {
        private readonly ISalesRule dal = DAShopSales.CreateSalesRule();

        public int Add(Maticsoft.Model.Shop.Sales.SalesRule model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Sales.SalesRule> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Sales.SalesRule> list = new List<Maticsoft.Model.Shop.Sales.SalesRule>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Sales.SalesRule item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int RuleId)
        {
            return this.dal.Delete(RuleId);
        }

        public bool DeleteEx(int RuleId)
        {
            return this.dal.DeleteEx(RuleId);
        }

        public bool DeleteList(string RuleIdlist)
        {
            return this.dal.DeleteList(RuleIdlist);
        }

        public bool DeleteListEx(string RuleIdlist)
        {
            return this.dal.DeleteListEx(RuleIdlist);
        }

        public bool Exists(int RuleId)
        {
            return this.dal.Exists(RuleId);
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

        public Maticsoft.Model.Shop.Sales.SalesRule GetModel(int RuleId)
        {
            return this.dal.GetModel(RuleId);
        }

        public Maticsoft.Model.Shop.Sales.SalesRule GetModelByCache(int RuleId)
        {
            string cacheKey = "SalesRuleModel-" + RuleId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(RuleId);
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
            return (Maticsoft.Model.Shop.Sales.SalesRule) cache;
        }

        public List<Maticsoft.Model.Shop.Sales.SalesRule> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Sales.SalesRule model)
        {
            return this.dal.Update(model);
        }
    }
}

