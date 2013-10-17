namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class TradeMonitor : TopObject
    {
        [XmlElement("buy_amount")]
        public long BuyAmount { get; set; }

        [XmlElement("buyer_nick")]
        public string BuyerNick { get; set; }

        [XmlElement("distributor_nick")]
        public string DistributorNick { get; set; }

        [XmlElement("item_id")]
        public long ItemId { get; set; }

        [XmlElement("item_number")]
        public string ItemNumber { get; set; }

        [XmlElement("item_price")]
        public long ItemPrice { get; set; }

        [XmlElement("item_sku_name")]
        public string ItemSkuName { get; set; }

        [XmlElement("item_sku_number")]
        public string ItemSkuNumber { get; set; }

        [XmlElement("item_title")]
        public string ItemTitle { get; set; }

        [XmlElement("item_total_price")]
        public long ItemTotalPrice { get; set; }

        [XmlElement("pay_time")]
        public string PayTime { get; set; }

        [XmlElement("product_id")]
        public long ProductId { get; set; }

        [XmlElement("product_number")]
        public string ProductNumber { get; set; }

        [XmlElement("product_sku_number")]
        public string ProductSkuNumber { get; set; }

        [XmlElement("product_title")]
        public string ProductTitle { get; set; }

        [XmlElement("retail_price_high")]
        public long RetailPriceHigh { get; set; }

        [XmlElement("retail_price_low")]
        public long RetailPriceLow { get; set; }

        [XmlElement("shipping_address")]
        public string ShippingAddress { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("sub_tc_order_id")]
        public long SubTcOrderId { get; set; }

        [XmlElement("supplier_nick")]
        public string SupplierNick { get; set; }

        [XmlElement("tc_order_id")]
        public long TcOrderId { get; set; }

        [XmlElement("trade_monitor_id")]
        public long TradeMonitorId { get; set; }
    }
}

