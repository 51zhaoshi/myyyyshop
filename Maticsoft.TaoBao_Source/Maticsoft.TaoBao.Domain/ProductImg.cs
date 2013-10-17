namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class ProductImg : TopObject
    {
        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("position")]
        public long Position { get; set; }

        [XmlElement("product_id")]
        public long ProductId { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }
    }
}

