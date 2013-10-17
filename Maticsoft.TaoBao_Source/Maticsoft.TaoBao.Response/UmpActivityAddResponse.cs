namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class UmpActivityAddResponse : TopResponse
    {
        [XmlElement("act_id")]
        public long ActId { get; set; }
    }
}

