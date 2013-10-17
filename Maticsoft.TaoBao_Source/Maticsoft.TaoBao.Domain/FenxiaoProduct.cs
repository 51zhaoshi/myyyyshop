namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class FenxiaoProduct : TopObject
    {
        [XmlElement("alarm_number")]
        public long AlarmNumber { get; set; }

        [XmlElement("category_id")]
        public string CategoryId { get; set; }

        [XmlElement("city")]
        public string City { get; set; }

        [XmlElement("cost_price")]
        public string CostPrice { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("dealer_cost_price")]
        public string DealerCostPrice { get; set; }

        [XmlElement("desc_path")]
        public string DescPath { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("discount_id")]
        public long DiscountId { get; set; }

        [XmlElement("have_guarantee")]
        public bool HaveGuarantee { get; set; }

        [XmlElement("have_invoice")]
        public bool HaveInvoice { get; set; }

        [XmlElement("input_properties")]
        public string InputProperties { get; set; }

        [XmlElement("is_authz")]
        public string IsAuthz { get; set; }

        [XmlElement("item_id")]
        public long ItemId { get; set; }

        [XmlElement("items_count")]
        public long ItemsCount { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("orders_count")]
        public long OrdersCount { get; set; }

        [XmlElement("outer_id")]
        public string OuterId { get; set; }

        [XmlArrayItem("fenxiao_pdu"), XmlArray("pdus")]
        public List<FenxiaoPdu> Pdus { get; set; }

        [XmlElement("pictures")]
        public string Pictures { get; set; }

        [XmlElement("pid")]
        public long Pid { get; set; }

        [XmlElement("postage_ems")]
        public string PostageEms { get; set; }

        [XmlElement("postage_fast")]
        public string PostageFast { get; set; }

        [XmlElement("postage_id")]
        public long PostageId { get; set; }

        [XmlElement("postage_ordinary")]
        public string PostageOrdinary { get; set; }

        [XmlElement("postage_type")]
        public string PostageType { get; set; }

        [XmlElement("productcat_id")]
        public long ProductcatId { get; set; }

        [XmlElement("properties")]
        public string Properties { get; set; }

        [XmlElement("property_alias")]
        public string PropertyAlias { get; set; }

        [XmlElement("prov")]
        public string Prov { get; set; }

        [XmlElement("quantity")]
        public long Quantity { get; set; }

        [XmlElement("retail_price_high")]
        public string RetailPriceHigh { get; set; }

        [XmlElement("retail_price_low")]
        public string RetailPriceLow { get; set; }

        [XmlArray("skus"), XmlArrayItem("fenxiao_sku")]
        public List<FenxiaoSku> Skus { get; set; }

        [XmlElement("standard_price")]
        public string StandardPrice { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("trade_type")]
        public string TradeType { get; set; }

        [XmlElement("upshelf_time")]
        public string UpshelfTime { get; set; }
    }
}

