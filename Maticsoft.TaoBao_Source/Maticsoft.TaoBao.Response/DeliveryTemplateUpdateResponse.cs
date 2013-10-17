namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class DeliveryTemplateUpdateResponse : TopResponse
    {
        [XmlElement("complete")]
        public bool Complete { get; set; }
    }
}

