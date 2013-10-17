namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class AlipayUserAccountGetResponse : TopResponse
    {
        [XmlElement("alipay_account")]
        public Maticsoft.TaoBao.Domain.AlipayAccount AlipayAccount { get; set; }
    }
}
