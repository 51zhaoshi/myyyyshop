namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class UmpPromotionGetResponse : TopResponse
    {
        [XmlElement("promotions")]
        public PromotionDisplayTop Promotions { get; set; }
    }
}

