namespace Maticsoft.IDAL.Shop.Coupon
{
    using Maticsoft.Model.Shop.Coupon;
    using System;
    using System.Data;

    public interface ICouponClass
    {
        int Add(CouponClass model);
        CouponClass DataRowToModel(DataRow row);
        bool Delete(int ClassId);
        bool DeleteList(string ClassIdlist);
        bool Exists(int ClassId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        CouponClass GetModel(int ClassId);
        int GetRecordCount(string strWhere);
        int GetSequence();
        bool Update(CouponClass model);
        bool UpdateSeqByCid(int Cid, int seq);
    }
}

