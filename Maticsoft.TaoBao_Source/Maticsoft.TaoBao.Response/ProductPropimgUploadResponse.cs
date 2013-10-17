namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ProductPropimgUploadResponse : TopResponse
    {
        [XmlElement("product_prop_img")]
        public Maticsoft.TaoBao.Domain.ProductPropImg ProductPropImg { get; set; }
    }
}

