namespace Maticsoft.IDAL.Shop.Coupon
{
    using Maticsoft.Model.Shop.Coupon;
    using System;
    using System.Data;

    public interface ICouponInfo
    {
        bool Add(CouponInfo model);
        bool AddHistory(CouponInfo info);
        CouponInfo DataRowToModel(DataRow row);
        bool Delete(string CouponCode);
        bool DeleteEx(int ruleId);
        bool DeleteList(string CouponCodelist);
        bool Exists(string CouponCode);
        CouponInfo GetCouponInfo(string CouponCode, bool IsExpired);
        CouponInfo GetCouponInfo(string CouponCode, string pwd, bool IsExpired);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        CouponInfo GetModel(string CouponCode);
        int GetRecordCount(string strWhere);
        bool Update(CouponInfo model);
        bool UpdateUser(int ruleId, int userId, string userEmail);
        bool UpdateUser(string couponCode, int userId, string userEmail);
        bool UseCoupon(string couponCode, int userId, string userEmail);
    }
}

