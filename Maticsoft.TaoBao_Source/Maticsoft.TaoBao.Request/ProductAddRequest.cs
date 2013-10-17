namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ProductAddRequest : ITopUploadRequest<ProductAddResponse>, ITopRequest<ProductAddResponse>
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
            return "taobao.product.add";
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
            dictionary.Add("binds", this.Binds);
            dictionary.Add("cid", this.Cid);
            dictionary.Add("customer_props", this.CustomerProps);
            dictionary.Add("desc", this.Desc);
            dictionary.Add("major", this.Major);
            dictionary.Add("market_time", this.MarketTime);
            dictionary.Add("name", this.Name);
            dictionary.Add("outer_id", this.OuterId);
            dictionary.Add("packing_list", this.PackingList);
            dictionary.Add("price", this.Price);
            dictionary.Add("property_alias", this.PropertyAlias);
            dictionary.Add("props", this.Props);
            dictionary.Add("sale_props", this.SaleProps);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxLength("binds", this.Binds, 0x200);
            RequestValidator.ValidateRequired("cid", this.Cid);
            RequestValidator.ValidateRequired("image", this.Image);
            RequestValidator.ValidateMaxLength("image", this.Image, 0x100000);
            RequestValidator.ValidateRequired("name", this.Name);
            RequestValidator.ValidateRequired("price", this.Price);
        }

        public string Binds { get; set; }

        public long? Cid { get; set; }

        public string CustomerProps { get; set; }

        public string Desc { get; set; }

        public FileItem Image { get; set; }

        public bool? Major { get; set; }

        public DateTime? MarketTime { get; set; }

        public string Name { get; set; }

        public string OuterId { get; set; }

        public string PackingList { get; set; }

        public string Price { get; set; }

        public string PropertyAlias { get; set; }

        public string Props { get; set; }

        public string SaleProps { get; set; }
    }
}

