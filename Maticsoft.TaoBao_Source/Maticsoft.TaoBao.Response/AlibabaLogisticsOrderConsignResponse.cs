namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class AlibabaLogisticsOrderConsignResponse : TopResponse
    {
        [XmlElement("consign_result")]
        public Maticsoft.TaoBao.Domain.ConsignResult ConsignResult { get; set; }
    }
}

