namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class AlibabaLogisticsOrderChargeResponse : TopResponse
    {
        [XmlElement("order_charge")]
        public Maticsoft.TaoBao.Domain.OrderCharge OrderCharge { get; set; }
    }
}

