namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class INWordAnalysis : TopObject
    {
        [XmlElement("word")]
        public string Word { get; set; }

        [XmlElement("word_area_per")]
        public string WordAreaPer { get; set; }

        [XmlElement("word_hp_price")]
        public string WordHpPrice { get; set; }

        [XmlElement("word_source_per")]
        public string WordSourcePer { get; set; }
    }
}

