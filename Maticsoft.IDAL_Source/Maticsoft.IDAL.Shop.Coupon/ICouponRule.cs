namespace Maticsoft.IDAL.Shop.Coupon
{
    using Maticsoft.Model.Shop.Coupon;
    using System;
    using System.Data;

    public interface ICouponRule
    {
        int Add(CouponRule model);
        CouponRule DataRowToModel(DataRow row);
        bool Delete(int CouponId);
        bool DeleteEx(int CouponId);
        bool DeleteList(string CouponIdlist);
        bool Exists(int CouponId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        CouponRule GetModel(int CouponId);
        int GetRecordCount(string strWhere);
        bool Update(CouponRule model);
    }
}

