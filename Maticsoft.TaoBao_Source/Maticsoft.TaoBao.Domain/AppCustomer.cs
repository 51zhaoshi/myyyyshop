namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class AppCustomer : TopObject
    {
        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlArrayItem("subscription"), XmlArray("subscriptions")]
        public List<Subscription> Subscriptions { get; set; }

        [XmlArray("type"), XmlArrayItem("string")]
        public List<string> Type { get; set; }

        [XmlElement("user_id")]
        public long UserId { get; set; }
    }
}

