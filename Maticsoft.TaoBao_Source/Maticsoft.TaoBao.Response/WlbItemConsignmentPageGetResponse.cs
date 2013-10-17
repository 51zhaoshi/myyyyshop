namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbItemConsignmentPageGetResponse : TopResponse
    {
        [XmlElement("total_count")]
        public long TotalCount { get; set; }

        [XmlArrayItem("wlb_consign_ment"), XmlArray("wlb_consign_ments")]
        public List<WlbConsignMent> WlbConsignMents { get; set; }
    }
}

