namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WidgetSku : TopObject
    {
        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("props")]
        public string Props { get; set; }

        [XmlElement("quantity")]
        public long Quantity { get; set; }

        [XmlElement("sku_id")]
        public long SkuId { get; set; }
    }
}

