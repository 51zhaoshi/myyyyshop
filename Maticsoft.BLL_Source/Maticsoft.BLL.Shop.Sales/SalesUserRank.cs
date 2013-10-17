namespace Maticsoft.BLL.Shop.Sales
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Sales;
    using Maticsoft.Model.Shop.Sales;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class SalesUserRank
    {
        private readonly ISalesUserRank dal = DAShopSales.CreateSalesUserRank();

        public bool Add(Maticsoft.Model.Shop.Sales.SalesUserRank model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Sales.SalesUserRank> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Sales.SalesUserRank> list = new List<Maticsoft.Model.Shop.Sales.SalesUserRank>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Sales.SalesUserRank item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int RuleId, int RankId)
        {
            return this.dal.Delete(RuleId, RankId);
        }

        public bool DeleteByRuleId(int ruleId)
        {
            return this.dal.DeleteByRuleId(ruleId);
        }

        public bool Exists(int RuleId, int RankId)
        {
            return this.dal.Exists(RuleId, RankId);
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

        public Maticsoft.Model.Shop.Sales.SalesUserRank GetModel(int RuleId, int RankId)
        {
            return this.dal.GetModel(RuleId, RankId);
        }

        public Maticsoft.Model.Shop.Sales.SalesUserRank GetModelByCache(int RuleId, int RankId)
        {
            string cacheKey = "SalesUserRankModel-" + RuleId + RankId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(RuleId, RankId);
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
            return (Maticsoft.Model.Shop.Sales.SalesUserRank) cache;
        }

        public List<Maticsoft.Model.Shop.Sales.SalesUserRank> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Sales.SalesUserRank model)
        {
            return this.dal.Update(model);
        }
    }
}

