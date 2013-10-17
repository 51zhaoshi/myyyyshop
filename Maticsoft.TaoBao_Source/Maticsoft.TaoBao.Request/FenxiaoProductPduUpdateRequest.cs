namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FenxiaoProductPduUpdateRequest : ITopRequest<FenxiaoProductPduUpdateResponse>
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
            return "taobao.fenxiao.product.pdu.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("distributor_id", this.DistributorId);
            dictionary.Add("is_delete", this.IsDelete);
            dictionary.Add("product_id", this.ProductId);
            dictionary.Add("quantity_type", this.QuantityType);
            dictionary.Add("quantitys", this.Quantitys);
            dictionary.Add("sku_properties", this.SkuProperties);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("distributor_id", this.DistributorId);
            RequestValidator.ValidateRequired("product_id", this.ProductId);
        }

        public long? DistributorId { get; set; }

        public bool? IsDelete { get; set; }

        public long? ProductId { get; set; }

        public string Quantitys { get; set; }

        public string QuantityType { get; set; }

        public string SkuProperties { get; set; }
    }
}

