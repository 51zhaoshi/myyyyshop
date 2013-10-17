namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class SubUserInfo : TopObject
    {
        [XmlElement("full_name")]
        public string FullName { get; set; }

        [XmlElement("is_online")]
        public long IsOnline { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("seller_id")]
        public long SellerId { get; set; }

        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        [XmlElement("status")]
        public long Status { get; set; }

        [XmlElement("sub_id")]
        public long SubId { get; set; }
    }
}

