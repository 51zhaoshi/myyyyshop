namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WlbItemAddRequest : ITopRequest<WlbItemAddResponse>
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
            return "taobao.wlb.item.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("color", this.Color);
            dictionary.Add("goods_cat", this.GoodsCat);
            dictionary.Add("height", this.Height);
            dictionary.Add("is_dangerous", this.IsDangerous);
            dictionary.Add("is_friable", this.IsFriable);
            dictionary.Add("is_sku", this.IsSku);
            dictionary.Add("item_code", this.ItemCode);
            dictionary.Add("length", this.Length);
            dictionary.Add("name", this.Name);
            dictionary.Add("package_material", this.PackageMaterial);
            dictionary.Add("price", this.Price);
            dictionary.Add("pricing_cat", this.PricingCat);
            dictionary.Add("pro_name_list", this.ProNameList);
            dictionary.Add("pro_value_list", this.ProValueList);
            dictionary.Add("remark", this.Remark);
            dictionary.Add("support_batch", this.SupportBatch);
            dictionary.Add("title", this.Title);
            dictionary.Add("type", this.Type);
            dictionary.Add("volume", this.Volume);
            dictionary.Add("weight", this.Weight);
            dictionary.Add("width", this.Width);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("is_sku", this.IsSku);
            RequestValidator.ValidateRequired("item_code", this.ItemCode);
            RequestValidator.ValidateRequired("name", this.Name);
        }

        public string Color { get; set; }

        public string GoodsCat { get; set; }

        public long? Height { get; set; }

        public bool? IsDangerous { get; set; }

        public bool? IsFriable { get; set; }

        public string IsSku { get; set; }

        public string ItemCode { get; set; }

        public long? Length { get; set; }

        public string Name { get; set; }

        public string PackageMaterial { get; set; }

        public long? Price { get; set; }

        public string PricingCat { get; set; }

        public string ProNameList { get; set; }

        public string ProValueList { get; set; }

        public string Remark { get; set; }

        public bool? SupportBatch { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public long? Volume { get; set; }

        public long? Weight { get; set; }

        public long? Width { get; set; }
    }
}

