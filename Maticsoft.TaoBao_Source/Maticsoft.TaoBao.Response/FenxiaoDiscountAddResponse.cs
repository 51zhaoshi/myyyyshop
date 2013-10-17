namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class FenxiaoDiscountAddResponse : TopResponse
    {
        [XmlElement("discount_id")]
        public long DiscountId { get; set; }
    }
}

