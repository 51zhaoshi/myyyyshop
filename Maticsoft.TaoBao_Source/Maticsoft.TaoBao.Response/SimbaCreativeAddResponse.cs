namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaCreativeAddResponse : TopResponse
    {
        [XmlElement("creative")]
        public Maticsoft.TaoBao.Domain.Creative Creative { get; set; }
    }
}

