namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class AlipayUserContractGetResponse : TopResponse
    {
        [XmlElement("alipay_contract")]
        public Maticsoft.TaoBao.Domain.AlipayContract AlipayContract { get; set; }
    }
}

