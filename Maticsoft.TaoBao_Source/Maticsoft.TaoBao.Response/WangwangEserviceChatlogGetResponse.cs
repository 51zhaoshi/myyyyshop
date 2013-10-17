namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WangwangEserviceChatlogGetResponse : TopResponse
    {
        [XmlElement("count")]
        public long Count { get; set; }

        [XmlArray("msgs"), XmlArrayItem("msg")]
        public List<Msg> Msgs { get; set; }

        [XmlElement("ret")]
        public long Ret { get; set; }
    }
}

