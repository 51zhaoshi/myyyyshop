namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ItempropsGetRequest : ITopRequest<ItempropsGetResponse>
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
            return "taobao.itemprops.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("child_path", this.ChildPath);
            dictionary.Add("cid", this.Cid);
            dictionary.Add("fields", this.Fields);
            dictionary.Add("is_color_prop", this.IsColorProp);
            dictionary.Add("is_enum_prop", this.IsEnumProp);
            dictionary.Add("is_input_prop", this.IsInputProp);
            dictionary.Add("is_item_prop", this.IsItemProp);
            dictionary.Add("is_key_prop", this.IsKeyProp);
            dictionary.Add("is_sale_prop", this.IsSaleProp);
            dictionary.Add("parent_pid", this.ParentPid);
            dictionary.Add("pid", this.Pid);
            dictionary.Add("type", this.Type);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("cid", this.Cid);
            RequestValidator.ValidateMaxValue("type", this.Type, 2L);
            RequestValidator.ValidateMinValue("type", this.Type, 1L);
        }

        public string ChildPath { get; set; }

        public long? Cid { get; set; }

        public string Fields { get; set; }

        public bool? IsColorProp { get; set; }

        public bool? IsEnumProp { get; set; }

        public bool? IsInputProp { get; set; }

        public bool? IsItemProp { get; set; }

        public bool? IsKeyProp { get; set; }

        public bool? IsSaleProp { get; set; }

        public long? ParentPid { get; set; }

        public long? Pid { get; set; }

        public long? Type { get; set; }
    }
}

