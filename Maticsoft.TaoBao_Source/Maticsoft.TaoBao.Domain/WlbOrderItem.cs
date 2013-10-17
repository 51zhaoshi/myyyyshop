namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WlbOrderItem : TopObject
    {
        [XmlElement("batch_remark")]
        public string BatchRemark { get; set; }

        [XmlElement("confirm_status")]
        public string ConfirmStatus { get; set; }

        [XmlElement("ext_entity_id")]
        public string ExtEntityId { get; set; }

        [XmlElement("ext_entity_type")]
        public string ExtEntityType { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("inventory_type")]
        public string InventoryType { get; set; }

        [XmlElement("item_code")]
        public string ItemCode { get; set; }

        [XmlElement("item_id")]
        public long ItemId { get; set; }

        [XmlElement("item_name")]
        public string ItemName { get; set; }

        [XmlElement("item_price")]
        public long ItemPrice { get; set; }

        [XmlElement("order_code")]
        public string OrderCode { get; set; }

        [XmlElement("order_id")]
        public long OrderId { get; set; }

        [XmlElement("order_sub_2code")]
        public string OrderSub2code { get; set; }

        [XmlElement("order_sub_code")]
        public string OrderSubCode { get; set; }

        [XmlElement("order_sub_type")]
        public string OrderSubType { get; set; }

        [XmlElement("picture_url")]
        public string PictureUrl { get; set; }

        [XmlElement("plan_quantity")]
        public long PlanQuantity { get; set; }

        [XmlElement("provider_tp_id")]
        public long ProviderTpId { get; set; }

        [XmlElement("provider_tp_nick")]
        public string ProviderTpNick { get; set; }

        [XmlElement("publish_version")]
        public long PublishVersion { get; set; }

        [XmlElement("real_quantity")]
        public long RealQuantity { get; set; }

        [XmlElement("remark")]
        public string Remark { get; set; }

        [XmlElement("user_id")]
        public long UserId { get; set; }

        [XmlElement("user_nick")]
        public string UserNick { get; set; }
    }
}

