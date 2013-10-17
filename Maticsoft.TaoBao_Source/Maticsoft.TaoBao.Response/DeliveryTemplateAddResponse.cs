namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class DeliveryTemplateAddResponse : TopResponse
    {
        [XmlElement("delivery_template")]
        public Maticsoft.TaoBao.Domain.DeliveryTemplate DeliveryTemplate { get; set; }
    }
}

