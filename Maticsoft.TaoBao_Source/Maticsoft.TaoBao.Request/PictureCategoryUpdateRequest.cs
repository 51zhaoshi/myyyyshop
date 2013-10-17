namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PictureCategoryUpdateRequest : ITopRequest<PictureCategoryUpdateResponse>
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
            return "taobao.picture.category.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("category_id", this.CategoryId);
            dictionary.Add("category_name", this.CategoryName);
            dictionary.Add("parent_id", this.ParentId);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("category_id", this.CategoryId);
            RequestValidator.ValidateMaxLength("category_name", this.CategoryName, 20);
        }

        public long? CategoryId { get; set; }

        public string CategoryName { get; set; }

        public long? ParentId { get; set; }
    }
}

