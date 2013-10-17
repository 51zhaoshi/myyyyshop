namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbOrderCreateResponse : TopResponse
    {
        [XmlElement("create_time")]
        public string CreateTime { get; set; }

        [XmlElement("order_code")]
        public string OrderCode { get; set; }
    }
}

