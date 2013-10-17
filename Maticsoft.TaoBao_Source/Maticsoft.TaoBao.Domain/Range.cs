namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Range : TopObject
    {
        [XmlElement("participate_id")]
        public long ParticipateId { get; set; }

        [XmlElement("participate_type")]
        public long ParticipateType { get; set; }
    }
}

