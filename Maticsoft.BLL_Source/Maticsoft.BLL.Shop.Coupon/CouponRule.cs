namespace Maticsoft.BLL.Shop.Coupon
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Shop.Coupon;
    using Maticsoft.Model.Shop.Coupon;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class CouponRule
    {
        private readonly ICouponRule dal = DAShopCoupon.CreateCouponRule();

        public int Add(Maticsoft.Model.Shop.Coupon.CouponRule model)
        {
            return this.dal.Add(model);
        }

        public bool AddEx(Maticsoft.Model.Shop.Coupon.CouponRule model, int cpLength, int pwdLegth)
        {
            int num = this.Add(model);
            if ((num <= 0) || (model.SendCount <= 0))
            {
                return false;
            }
            List<string> list = new List<string>();
            Maticsoft.BLL.Shop.Coupon.CouponInfo info = new Maticsoft.BLL.Shop.Coupon.CouponInfo();
            Maticsoft.Model.Shop.Coupon.CouponInfo info2 = new Maticsoft.Model.Shop.Coupon.CouponInfo {
                CategoryId = model.CategoryId,
                ClassId = model.ClassId,
                RuleId = num,
                CouponName = model.Name,
                CouponPrice = model.CouponPrice
            };
            Random random = new Random();
            info2.EndDate = model.EndDate;
            info2.StartDate = model.StartDate;
            info2.Status = 0;
            info2.GenerateTime = DateTime.Now;
            info2.IsPwd = model.IsPwd;
            info2.IsReuse = model.IsReuse;
            info2.LimitPrice = model.LimitPrice;
            info2.SupplierId = model.SupplierId;
            info2.NeedPoint = model.NeedPoint;
            int num2 = 10;
            for (int i = 1; i < (cpLength - 4); i++)
            {
                num2 *= 10;
            }
            int num4 = 10;
            for (int j = 1; j < pwdLegth; j++)
            {
                num4 *= 10;
            }
            for (int k = 0; k < model.SendCount; k++)
            {
                int num7 = random.Next((num2 / 10) + 1, num2 - 1);
                info2.CouponCode = model.PreName + DateTime.Now.ToString("MMdd") + num7.ToString();
                info2.CouponPwd = (info2.IsPwd == 1) ? random.Next(num4 / 10, num4 - 1).ToString() : "";
                while (list.Contains(info2.CouponCode))
                {
                    info2.CouponCode = model.PreName + DateTime.Now.ToString("MMdd") + random.Next((num2 / 10) + 1, num2 - 1).ToString();
                }
                list.Add(info2.CouponCode);
                info.Add(info2);
            }
            return true;
        }

        public List<Maticsoft.Model.Shop.Coupon.CouponRule> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Coupon.CouponRule> list = new List<Maticsoft.Model.Shop.Coupon.CouponRule>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Shop.Coupon.CouponRule item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int CouponId)
        {
            return this.dal.Delete(CouponId);
        }

        public bool DeleteEx(int CouponId)
        {
            return this.dal.DeleteEx(CouponId);
        }

        public bool DeleteList(string CouponIdlist)
        {
            return this.dal.DeleteList(CouponIdlist);
        }

        public bool Exists(int CouponId)
        {
            return this.dal.Exists(CouponId);
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

        public Maticsoft.Model.Shop.Coupon.CouponRule GetModel(int CouponId)
        {
            return this.dal.GetModel(CouponId);
        }

        public Maticsoft.Model.Shop.Coupon.CouponRule GetModelByCache(int CouponId)
        {
            string cacheKey = "CouponRuleModel-" + CouponId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(CouponId);
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
            return (Maticsoft.Model.Shop.Coupon.CouponRule) cache;
        }

        public List<Maticsoft.Model.Shop.Coupon.CouponRule> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Shop.Coupon.CouponRule model)
        {
            return this.dal.Update(model);
        }
    }
}

