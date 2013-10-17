namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaCreativeUpdateResponse : TopResponse
    {
        [XmlElement("creativerecord")]
        public CreativeRecord Creativerecord { get; set; }
    }
}

