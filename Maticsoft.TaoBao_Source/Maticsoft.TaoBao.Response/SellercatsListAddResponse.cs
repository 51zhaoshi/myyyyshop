namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SellercatsListAddResponse : TopResponse
    {
        [XmlElement("seller_cat")]
        public Maticsoft.TaoBao.Domain.SellerCat SellerCat { get; set; }
    }
}

