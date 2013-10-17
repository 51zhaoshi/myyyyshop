namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbItemCombinationDeleteResponse : TopResponse
    {
        [XmlElement("gmt_modified")]
        public string GmtModified { get; set; }
    }
}

