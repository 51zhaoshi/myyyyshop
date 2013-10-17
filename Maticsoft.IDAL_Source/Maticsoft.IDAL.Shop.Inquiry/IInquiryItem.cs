namespace Maticsoft.IDAL.Shop.Inquiry
{
    using Maticsoft.Model.Shop.Inquiry;
    using System;
    using System.Data;

    public interface IInquiryItem
    {
        long Add(InquiryItem model);
        InquiryItem DataRowToModel(DataRow row);
        bool Delete(long ItemId);
        bool DeleteList(string ItemIdlist);
        bool Exists(long ItemId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        InquiryItem GetModel(long ItemId);
        int GetRecordCount(string strWhere);
        bool Update(InquiryItem model);
    }
}

