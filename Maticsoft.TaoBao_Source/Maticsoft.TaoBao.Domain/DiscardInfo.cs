namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class DiscardInfo : TopObject
    {
        [XmlElement("end")]
        public long End { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("start")]
        public long Start { get; set; }

        [XmlElement("subscribe_key")]
        public string SubscribeKey { get; set; }

        [XmlElement("subscribe_value")]
        public string SubscribeValue { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }

        [XmlElement("user_id")]
        public long UserId { get; set; }
    }
}

