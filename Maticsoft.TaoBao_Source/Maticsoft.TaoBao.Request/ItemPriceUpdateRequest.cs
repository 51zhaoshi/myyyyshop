namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ItemPriceUpdateRequest : ITopUploadRequest<ItemPriceUpdateResponse>, ITopRequest<ItemPriceUpdateResponse>
    {
        private IDictionary<string, string> otherParameters;

        public void AddOtherParameter(string key, string value)
        {
            if (this.otherParameters == null)
            {
                this.otherParameters = new TopDictionary();
            }
            this.otherParameters.Add(key, value);
        }

        public string GetApiName()
        {
            return "taobao.item.price.update";
        }

        public IDictionary<string, FileItem> GetFileParameters()
        {
            IDictionary<string, FileItem> dictionary = new Dictionary<string, FileItem>();
            dictionary.Add("image", this.Image);
            return dictionary;
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("after_sale_id", this.AfterSaleId);
            dictionary.Add("approve_status", this.ApproveStatus);
            dictionary.Add("auction_point", this.AuctionPoint);
            dictionary.Add("auto_fill", this.AutoFill);
            dictionary.Add("cid", this.Cid);
            dictionary.Add("cod_postage_id", this.CodPostageId);
            dictionary.Add("desc", this.Desc);
            dictionary.Add("ems_fee", this.EmsFee);
            dictionary.Add("express_fee", this.ExpressFee);
            dictionary.Add("freight_payer", this.FreightPayer);
            dictionary.Add("has_discount", this.HasDiscount);
            dictionary.Add("has_invoice", this.HasInvoice);
            dictionary.Add("has_showcase", this.HasShowcase);
            dictionary.Add("has_warranty", this.HasWarranty);
            dictionary.Add("increment", this.Increment);
            dictionary.Add("input_pids", this.InputPids);
            dictionary.Add("input_str", this.InputStr);
            dictionary.Add("is_3D", this.Is3D);
            dictionary.Add("is_ex", this.IsEx);
            dictionary.Add("is_lightning_consignment", this.IsLightningConsignment);
            dictionary.Add("is_replace_sku", this.IsReplaceSku);
            dictionary.Add("is_taobao", this.IsTaobao);
            dictionary.Add("is_xinpin", this.IsXinpin);
            dictionary.Add("lang", this.Lang);
            dictionary.Add("list_time", this.ListTime);
            dictionary.Add("location.city", this.LocationCity);
            dictionary.Add("location.state", this.LocationState);
            dictionary.Add("num", this.Num);
            dictionary.Add("num_iid", this.NumIid);
            dictionary.Add("outer_id", this.OuterId);
            dictionary.Add("pic_path", this.PicPath);
            dictionary.Add("post_fee", this.PostFee);
            dictionary.Add("postage_id", this.PostageId);
            dictionary.Add("price", this.Price);
            dictionary.Add("product_id", this.ProductId);
            dictionary.Add("property_alias", this.PropertyAlias);
            dictionary.Add("props", this.Props);
            dictionary.Add("sell_promise", this.SellPromise);
            dictionary.Add("seller_cids", this.SellerCids);
            dictionary.Add("sku_outer_ids", this.SkuOuterIds);
            dictionary.Add("sku_prices", this.SkuPrices);
            dictionary.Add("sku_properties", this.SkuProperties);
            dictionary.Add("sku_quantities", this.SkuQuantities);
            dictionary.Add("stuff_status", this.StuffStatus);
            dictionary.Add("sub_stock", this.SubStock);
            dictionary.Add("title", this.Title);
            dictionary.Add("valid_thru", this.ValidThru);
            dictionary.Add("weight", this.Weight);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMinValue("cid", this.Cid, 0L);
            RequestValidator.ValidateMaxLength("desc", this.Desc, 0x30d40);
            RequestValidator.ValidateMaxLength("image", this.Image, 0x80000);
            RequestValidator.ValidateMaxValue("num", this.Num, 0xf423fL);
            RequestValidator.ValidateMinValue("num", this.Num, 0L);
            RequestValidator.ValidateRequired("num_iid", this.NumIid);
            RequestValidator.ValidateMinValue("num_iid", this.NumIid, 1L);
            RequestValidator.ValidateMaxListSize("seller_cids", this.SellerCids, 10);
            RequestValidator.ValidateMaxLength("title", this.Title, 60);
        }

        public long? AfterSaleId { get; set; }

        public string ApproveStatus { get; set; }

        public long? AuctionPoint { get; set; }

        public string AutoFill { get; set; }

        public long? Cid { get; set; }

        public long? CodPostageId { get; set; }

        public string Desc { get; set; }

        public string EmsFee { get; set; }

        public string ExpressFee { get; set; }

        public string FreightPayer { get; set; }

        public bool? HasDiscount { get; set; }

        public bool? HasInvoice { get; set; }

        public bool? HasShowcase { get; set; }

        public bool? HasWarranty { get; set; }

        public FileItem Image { get; set; }

        public string Increment { get; set; }

        public string InputPids { get; set; }

        public string InputStr { get; set; }

        public bool? Is3D { get; set; }

        public bool? IsEx { get; set; }

        public bool? IsLightningConsignment { get; set; }

        public bool? IsReplaceSku { get; set; }

        public bool? IsTaobao { get; set; }

        public bool? IsXinpin { get; set; }

        public string Lang { get; set; }

        public DateTime? ListTime { get; set; }

        public string LocationCity { get; set; }

        public string LocationState { get; set; }

        public long? Num { get; set; }

        public long? NumIid { get; set; }

        public string OuterId { get; set; }

        public string PicPath { get; set; }

        public long? PostageId { get; set; }

        public string PostFee { get; set; }

        public string Price { get; set; }

        public long? ProductId { get; set; }

        public string PropertyAlias { get; set; }

        public string Props { get; set; }

        public string SellerCids { get; set; }

        public bool? SellPromise { get; set; }

        public string SkuOuterIds { get; set; }

        public string SkuPrices { get; set; }

        public string SkuProperties { get; set; }

        public string SkuQuantities { get; set; }

        public string StuffStatus { get; set; }

        public long? SubStock { get; set; }

        public string Title { get; set; }

        public long? ValidThru { get; set; }

        public long? Weight { get; set; }
    }
}

