namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WlbItemUpdateRequest : ITopRequest<WlbItemUpdateResponse>
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
            return "taobao.wlb.item.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("color", this.Color);
            dictionary.Add("delete_property_key_list", this.DeletePropertyKeyList);
            dictionary.Add("goods_cat", this.GoodsCat);
            dictionary.Add("height", this.Height);
            dictionary.Add("id", this.Id);
            dictionary.Add("is_dangerous", this.IsDangerous);
            dictionary.Add("is_friable", this.IsFriable);
            dictionary.Add("length", this.Length);
            dictionary.Add("name", this.Name);
            dictionary.Add("package_material", this.PackageMaterial);
            dictionary.Add("pricing_cat", this.PricingCat);
            dictionary.Add("remark", this.Remark);
            dictionary.Add("title", this.Title);
            dictionary.Add("update_property_key_list", this.UpdatePropertyKeyList);
            dictionary.Add("update_property_value_list", this.UpdatePropertyValueList);
            dictionary.Add("volume", this.Volume);
            dictionary.Add("weight", this.Weight);
            dictionary.Add("width", this.Width);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("id", this.Id);
        }

        public string Color { get; set; }

        public string DeletePropertyKeyList { get; set; }

        public string GoodsCat { get; set; }

        public long? Height { get; set; }

        public long? Id { get; set; }

        public bool? IsDangerous { get; set; }

        public bool? IsFriable { get; set; }

        public long? Length { get; set; }

        public string Name { get; set; }

        public string PackageMaterial { get; set; }

        public string PricingCat { get; set; }

        public string Remark { get; set; }

        public string Title { get; set; }

        public string UpdatePropertyKeyList { get; set; }

        public string UpdatePropertyValueList { get; set; }

        public long? Volume { get; set; }

        public long? Weight { get; set; }

        public long? Width { get; set; }
    }
}

