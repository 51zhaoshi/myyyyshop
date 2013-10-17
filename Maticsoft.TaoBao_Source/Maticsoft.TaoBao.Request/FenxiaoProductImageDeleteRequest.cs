namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FenxiaoProductImageDeleteRequest : ITopRequest<FenxiaoProductImageDeleteResponse>
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
            return "taobao.fenxiao.product.image.delete";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("position", this.Position);
            dictionary.Add("product_id", this.ProductId);
            dictionary.Add("properties", this.Properties);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("position", this.Position);
            RequestValidator.ValidateRequired("product_id", this.ProductId);
        }

        public long? Position { get; set; }

        public long? ProductId { get; set; }

        public string Properties { get; set; }
    }
}

