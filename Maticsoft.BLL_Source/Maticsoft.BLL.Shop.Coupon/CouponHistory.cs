namespace Maticsoft.BLL.Shop.Coupon
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Coupon;
    using Maticsoft.Model.Shop.Coupon;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class CouponHistory
    {
        private readonly ICouponHistory dal = DAShopCoupon.CreateCouponHistory();

        public bool Add(Maticsoft.Model.Shop.Coupon.CouponHistory model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Coupon.CouponHistory> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Coupon.CouponHistory> list = new List<Maticsoft.Model.Shop.Coupon.CouponHistory>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Coupon.CouponHistory item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(string CouponCode)
        {
            return this.dal.Delete(CouponCode);
        }

        public bool DeleteList(string CouponCodelist)
        {
            return this.dal.DeleteList(CouponCodelist);
        }

        public bool Exists(string CouponCode)
        {
            return this.dal.Exists(CouponCode);
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

        public Maticsoft.Model.Shop.Coupon.CouponHistory GetModel(string CouponCode)
        {
            return this.dal.GetModel(CouponCode);
        }

        public Maticsoft.Model.Shop.Coupon.CouponHistory GetModelByCache(string CouponCode)
        {
            string cacheKey = "CouponHistoryModel-" + CouponCode;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(CouponCode);
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
            return (Maticsoft.Model.Shop.Coupon.CouponHistory) cache;
        }

        public List<Maticsoft.Model.Shop.Coupon.CouponHistory> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Coupon.CouponHistory model)
        {
            return this.dal.Update(model);
        }
    }
}

