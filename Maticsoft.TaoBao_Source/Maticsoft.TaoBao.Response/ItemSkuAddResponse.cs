namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ItemSkuAddResponse : TopResponse
    {
        [XmlElement("sku")]
        public Maticsoft.TaoBao.Domain.Sku Sku { get; set; }
    }
}

