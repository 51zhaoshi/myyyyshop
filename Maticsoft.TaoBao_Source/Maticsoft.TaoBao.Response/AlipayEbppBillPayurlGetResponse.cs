namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class AlipayEbppBillPayurlGetResponse : TopResponse
    {
        [XmlElement("pay_url")]
        public string PayUrl { get; set; }
    }
}

