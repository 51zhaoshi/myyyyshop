namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class NotifyTrade : TopObject
    {
        [XmlElement("buyer_nick")]
        public string BuyerNick { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("oid")]
        public long Oid { get; set; }

        [XmlElement("payment")]
        public string Payment { get; set; }

        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("tid")]
        public long Tid { get; set; }

        [XmlElement("trade_mark")]
        public string TradeMark { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }

        [XmlElement("user_id")]
        public long UserId { get; set; }
    }
}

