namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TopatsSimbaCampkeywordbaseGetResponse : TopResponse
    {
        [XmlElement("task")]
        public Maticsoft.TaoBao.Domain.Task Task { get; set; }
    }
}

