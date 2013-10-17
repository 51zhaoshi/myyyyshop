namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class UmpActivityGetResponse : TopResponse
    {
        [XmlElement("content")]
        public string Content { get; set; }
    }
}

