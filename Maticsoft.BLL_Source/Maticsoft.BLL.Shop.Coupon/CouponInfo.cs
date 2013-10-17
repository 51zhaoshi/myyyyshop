namespace Maticsoft.BLL.Shop.Coupon
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Coupon;
    using Maticsoft.Model.Shop.Coupon;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class CouponInfo
    {
        private readonly ICouponInfo dal = DAShopCoupon.CreateCouponInfo();

        public bool Add(Maticsoft.Model.Shop.Coupon.CouponInfo model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Coupon.CouponInfo> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Coupon.CouponInfo> list = new List<Maticsoft.Model.Shop.Coupon.CouponInfo>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Coupon.CouponInfo item = this.dal.DataRowToModel(dt.Rows[i]);
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

        public bool DeleteEx(int ruleId)
        {
            return this.dal.DeleteEx(ruleId);
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

        public Maticsoft.Model.Shop.Coupon.CouponInfo GetCouponInfo(string CouponCode, bool IsExpired = false)
        {
            return this.dal.GetCouponInfo(CouponCode, IsExpired);
        }

        public Maticsoft.Model.Shop.Coupon.CouponInfo GetCouponInfo(string CouponCode, string pwd, bool IsExpired = true)
        {
            return this.dal.GetCouponInfo(CouponCode, pwd, IsExpired);
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

        public Maticsoft.Model.Shop.Coupon.CouponInfo GetModel(string CouponCode)
        {
            return this.dal.GetModel(CouponCode);
        }

        public Maticsoft.Model.Shop.Coupon.CouponInfo GetModelByCache(string CouponCode)
        {
            string cacheKey = "CouponInfoModel-" + CouponCode;
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
            return (Maticsoft.Model.Shop.Coupon.CouponInfo) cache;
        }

        public List<Maticsoft.Model.Shop.Coupon.CouponInfo> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool MoveHistory()
        {
            List<Maticsoft.Model.Shop.Coupon.CouponInfo> modelList = this.GetModelList(" EndDate<'" + DateTime.Now + "' ");
            bool flag = true;
            if ((modelList != null) && (modelList.Count > 0))
            {
                foreach (Maticsoft.Model.Shop.Coupon.CouponInfo info in modelList)
                {
                    if (!this.dal.AddHistory(info))
                    {
                        flag = false;
                    }
                }
            }
            return flag;
        }

        public bool Update(Maticsoft.Model.Shop.Coupon.CouponInfo model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateUser(int ruleId, int userId, string userEmail)
        {
            return this.dal.UpdateUser(ruleId, userId, userEmail);
        }

        public bool UpdateUser(string couponCode, int userId, string userEmail)
        {
            return this.dal.UpdateUser(couponCode, userId, userEmail);
        }

        public bool UseCoupon(string couponCode, int userId, string userEmail)
        {
            return this.dal.UseCoupon(couponCode, userId, userEmail);
        }
    }
}

