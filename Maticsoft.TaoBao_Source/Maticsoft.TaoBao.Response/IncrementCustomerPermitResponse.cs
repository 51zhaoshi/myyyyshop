namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class IncrementCustomerPermitResponse : TopResponse
    {
        [XmlElement("app_customer")]
        public Maticsoft.TaoBao.Domain.AppCustomer AppCustomer { get; set; }
    }
}

