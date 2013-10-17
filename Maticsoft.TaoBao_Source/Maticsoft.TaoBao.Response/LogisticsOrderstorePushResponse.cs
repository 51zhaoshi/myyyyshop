namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class LogisticsOrderstorePushResponse : TopResponse
    {
        [XmlElement("shipping")]
        public Maticsoft.TaoBao.Domain.Shipping Shipping { get; set; }
    }
}

