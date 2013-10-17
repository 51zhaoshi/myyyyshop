namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbItemCombinationCreateResponse : TopResponse
    {
        [XmlElement("gmt_create")]
        public string GmtCreate { get; set; }
    }
}

