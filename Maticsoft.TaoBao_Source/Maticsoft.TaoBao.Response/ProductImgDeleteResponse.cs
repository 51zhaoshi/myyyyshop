namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ProductImgDeleteResponse : TopResponse
    {
        [XmlElement("product_img")]
        public Maticsoft.TaoBao.Domain.ProductImg ProductImg { get; set; }
    }
}

