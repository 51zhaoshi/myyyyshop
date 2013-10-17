namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class RefundRemindTimeout : TopObject
    {
        [XmlElement("exist_timeout")]
        public bool ExistTimeout { get; set; }

        [XmlElement("remind_type")]
        public long RemindType { get; set; }

        [XmlElement("timeout")]
        public string Timeout { get; set; }
    }
}

