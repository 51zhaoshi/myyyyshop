namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Item : TopObject
    {
        [XmlElement("after_sale_id")]
        public long AfterSaleId { get; set; }

        [XmlElement("approve_status")]
        public string ApproveStatus { get; set; }

        [XmlElement("auction_point")]
        public long AuctionPoint { get; set; }

        [XmlElement("auto_fill")]
        public string AutoFill { get; set; }

        [XmlElement("cid")]
        public long Cid { get; set; }

        [XmlElement("cod_postage_id")]
        public long CodPostageId { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("delist_time")]
        public string DelistTime { get; set; }

        [XmlElement("desc")]
        public string Desc { get; set; }

        [XmlElement("desc_module_info")]
        public Maticsoft.TaoBao.Domain.DescModuleInfo DescModuleInfo { get; set; }

        [XmlElement("detail_url")]
        public string DetailUrl { get; set; }

        [XmlElement("ems_fee")]
        public string EmsFee { get; set; }

        [XmlElement("express_fee")]
        public string ExpressFee { get; set; }

        [XmlElement("features")]
        public string Features { get; set; }

        [XmlElement("food_security")]
        public Maticsoft.TaoBao.Domain.FoodSecurity FoodSecurity { get; set; }

        [XmlElement("freight_payer")]
        public string FreightPayer { get; set; }

        [XmlElement("global_stock_type")]
        public string GlobalStockType { get; set; }

        [XmlElement("has_discount")]
        public bool HasDiscount { get; set; }

        [XmlElement("has_invoice")]
        public bool HasInvoice { get; set; }

        [XmlElement("has_showcase")]
        public bool HasShowcase { get; set; }

        [XmlElement("has_warranty")]
        public bool HasWarranty { get; set; }

        [XmlElement("increment")]
        public string Increment { get; set; }

        [XmlElement("inner_shop_auction_template_id")]
        public long InnerShopAuctionTemplateId { get; set; }

        [XmlElement("input_pids")]
        public string InputPids { get; set; }

        [XmlElement("input_str")]
        public string InputStr { get; set; }

        [XmlElement("is_3D")]
        public bool Is3D { get; set; }

        [XmlElement("is_ex")]
        public bool IsEx { get; set; }

        [XmlElement("is_fenxiao")]
        public long IsFenxiao { get; set; }

        [XmlElement("is_lightning_consignment")]
        public bool IsLightningConsignment { get; set; }

        [XmlElement("is_prepay")]
        public bool IsPrepay { get; set; }

        [XmlElement("is_taobao")]
        public bool IsTaobao { get; set; }

        [XmlElement("is_timing")]
        public bool IsTiming { get; set; }

        [XmlElement("is_virtual")]
        public bool IsVirtual { get; set; }

        [XmlElement("is_xinpin")]
        public bool IsXinpin { get; set; }

        [XmlArrayItem("item_img"), XmlArray("item_imgs")]
        public List<ItemImg> ItemImgs { get; set; }

        [XmlElement("list_time")]
        public string ListTime { get; set; }

        [XmlElement("locality_life")]
        public Maticsoft.TaoBao.Domain.LocalityLife LocalityLife { get; set; }

        [XmlElement("location")]
        public Maticsoft.TaoBao.Domain.Location Location { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("num")]
        public long Num { get; set; }

        [XmlElement("num_iid")]
        public long NumIid { get; set; }

        [XmlElement("one_station")]
        public bool OneStation { get; set; }

        [XmlElement("outer_id")]
        public string OuterId { get; set; }

        [XmlElement("outer_shop_auction_template_id")]
        public long OuterShopAuctionTemplateId { get; set; }

        [XmlElement("pic_url")]
        public string PicUrl { get; set; }

        [XmlElement("postage_id")]
        public long PostageId { get; set; }

        [XmlElement("post_fee")]
        public string PostFee { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("product_id")]
        public long ProductId { get; set; }

        [XmlElement("promoted_service")]
        public string PromotedService { get; set; }

        [XmlElement("property_alias")]
        public string PropertyAlias { get; set; }

        [XmlArray("prop_imgs"), XmlArrayItem("prop_img")]
        public List<PropImg> PropImgs { get; set; }

        [XmlElement("props")]
        public string Props { get; set; }

        [XmlElement("props_name")]
        public string PropsName { get; set; }

        [XmlElement("score")]
        public long Score { get; set; }

        [XmlElement("second_kill")]
        public string SecondKill { get; set; }

        [XmlElement("seller_cids")]
        public string SellerCids { get; set; }

        [XmlElement("sell_promise")]
        public bool SellPromise { get; set; }

        [XmlArrayItem("sku"), XmlArray("skus")]
        public List<Sku> Skus { get; set; }

        [XmlElement("stuff_status")]
        public string StuffStatus { get; set; }

        [XmlElement("sub_stock")]
        public long SubStock { get; set; }

        [XmlElement("template_id")]
        public string TemplateId { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }

        [XmlElement("valid_thru")]
        public long ValidThru { get; set; }

        [XmlArrayItem("video"), XmlArray("videos")]
        public List<Video> Videos { get; set; }

        [XmlElement("violation")]
        public bool Violation { get; set; }

        [XmlElement("volume")]
        public long Volume { get; set; }

        [XmlElement("wap_desc")]
        public string WapDesc { get; set; }

        [XmlElement("wap_detail_url")]
        public string WapDetailUrl { get; set; }

        [XmlElement("ww_status")]
        public bool WwStatus { get; set; }
    }
}

