namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ShopRemainshowcaseGetResponse : TopResponse
    {
        [XmlElement("shop")]
        public Maticsoft.TaoBao.Domain.Shop Shop { get; set; }
    }
}

