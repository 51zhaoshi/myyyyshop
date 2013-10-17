namespace Maticsoft.BLL.Shop.PromoteSales
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.PromoteSales;
    using Maticsoft.Model.Shop.PromoteSales;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class CountDown
    {
        private readonly ICountDown dal = DAShopProSales.CreateCountDown();

        public int Add(Maticsoft.Model.Shop.PromoteSales.CountDown model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.PromoteSales.CountDown> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.PromoteSales.CountDown> list = new List<Maticsoft.Model.Shop.PromoteSales.CountDown>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.PromoteSales.CountDown item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int CountDownId)
        {
            return this.dal.Delete(CountDownId);
        }

        public bool DeleteList(string CountDownIdlist)
        {
            return this.dal.DeleteList(CountDownIdlist);
        }

        public bool Exists(int CountDownId)
        {
            return this.dal.Exists(CountDownId);
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

        public Maticsoft.Model.Shop.PromoteSales.CountDown GetModel(int CountDownId)
        {
            return this.dal.GetModel(CountDownId);
        }

        public Maticsoft.Model.Shop.PromoteSales.CountDown GetModelByCache(int CountDownId)
        {
            string cacheKey = "CountDownModel-" + CountDownId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(CountDownId);
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
            return (Maticsoft.Model.Shop.PromoteSales.CountDown) cache;
        }

        public List<Maticsoft.Model.Shop.PromoteSales.CountDown> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool IsActivity(int countId)
        {
            Maticsoft.Model.Shop.PromoteSales.CountDown modelByCache = this.GetModelByCache(countId);
            if (modelByCache == null)
            {
                return false;
            }
            return (modelByCache.EndDate >= DateTime.Now);
        }

        public bool IsExists(long ProductId)
        {
            return this.dal.IsExists(ProductId);
        }

        public int MaxSequence()
        {
            return this.dal.MaxSequence();
        }

        public bool Update(Maticsoft.Model.Shop.PromoteSales.CountDown model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateStatus(string ids, int status)
        {
            return this.dal.UpdateStatus(ids, status);
        }
    }
}

