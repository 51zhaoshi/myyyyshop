namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class PartnerDetail : TopObject
    {
        [XmlElement("account_no")]
        public string AccountNo { get; set; }

        [XmlElement("company_code")]
        public string CompanyCode { get; set; }

        [XmlElement("company_id")]
        public long CompanyId { get; set; }

        [XmlElement("company_name")]
        public string CompanyName { get; set; }

        [XmlElement("full_name")]
        public string FullName { get; set; }

        [XmlElement("reg_mail_no")]
        public string RegMailNo { get; set; }

        [XmlElement("wangwang_id")]
        public string WangwangId { get; set; }
    }
}

