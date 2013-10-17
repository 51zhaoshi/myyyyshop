namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class LogisticsPartner : TopObject
    {
        [XmlElement("carriage")]
        public CarriageDetail Carriage { get; set; }

        [XmlElement("cover_remark")]
        public string CoverRemark { get; set; }

        [XmlElement("partner")]
        public PartnerDetail Partner { get; set; }

        [XmlElement("uncover_remark")]
        public string UncoverRemark { get; set; }
    }
}

