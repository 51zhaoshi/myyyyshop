namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class NotifyRefund : TopObject
    {
        [XmlElement("buyer_nick")]
        public string BuyerNick { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("oid")]
        public long Oid { get; set; }

        [XmlElement("refund_fee")]
        public string RefundFee { get; set; }

        [XmlElement("rid")]
        public long Rid { get; set; }

        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("tid")]
        public long Tid { get; set; }

        [XmlElement("user_id")]
        public long UserId { get; set; }
    }
}

