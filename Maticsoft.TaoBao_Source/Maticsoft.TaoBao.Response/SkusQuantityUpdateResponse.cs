namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SkusQuantityUpdateResponse : TopResponse
    {
        [XmlElement("item")]
        public Maticsoft.TaoBao.Domain.Item Item { get; set; }
    }
}

