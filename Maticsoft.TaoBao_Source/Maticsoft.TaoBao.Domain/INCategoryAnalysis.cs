namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class INCategoryAnalysis : TopObject
    {
        [XmlElement("category_area_per")]
        public string CategoryAreaPer { get; set; }

        [XmlElement("category_hp_price")]
        public string CategoryHpPrice { get; set; }

        [XmlElement("category_id")]
        public long CategoryId { get; set; }

        [XmlElement("category_name")]
        public string CategoryName { get; set; }

        [XmlElement("category_source_per")]
        public string CategorySourcePer { get; set; }
    }
}

