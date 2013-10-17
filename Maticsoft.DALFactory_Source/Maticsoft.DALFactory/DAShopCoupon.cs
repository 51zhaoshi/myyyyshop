namespace Maticsoft.DALFactory
{
    using Maticsoft.IDAL.Shop.Coupon;
    using System;

    public class DAShopCoupon : DataAccessBase
    {
        public static ICouponClass CreateCouponClass()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Coupon.CouponClass";
            return (ICouponClass) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ICouponHistory CreateCouponHistory()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Coupon.CouponHistory";
            return (ICouponHistory) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ICouponInfo CreateCouponInfo()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Coupon.CouponInfo";
            return (ICouponInfo) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ICouponRule CreateCouponRule()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Coupon.CouponRule";
            return (ICouponRule) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }
    }
}

