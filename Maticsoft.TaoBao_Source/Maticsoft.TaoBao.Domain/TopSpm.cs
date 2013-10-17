namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class TopSpm : TopObject
    {
        [XmlElement("alipay_deal_amount")]
        public string AlipayDealAmount { get; set; }

        [XmlElement("alipay_deal_count")]
        public long AlipayDealCount { get; set; }

        [XmlElement("alipay_uv")]
        public long AlipayUv { get; set; }

        [XmlElement("module")]
        public string Module { get; set; }

        [XmlElement("page")]
        public string Page { get; set; }

        [XmlElement("pv")]
        public long Pv { get; set; }

        [XmlElement("uv")]
        public long Uv { get; set; }
    }
}

