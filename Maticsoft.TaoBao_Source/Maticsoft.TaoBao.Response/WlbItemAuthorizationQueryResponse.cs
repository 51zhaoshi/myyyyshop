namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbItemAuthorizationQueryResponse : TopResponse
    {
        [XmlArray("authorization_list"), XmlArrayItem("wlb_authorization")]
        public List<WlbAuthorization> AuthorizationList { get; set; }

        [XmlElement("total_count")]
        public long TotalCount { get; set; }
    }
}

