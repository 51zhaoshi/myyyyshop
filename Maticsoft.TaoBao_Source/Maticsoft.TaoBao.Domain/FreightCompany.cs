namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class FreightCompany : TopObject
    {
        [XmlElement("comments")]
        public string Comments { get; set; }

        [XmlElement("companye_code")]
        public string CompanyeCode { get; set; }

        [XmlElement("company_id")]
        public long CompanyId { get; set; }

        [XmlElement("company_name")]
        public string CompanyName { get; set; }

        [XmlElement("corp_level")]
        public string CorpLevel { get; set; }

        [XmlElement("customer_service_tel")]
        public string CustomerServiceTel { get; set; }

        [XmlElement("logo_url")]
        public string LogoUrl { get; set; }

        [XmlElement("shop_url")]
        public string ShopUrl { get; set; }

        [XmlElement("sort")]
        public long Sort { get; set; }

        [XmlElement("vas_fee_help_url")]
        public string VasFeeHelpUrl { get; set; }

        [XmlArray("wangwang_list"), XmlArrayItem("wangwang_info")]
        public List<WangwangInfo> WangwangList { get; set; }
    }
}

