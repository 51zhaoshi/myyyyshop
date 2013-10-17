namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TmallBrandcatSalesproGetRequest : ITopRequest<TmallBrandcatSalesproGetResponse>
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
            return "tmall.brandcat.salespro.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("brand_id", this.BrandId);
            dictionary.Add("cat_id", this.CatId);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("brand_id", this.BrandId);
            RequestValidator.ValidateRequired("cat_id", this.CatId);
        }

        public long? BrandId { get; set; }

        public long? CatId { get; set; }
    }
}

