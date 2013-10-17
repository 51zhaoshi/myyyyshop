namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class CouponDetail : TopObject
    {
        [XmlElement("buyer_nick")]
        public string BuyerNick { get; set; }

        [XmlElement("channel")]
        public string Channel { get; set; }

        [XmlElement("coupon_number")]
        public long CouponNumber { get; set; }

        [XmlElement("state")]
        public string State { get; set; }
    }
}

