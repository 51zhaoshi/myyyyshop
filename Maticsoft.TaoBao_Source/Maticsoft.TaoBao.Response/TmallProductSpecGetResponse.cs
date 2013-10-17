namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TmallProductSpecGetResponse : TopResponse
    {
        [XmlElement("product_spec")]
        public Maticsoft.TaoBao.Domain.ProductSpec ProductSpec { get; set; }
    }
}

