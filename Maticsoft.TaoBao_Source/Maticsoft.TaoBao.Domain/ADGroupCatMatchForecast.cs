namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class ADGroupCatMatchForecast : TopObject
    {
        [XmlElement("adgroup_id")]
        public long AdgroupId { get; set; }

        [XmlElement("catmatch_id")]
        public long CatmatchId { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("price_click")]
        public string PriceClick { get; set; }

        [XmlElement("price_cust")]
        public string PriceCust { get; set; }

        [XmlElement("price_rank")]
        public string PriceRank { get; set; }
    }
}

