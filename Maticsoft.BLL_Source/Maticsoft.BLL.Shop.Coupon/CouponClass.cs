namespace Maticsoft.BLL.Shop.Coupon
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Coupon;
    using Maticsoft.Model.Shop.Coupon;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class CouponClass
    {
        private readonly ICouponClass dal = DAShopCoupon.CreateCouponClass();

        public int Add(Maticsoft.Model.Shop.Coupon.CouponClass model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Shop.Coupon.CouponClass> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Coupon.CouponClass> list = new List<Maticsoft.Model.Shop.Coupon.CouponClass>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Coupon.CouponClass item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int ClassId)
        {
            return this.dal.Delete(ClassId);
        }

        public bool DeleteList(string ClassIdlist)
        {
            return this.dal.DeleteList(ClassIdlist);
        }

        public bool Exists(int ClassId)
        {
            return this.dal.Exists(ClassId);
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

        public Maticsoft.Model.Shop.Coupon.CouponClass GetModel(int ClassId)
        {
            return this.dal.GetModel(ClassId);
        }

        public Maticsoft.Model.Shop.Coupon.CouponClass GetModelByCache(int ClassId)
        {
            string cacheKey = "CouponClassModel-" + ClassId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ClassId);
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
            return (Maticsoft.Model.Shop.Coupon.CouponClass) cache;
        }

        public List<Maticsoft.Model.Shop.Coupon.CouponClass> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int GetSequence()
        {
            return this.dal.GetSequence();
        }

        public bool Update(Maticsoft.Model.Shop.Coupon.CouponClass model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateSeqByCid(int Cid, int seq)
        {
            return this.dal.UpdateSeqByCid(Cid, seq);
        }
    }
}

