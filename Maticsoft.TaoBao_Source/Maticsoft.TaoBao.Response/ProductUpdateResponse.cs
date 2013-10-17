namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ProductUpdateResponse : TopResponse
    {
        [XmlElement("product")]
        public Maticsoft.TaoBao.Domain.Product Product { get; set; }
    }
}

