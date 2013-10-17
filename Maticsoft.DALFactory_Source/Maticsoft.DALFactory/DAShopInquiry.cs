namespace Maticsoft.DALFactory
{
    using Maticsoft.IDAL.Shop.Inquiry;
    using System;

    public class DAShopInquiry : DataAccessBase
    {
        public static IInquiryInfo CreateInquiryInfo()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Inquiry.InquiryInfo";
            return (IInquiryInfo) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IInquiryItem CreateInquiryItem()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Inquiry.InquiryItem";
            return (IInquiryItem) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }
    }
}

