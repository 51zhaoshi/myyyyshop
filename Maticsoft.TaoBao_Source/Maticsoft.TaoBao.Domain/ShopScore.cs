namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class ShopScore : TopObject
    {
        [XmlElement("delivery_score")]
        public string DeliveryScore { get; set; }

        [XmlElement("item_score")]
        public string ItemScore { get; set; }

        [XmlElement("service_score")]
        public string ServiceScore { get; set; }
    }
}

