namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FenxiaoProductAddRequest : ITopUploadRequest<FenxiaoProductAddResponse>, ITopRequest<FenxiaoProductAddResponse>
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
            return "taobao.fenxiao.product.add";
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
            dictionary.Add("alarm_number", this.AlarmNumber);
            dictionary.Add("category_id", this.CategoryId);
            dictionary.Add("city", this.City);
            dictionary.Add("cost_price", this.CostPrice);
            dictionary.Add("dealer_cost_price", this.DealerCostPrice);
            dictionary.Add("desc", this.Desc);
            dictionary.Add("discount_id", this.DiscountId);
            dictionary.Add("have_guarantee", this.HaveGuarantee);
            dictionary.Add("have_invoice", this.HaveInvoice);
            dictionary.Add("input_properties", this.InputProperties);
            dictionary.Add("is_authz", this.IsAuthz);
            dictionary.Add("item_id", this.ItemId);
            dictionary.Add("name", this.Name);
            dictionary.Add("outer_id", this.OuterId);
            dictionary.Add("pic_path", this.PicPath);
            dictionary.Add("postage_ems", this.PostageEms);
            dictionary.Add("postage_fast", this.PostageFast);
            dictionary.Add("postage_id", this.PostageId);
            dictionary.Add("postage_ordinary", this.PostageOrdinary);
            dictionary.Add("postage_type", this.PostageType);
            dictionary.Add("productcat_id", this.ProductcatId);
            dictionary.Add("properties", this.Properties);
            dictionary.Add("property_alias", this.PropertyAlias);
            dictionary.Add("prov", this.Prov);
            dictionary.Add("quantity", this.Quantity);
            dictionary.Add("retail_price_high", this.RetailPriceHigh);
            dictionary.Add("retail_price_low", this.RetailPriceLow);
            dictionary.Add("sku_cost_prices", this.SkuCostPrices);
            dictionary.Add("sku_dealer_cost_prices", this.SkuDealerCostPrices);
            dictionary.Add("sku_outer_ids", this.SkuOuterIds);
            dictionary.Add("sku_properties", this.SkuProperties);
            dictionary.Add("sku_quantitys", this.SkuQuantitys);
            dictionary.Add("sku_standard_prices", this.SkuStandardPrices);
            dictionary.Add("standard_price", this.StandardPrice);
            dictionary.Add("trade_type", this.TradeType);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("alarm_number", this.AlarmNumber);
            RequestValidator.ValidateRequired("category_id", this.CategoryId);
            RequestValidator.ValidateRequired("city", this.City);
            RequestValidator.ValidateRequired("desc", this.Desc);
            RequestValidator.ValidateRequired("have_guarantee", this.HaveGuarantee);
            RequestValidator.ValidateRequired("have_invoice", this.HaveInvoice);
            RequestValidator.ValidateRequired("name", this.Name);
            RequestValidator.ValidateRequired("postage_type", this.PostageType);
            RequestValidator.ValidateRequired("productcat_id", this.ProductcatId);
            RequestValidator.ValidateRequired("prov", this.Prov);
            RequestValidator.ValidateRequired("quantity", this.Quantity);
            RequestValidator.ValidateRequired("retail_price_high", this.RetailPriceHigh);
            RequestValidator.ValidateRequired("retail_price_low", this.RetailPriceLow);
            RequestValidator.ValidateRequired("standard_price", this.StandardPrice);
        }

        public long? AlarmNumber { get; set; }

        public long? CategoryId { get; set; }

        public string City { get; set; }

        public string CostPrice { get; set; }

        public string DealerCostPrice { get; set; }

        public string Desc { get; set; }

        public long? DiscountId { get; set; }

        public string HaveGuarantee { get; set; }

        public string HaveInvoice { get; set; }

        public FileItem Image { get; set; }

        public string InputProperties { get; set; }

        public string IsAuthz { get; set; }

        public long? ItemId { get; set; }

        public string Name { get; set; }

        public string OuterId { get; set; }

        public string PicPath { get; set; }

        public string PostageEms { get; set; }

        public string PostageFast { get; set; }

        public long? PostageId { get; set; }

        public string PostageOrdinary { get; set; }

        public string PostageType { get; set; }

        public long? ProductcatId { get; set; }

        public string Properties { get; set; }

        public string PropertyAlias { get; set; }

        public string Prov { get; set; }

        public long? Quantity { get; set; }

        public string RetailPriceHigh { get; set; }

        public string RetailPriceLow { get; set; }

        public string SkuCostPrices { get; set; }

        public string SkuDealerCostPrices { get; set; }

        public string SkuOuterIds { get; set; }

        public string SkuProperties { get; set; }

        public string SkuQuantitys { get; set; }

        public string SkuStandardPrices { get; set; }

        public string StandardPrice { get; set; }

        public string TradeType { get; set; }
    }
}

