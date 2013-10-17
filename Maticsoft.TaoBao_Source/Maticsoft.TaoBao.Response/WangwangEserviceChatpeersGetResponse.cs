namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WangwangEserviceChatpeersGetResponse : TopResponse
    {
        [XmlArray("chatpeers"), XmlArrayItem("chatpeer")]
        public List<Chatpeer> Chatpeers { get; set; }

        [XmlElement("count")]
        public long Count { get; set; }

        [XmlElement("ret")]
        public long Ret { get; set; }
    }
}

