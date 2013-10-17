namespace Maticsoft.IDAL.Shop.Coupon
{
    using Maticsoft.Model.Shop.Coupon;
    using System;
    using System.Data;

    public interface ICouponHistory
    {
        bool Add(CouponHistory model);
        CouponHistory DataRowToModel(DataRow row);
        bool Delete(string CouponCode);
        bool DeleteList(string CouponCodelist);
        bool Exists(string CouponCode);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        CouponHistory GetModel(string CouponCode);
        int GetRecordCount(string strWhere);
        bool Update(CouponHistory model);
    }
}

