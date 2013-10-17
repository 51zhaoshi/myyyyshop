namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class AddressResult : TopObject
    {
        [XmlElement("addr")]
        public string Addr { get; set; }

        [XmlElement("area_id")]
        public long AreaId { get; set; }

        [XmlElement("cancel_def")]
        public bool CancelDef { get; set; }

        [XmlElement("city")]
        public string City { get; set; }

        [XmlElement("contact_id")]
        public long ContactId { get; set; }

        [XmlElement("contact_name")]
        public string ContactName { get; set; }

        [XmlElement("country")]
        public string Country { get; set; }

        [XmlElement("get_def")]
        public bool GetDef { get; set; }

        [XmlElement("memo")]
        public string Memo { get; set; }

        [XmlElement("mobile_phone")]
        public string MobilePhone { get; set; }

        [XmlElement("modify_date")]
        public string ModifyDate { get; set; }

        [XmlElement("phone")]
        public string Phone { get; set; }

        [XmlElement("province")]
        public string Province { get; set; }

        [XmlElement("seller_company")]
        public string SellerCompany { get; set; }

        [XmlElement("send_def")]
        public bool SendDef { get; set; }

        [XmlElement("zip_code")]
        public string ZipCode { get; set; }
    }
}

