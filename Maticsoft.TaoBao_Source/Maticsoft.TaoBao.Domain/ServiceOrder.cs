namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class ServiceOrder : TopObject
    {
        [XmlElement("buyer_nick")]
        public string BuyerNick { get; set; }

        [XmlElement("item_oid")]
        public long ItemOid { get; set; }

        [XmlElement("num")]
        public long Num { get; set; }

        [XmlElement("oid")]
        public long Oid { get; set; }

        [XmlElement("payment")]
        public string Payment { get; set; }

        [XmlElement("pic_path")]
        public string PicPath { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("refund_id")]
        public long RefundId { get; set; }

        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        [XmlElement("service_detail_url")]
        public string ServiceDetailUrl { get; set; }

        [XmlElement("service_id")]
        public long ServiceId { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("total_fee")]
        public string TotalFee { get; set; }
    }
}

