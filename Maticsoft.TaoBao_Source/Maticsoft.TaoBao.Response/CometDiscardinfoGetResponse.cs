namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class CometDiscardinfoGetResponse : TopResponse
    {
        [XmlArray("discard_info_list"), XmlArrayItem("discard_info")]
        public List<DiscardInfo> DiscardInfoList { get; set; }
    }
}

