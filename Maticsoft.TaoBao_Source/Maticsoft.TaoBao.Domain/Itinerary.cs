namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Itinerary : TopObject
    {
        [XmlElement("address")]
        public string Address { get; set; }

        [XmlElement("alipay_trade_no")]
        public string AlipayTradeNo { get; set; }

        [XmlElement("company_code")]
        public string CompanyCode { get; set; }

        [XmlElement("express_code")]
        public string ExpressCode { get; set; }

        [XmlElement("extra")]
        public string Extra { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("itinerary_no")]
        public string ItineraryNo { get; set; }

        [XmlElement("mobile")]
        public string Mobile { get; set; }

        [XmlElement("mobile_bak")]
        public string MobileBak { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("send_date")]
        public string SendDate { get; set; }

        [XmlElement("status")]
        public long Status { get; set; }

        [XmlElement("type")]
        public long Type { get; set; }
    }
}

