namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class LogisticsAddressModifyResponse : TopResponse
    {
        [XmlElement("address_result")]
        public Maticsoft.TaoBao.Domain.AddressResult AddressResult { get; set; }
    }
}

