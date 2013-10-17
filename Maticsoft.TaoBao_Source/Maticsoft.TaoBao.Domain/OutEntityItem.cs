namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class OutEntityItem : TopObject
    {
        [XmlElement("entity_id")]
        public string EntityId { get; set; }

        [XmlElement("entity_type")]
        public string EntityType { get; set; }
    }
}

