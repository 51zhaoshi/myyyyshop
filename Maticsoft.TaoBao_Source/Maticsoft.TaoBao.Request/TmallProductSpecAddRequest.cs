namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TmallProductSpecAddRequest : ITopUploadRequest<TmallProductSpecAddResponse>, ITopRequest<TmallProductSpecAddResponse>
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
            return "tmall.product.spec.add";
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
            dictionary.Add("barcode", this.Barcode);
            dictionary.Add("certified_pic_str", this.CertifiedPicStr);
            dictionary.Add("product_id", this.ProductId);
            dictionary.Add("spec_props", this.SpecProps);
            dictionary.Add("spec_props_alias", this.SpecPropsAlias);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("barcode", this.Barcode);
            RequestValidator.ValidateRequired("image", this.Image);
            RequestValidator.ValidateRequired("product_id", this.ProductId);
            RequestValidator.ValidateRequired("spec_props", this.SpecProps);
            RequestValidator.ValidateMaxLength("spec_props_alias", this.SpecPropsAlias, 60);
        }

        public string Barcode { get; set; }

        public string CertifiedPicStr { get; set; }

        public FileItem Image { get; set; }

        public long? ProductId { get; set; }

        public string SpecProps { get; set; }

        public string SpecPropsAlias { get; set; }
    }
}

