namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbOrderConsignResponse : TopResponse
    {
        [XmlElement("modify_time")]
        public string ModifyTime { get; set; }
    }
}

