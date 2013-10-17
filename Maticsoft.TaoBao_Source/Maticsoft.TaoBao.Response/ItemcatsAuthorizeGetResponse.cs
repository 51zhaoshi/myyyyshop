namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ItemcatsAuthorizeGetResponse : TopResponse
    {
        [XmlElement("seller_authorize")]
        public Maticsoft.TaoBao.Domain.SellerAuthorize SellerAuthorize { get; set; }
    }
}

