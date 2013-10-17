namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Shipping : TopObject
    {
        [XmlElement("buyer_nick")]
        public string BuyerNick { get; set; }

        [XmlElement("company_name")]
        public string CompanyName { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("delivery_end")]
        public string DeliveryEnd { get; set; }

        [XmlElement("delivery_start")]
        public string DeliveryStart { get; set; }

        [XmlElement("freight_payer")]
        public string FreightPayer { get; set; }

        [XmlElement("is_quick_cod_order")]
        public bool IsQuickCodOrder { get; set; }

        [XmlElement("is_success")]
        public bool IsSuccess { get; set; }

        [XmlElement("item_title")]
        public string ItemTitle { get; set; }

        [XmlElement("location")]
        public Maticsoft.TaoBao.Domain.Location Location { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("order_code")]
        public string OrderCode { get; set; }

        [XmlElement("out_sid")]
        public string OutSid { get; set; }

        [XmlElement("receiver_mobile")]
        public string ReceiverMobile { get; set; }

        [XmlElement("receiver_name")]
        public string ReceiverName { get; set; }

        [XmlElement("receiver_phone")]
        public string ReceiverPhone { get; set; }

        [XmlElement("seller_confirm")]
        public string SellerConfirm { get; set; }

        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("tid")]
        public long Tid { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }
    }
}

