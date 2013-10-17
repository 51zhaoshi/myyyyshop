namespace Maticsoft.IDAL.Shop.Inquiry
{
    using Maticsoft.Model.Shop.Inquiry;
    using System;
    using System.Data;

    public interface IInquiryInfo
    {
        long Add(InquiryInfo model);
        InquiryInfo DataRowToModel(DataRow row);
        bool Delete(long InquiryId);
        bool DeleteEx(long InquiryId);
        bool DeleteList(string InquiryIdlist);
        bool Exists(long InquiryId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        InquiryInfo GetModel(long InquiryId);
        int GetRecordCount(string strWhere);
        bool Update(InquiryInfo model);
    }
}

