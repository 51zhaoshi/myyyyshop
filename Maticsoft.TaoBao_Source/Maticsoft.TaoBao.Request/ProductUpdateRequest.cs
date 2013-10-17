namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ProductUpdateRequest : ITopUploadRequest<ProductUpdateResponse>, ITopRequest<ProductUpdateResponse>
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
            return "taobao.product.update";
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
            dictionary.Add("desc", this.Desc);
            dictionary.Add("major", this.Major);
            dictionary.Add("name", this.Name);
            dictionary.Add("native_unkeyprops", this.NativeUnkeyprops);
            dictionary.Add("outer_id", this.OuterId);
            dictionary.Add("packing_list", this.PackingList);
            dictionary.Add("price", this.Price);
            dictionary.Add("product_id", this.ProductId);
            dictionary.Add("sale_props", this.SaleProps);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxLength("image", this.Image, 0x100000);
            RequestValidator.ValidateRequired("product_id", this.ProductId);
        }

        public string Binds { get; set; }

        public string Desc { get; set; }

        public FileItem Image { get; set; }

        public bool? Major { get; set; }

        public string Name { get; set; }

        public string NativeUnkeyprops { get; set; }

        public string OuterId { get; set; }

        public string PackingList { get; set; }

        public string Price { get; set; }

        public long? ProductId { get; set; }

        public string SaleProps { get; set; }
    }
}

