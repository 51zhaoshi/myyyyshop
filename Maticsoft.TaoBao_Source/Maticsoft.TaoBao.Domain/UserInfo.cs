namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class UserInfo : TopObject
    {
        [XmlElement("available_space")]
        public string AvailableSpace { get; set; }

        [XmlElement("free_space")]
        public string FreeSpace { get; set; }

        [XmlElement("order_expiry_date")]
        public string OrderExpiryDate { get; set; }

        [XmlElement("order_space")]
        public string OrderSpace { get; set; }

        [XmlElement("remaining_space")]
        public string RemainingSpace { get; set; }

        [XmlElement("used_space")]
        public string UsedSpace { get; set; }

        [XmlElement("water_mark")]
        public string WaterMark { get; set; }
    }
}

