namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class UmpToolUpdateResponse : TopResponse
    {
        [XmlElement("tool_id")]
        public long ToolId { get; set; }
    }
}

