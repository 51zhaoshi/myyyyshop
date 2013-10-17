namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class SimbaItem : TopObject
    {
        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("num_id")]
        public long NumId { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("publish_time")]
        public string PublishTime { get; set; }

        [XmlElement("quantity")]
        public long Quantity { get; set; }

        [XmlElement("sales_count")]
        public long SalesCount { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }
    }
}

